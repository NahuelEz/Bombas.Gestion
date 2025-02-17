using Clover.DbLayer;
using Clover.Shared;
using iText.Html2pdf;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Font;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Clover.Gestion
{
    public static class PdfGeneration
    {
        private const string PdfEncryptionPassword = "sellosmec2016";

        /// <summary>
        /// Exporta un presupuesto en formato PDF.
        /// </summary>
        /// <param name="estimate">Presupuesto a exportar.</param>
        /// <param name="items">Items del presupuesto.</param>
        /// <param name="business">Empresa.</param>
        /// <param name="customer">Cliente.</param>
        /// <param name="contact">Contacto del cliente.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        /// <param name="keepItemTogether">Opción para mantener ítems sin cortes.</param>
        /// <returns></returns>
        public static void ExportPdfEstimate(Estimate estimate, List<EstimateItem> items, Business business, Customer customer, CustomerContact contact, string pdfPath, bool keepItemTogether)
        {
            Style StNormal = GetStyle(12, "Helvetica", ColorConstants.BLACK, false);
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            Style StVerySmall = GetStyle(8, "Helvetica", ColorConstants.BLACK, false);
            var baseWriter = new PdfWriter(pdfPath,
                    new WriterProperties().SetStandardEncryption(null, System.Text.Encoding.UTF8.GetBytes(PdfEncryptionPassword), EncryptionConstants.ALLOW_PRINTING,
                    EncryptionConstants.ENCRYPTION_AES_128 | EncryptionConstants.DO_NOT_ENCRYPT_METADATA));
            var baseDocument = new PdfDocument(baseWriter);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(20, 20, 20, 20);
                #region Encabezado
                var businessLogo = new Image(ImageDataFactory.Create(business.BusinessLogo));
                businessLogo.ScaleAbsolute(245F, 144F);
                document.Add(businessLogo);
                var canvas = new PdfCanvas(baseDocument.GetPage(1));
                // Dibuja rectángulo de información del presupuesto.
                canvas
                    .SetFillColorGray(0.8F)
                    .RoundRectangle(305, 740, 270, 80, 20)
                    .Fill();
                // Dibuja rectángulo de información del cliente.
                canvas
                    .RoundRectangle(20, 580, 555, 90, 20)
                    .Stroke();
                // Dibuja rectángulo de recordatorio orden de compra.
                canvas
                    .RoundRectangle(305, 678, 270, 55, 20)
                    .Stroke();
                string formattedAddress = string.IsNullOrWhiteSpace(customer.Address) ? "< No registrado >" : $"{customer.Address}, {customer.City}, {customer.District}, {customer.Country}";
                string formattedDescription = string.IsNullOrWhiteSpace(estimate.Description) ? string.Empty : estimate.Description;
                document.Add(new Paragraph("Propuesta comercial N°:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 792, 200));
                document.Add(new Paragraph("Fecha:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 770, 200));
                document.Add(new Paragraph("Validez de la oferta:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 748, 200));
                document.Add(new Paragraph("Emitir orden de compra a nombre de:").AddStyle(StNormalBold).SetFontSize(12).SetFixedPosition(325, 710, 220));
                document.Add(new Paragraph("Sellosmec y Bombascen Argentina S.A.").AddStyle(StNormalBold).SetFontSize(12).SetFixedPosition(325, 695, 220));
                document.Add(new Paragraph("CUIT: 30-71698734-1").AddStyle(StNormalBold).SetFontSize(12).SetFixedPosition(325, 680, 220));
                document.Add(new Paragraph(estimate.EstimateID.ToString("D8")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 792, 200));
                document.Add(new Paragraph(estimate.Date.ToString("dd/MM/yy")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 770, 200));
                document.Add(new Paragraph($"{(estimate.ExpirationDate.Date - estimate.Date.Date).Days} días.").AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 748, 200));
                document.Add(new Paragraph("Cliente:").AddStyle(StNormalBold).SetFixedPosition(40, 640, 100));
                document.Add(new Paragraph("Atención:").AddStyle(StNormalBold).SetFixedPosition(40, 615, 100));
                document.Add(new Paragraph("Dirección:").AddStyle(StNormalBold).SetFixedPosition(40, 590, 100));
                document.Add(new Paragraph("Teléfono:").AddStyle(StNormalBold).SetFixedPosition(300, 640, 100));
                document.Add(new Paragraph("Correo:").AddStyle(StNormalBold).SetFixedPosition(300, 615, 100));
                document.Add(new Paragraph($"{customer.CustomerName} ({customer.IdentityNumber})").AddStyle(StNormal).SetFixedPosition(100, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Greeting).AddStyle(StNormal).SetFixedPosition(100, 603, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(formattedAddress).AddStyle(StNormal).SetFixedPosition(100, 578, 460).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Phone).AddStyle(StNormal).SetFixedPosition(360, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Email).AddStyle(StNormal).SetFixedPosition(360, 603, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph().SetMarginTop(115).Add(new Text("COTIZAMOS: ").AddStyle(StNormalBold)).Add(new Text(formattedDescription).AddStyle(StNormal)));
                #endregion
                #region Detalle de ítems
                var itemsTable = new Table(new float[] { 1, 1, 8, 2, 2, 2 });
                itemsTable.SetFixedLayout();
                itemsTable.UseAllAvailableWidth();
                itemsTable.SetMarginTop(20);
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Item").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Cant.").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Descripción").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("IVA (%)").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                     .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("P. unitario").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Subtotal").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                int itemNumber = 1;
                foreach (var item in items)
                {
                    itemsTable.AddCell(new Cell().Add(new Paragraph(itemNumber.ToString()).AddStyle(StNormal)).SetBorderLeft(Border.NO_BORDER));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToStringPreferIntegerFormat()).AddStyle(StNormal)));
                    var descriptionCell = new Cell().Add(new Paragraph(item.Description).AddStyle(StNormal)).SetKeepTogether(keepItemTogether);
                    System.Drawing.Image itemImage = null;
                    if (item.CustomImage != null)
                    {
                        itemImage = item.CustomImage.ToImage();
                    }
                    else if (item.ProductImage != null)
                    {
                        itemImage = item.ProductImage.ToImage();
                    }
                    if (itemImage != null)
                    {
                        var descriptionImage = new Image(ImageDataFactory.Create(itemImage, System.Drawing.Color.White));
                        descriptionImage.ScaleAbsolute(90, 90);
                        descriptionCell.Add(descriptionImage);
                        descriptionCell.Add(new Paragraph("La imagen es solo a modo ilustrativo. Puede diferir de acuerdo a lo cotizado.").AddStyle(StVerySmall));
                    }
                    if (item.DeliveryDelay.HasValue)
                    {
                        if (item.DeliveryDelay.Value == 0)
                        {
                            descriptionCell.Add(new Paragraph("Fecha de entrega: entrega inmediata.").AddStyle(StNormalBold));
                        }
                        else
                        {
                            descriptionCell.Add(new Paragraph($"Fecha de entrega: {item.DeliveryDelay} día(s) a partir de aceptación.").AddStyle(StNormalBold));
                        }
                    }
                    itemsTable.AddCell(descriptionCell);
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.VatPercentage.ToStringPreferIntegerFormat()).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Amount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.TotalAmount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)).SetBorderRight(Border.NO_BORDER));
                    itemNumber++;
                }
                document.Add(itemsTable);
                #endregion
                #region Totales
                if (estimate.DontTotalize == false)
                {
                    var totalsTable = new Table(new float[] { 1, 1, 1, 1 });
                    totalsTable.SetFixedLayout();
                    totalsTable.UseAllAvailableWidth();
                    totalsTable.SetMarginTop(20);
                    totalsTable.SetKeepTogether(true);
                    // Calcula totales.
                    decimal totalAmount = items.Sum(i => i.TotalAmount);
                    decimal discount = totalAmount * estimate.Discount / 100M;
                    decimal totalBeforeTax = totalAmount - discount;
                    decimal vatTotal = items.Sum(i => (i.TotalAmount * i.VatPercentage / 100M)) * (100M - estimate.Discount) / 100M;
                    decimal grandTotal = totalBeforeTax + vatTotal;
                    if (discount == 0)
                    {
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph("Total").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph(totalBeforeTax.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                    }
                    else
                    {
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph("Subtotal").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph(totalAmount.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph($"Descuento {estimate.Discount.ToStringPreferIntegerFormat()}%").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph($"-{discount:N2}").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph("Total").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph(totalBeforeTax.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                    }
                    if (vatTotal != 0)
                    {
                        foreach (var vatGroup in items.GroupBy(i => i.VatPercentage).Where(g => g.Key != 0))
                        {
                            decimal vatAmount = (vatGroup.Sum(i => (i.TotalAmount * i.VatPercentage / 100M)) * (100M - estimate.Discount) / 100M);
                            totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                            totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                            totalsTable.AddCell(new Cell().Add(new Paragraph($"IVA {vatGroup.Key.ToStringPreferIntegerFormat()}%").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                            totalsTable.AddCell(new Cell().Add(new Paragraph(vatAmount.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                        }
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph("Total IVA incluido").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                        totalsTable.AddCell(new Cell().Add(new Paragraph(grandTotal.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                    }
                    document.Add(totalsTable);
                }
                else
                {
                    document.Add(new Paragraph("IMPUESTOS NO INCLUIDOS").AddStyle(StNormalBold));
                }
                #endregion
                #region Pie de página
                var footer = new Div().SetMarginTop(20).SetKeepTogether(true);
                footer.Add(new Paragraph("CONDICIONES COMERCIALES:").AddStyle(StNormalBold));
                var conditionsList = new List().SetSymbolIndent(12).SetListSymbol("\u2022").AddStyle(StNormal);

                // Nota sobre la moneda en la que se expresan los montos
                if (estimate.CurrencyID == 1) // ARS
                {
                    conditionsList.Add(new ListItem($"Los montos están expresados en: {estimate.CurrencyName}"));
                }
                if (estimate.CurrencyID == 2) // USD
                {
                    conditionsList.Add(new ListItem($"Los montos están expresados en: {estimate.CurrencyName}, al tipo de cambio según cotización Dólar Billete tipo vendedor del Banco Nación."));
                    conditionsList.Add(new ListItem("A la fecha de cobro, en caso de que el dólar haya sufrido una variación mayor al 5% del valor cotizado, se realizará una nota de débito o crédito según corresponda."));
                }

                // Nota sobre el medio de pago
                conditionsList.Add(new ListItem($"El medio de pago es: {estimate.PaymentName}"));

                // Nota sobre el plazo de pago
                if (customer.PaymentTerm > 0)
                {
                    conditionsList.Add(new ListItem($"Plazo de pago: {customer.PaymentTerm} días."));
                }

                // Nota sobre la fecha de entrega
                if (estimate.DeliveryDelay.HasValue)
                {
                    conditionsList.Add(new ListItem($"La fecha de entrega será: {estimate.Date.AddDays(estimate.DeliveryDelay.Value):dd/MM/yy}"));
                }

                // Nota sobre la garantía
                if (estimate.WarrantyMonths > 0)
                {
                    conditionsList.Add(new ListItem($"GARANTÍA: {estimate.WarrantyMonths} mes(es)."));
                }
                conditionsList.Add(new ListItem("Las reparaciones de bombas, sellos mecánicos o motores tienen una garantía de 60 días corridos, a partir de la fecha de facturación o remito."));
                
                // Nota sobre especiales
                conditionsList.Add(new ListItem("Los repuestos y fabricaciones especiales no tienen devolución."));

                footer.Add(conditionsList);
                footer.Add(new Paragraph($"Lo saluda atentamente, {business.BusinessName}").AddStyle(StNormal).SetMarginTop(20).SetTextAlignment(TextAlignment.RIGHT));
                document.Add(footer);
                #endregion
                document.Close();
            }
        }

        /// <summary>
        /// Exporta comprobante de presupuesto en formato PDF. 
        /// </summary>
        /// <param name="sale">Venta a exportar.</param>
        /// <param name="items">Items del presupuesto.</param>
        /// <param name="customer">Cliente.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        public static void ExportPdfControlTicket(Sale sale, List<SaleItem> items, Customer customer, string pdfPath)
        {
            Style StBig = GetStyle(42, "Helvetica", ColorConstants.BLACK, false);
            Style StNormal = GetStyle(12, "Helvetica", ColorConstants.BLACK, false);
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            Style StSmall = GetStyle(10, "Helvetica", ColorConstants.BLACK, false);
            var baseWriter = new PdfWriter(pdfPath);
            var baseDocument = new PdfDocument(baseWriter);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(20, 20, 20, 20);
                var headerTable = new Table(new float[] { 1, 1, 1 });
                headerTable.SetFixedLayout();
                headerTable.UseAllAvailableWidth();
                headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().Add(new Paragraph(new Text("X").AddStyle(StBig).SetBorder(new SolidBorder(1)))).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().Add(new Paragraph(DateTime.Today.ToString("dd-MM-yyyy")).AddStyle(StNormalBold)).SetTextAlignment(TextAlignment.RIGHT).SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().Add(new Paragraph("DOCUMENTO NO VÁLIDO COMO FACTURA").AddStyle(StSmall)).SetTextAlignment(TextAlignment.CENTER).SetBorder(Border.NO_BORDER));
                headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                document.Add(headerTable);
                document.Add(new Paragraph().Add(new Text("Cliente: ").AddStyle(StNormalBold)).Add(new Text(customer.CustomerName).AddStyle(StNormal)).SetMarginTop(20));
                document.Add(new Paragraph().Add(new Text("CUIT/DNI: ").AddStyle(StNormalBold)).Add(new Text(customer.IdentityNumber).AddStyle(StNormal)));
                document.Add(new Paragraph().Add(new Text("Condición frente al IVA: ").AddStyle(StNormalBold)).Add(new Text(customer.TaxGroup).AddStyle(StNormal)));
                string formattedAddress = string.IsNullOrWhiteSpace(customer.Address) ? "< No registrado >" : $"{customer.Address}, {customer.City}, {customer.District}, {customer.Country}";
                document.Add(new Paragraph().Add(new Text("Dirección: ").AddStyle(StNormalBold)).Add(new Text(formattedAddress).AddStyle(StNormal)));
                var itemsTable = new Table(new float[] { 1, 1, 8, 2, 2 });
                itemsTable.SetFixedLayout();
                itemsTable.UseAllAvailableWidth();
                itemsTable.SetMarginTop(20);
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Item").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                .SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Cant.").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Descripción").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("P. unitario").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Subtotal").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                foreach (var item in items)
                {
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.ItemNumber.ToString()).AddStyle(StNormal)).SetBorderLeft(Border.NO_BORDER));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToStringPreferIntegerFormat()).AddStyle(StNormal)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Description).AddStyle(StNormal)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Amount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.TotalAmount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)).SetBorderRight(Border.NO_BORDER));
                }
                document.Add(itemsTable);
                var totals = new Div().SetMarginTop(20).SetKeepTogether(true);
                var totalsTable = new Table(new float[] { 1, 1, 1, 1 });
                totalsTable.SetFixedLayout();
                totalsTable.UseAllAvailableWidth();
                totalsTable.SetKeepTogether(true);
                // Calcula totales.
                decimal totalAmount = items.Sum(i => i.TotalAmount);
                decimal discount = (totalAmount * sale.Discount / 100M);
                decimal totalBeforeTax = (totalAmount - discount);
                if (discount == 0)
                {
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph("Total").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph(totalBeforeTax.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                }
                else
                {
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph("Subtotal").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph(totalAmount.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph($"Descuento {sale.Discount.ToStringPreferIntegerFormat()}%").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph($"-{discount:N2}").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph("Total").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph(totalBeforeTax.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                }
                totals.Add(totalsTable);
                totals.Add(new Paragraph($"Los montos están expresados en: {sale.CurrencySymbol}").AddStyle(StNormal).SetMarginTop(20));
                document.Add(totals);
                document.Close();
            }
        }

        /// <summary>
        /// Exporta remito en formato PDF.
        /// </summary>
        /// <param name="deliveryNote">Remito a exportar.</param>
        /// <param name="selectedItems">Items de la venta asociados al remito.</param>
        /// <param name="sale">Venta asociada.</param>
        /// <param name="customer">Cliente correspondiente.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        /// <param name="includeItemAmounts">Especifica si se deben incluir los importes de los ítems.</param>
        public static void ExportPdfDeliveryNote(DeliveryNote deliveryNote, List<SaleItem> selectedItems, Sale sale, Customer customer, string pdfPath, bool includeItemAmounts)
        {
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            Style StVerySmallBold = GetStyle(8, "Helvetica", ColorConstants.BLACK, true);
            var baseWriter = new PdfWriter(pdfPath);
            var baseDocument = new PdfDocument(baseWriter);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(370, 40, 140, 40);
                string formattedAddress = string.IsNullOrWhiteSpace(customer.Address) ? string.Empty : $"{customer.Address}, {customer.City}, {customer.District}";
                string formattedInvoiceNumber = string.IsNullOrWhiteSpace(deliveryNote.MaskedInvoiceNumber) ? string.Empty : deliveryNote.MaskedInvoiceNumber;
                document.Add(new Paragraph(DateTime.Today.ToString("dd")).AddStyle(StNormalBold).SetFixedPosition(400, 738, 50));
                document.Add(new Paragraph(DateTime.Today.ToString("MM")).AddStyle(StNormalBold).SetFixedPosition(430, 738, 50));
                document.Add(new Paragraph(DateTime.Today.ToString("yy")).AddStyle(StNormalBold).SetFixedPosition(460, 738, 50));
                document.Add(new Paragraph(customer.CustomerName).AddStyle(StNormalBold).SetFixedPosition(100, 600, 200).SetFixedLeading(25).SetHeight(60));
                document.Add(new Paragraph(customer.TaxGroup).AddStyle(StNormalBold).SetFixedPosition(100, 590, 305));
                document.Add(new Paragraph(formattedAddress).AddStyle(StVerySmallBold).SetFixedPosition(350, 600, 200).SetFixedLeading(25).SetHeight(60));
                document.Add(new Paragraph(customer.IdentityNumber).AddStyle(StNormalBold).SetFixedPosition(415, 590, 300));
                document.Add(new Paragraph(formattedInvoiceNumber).AddStyle(StNormalBold).SetFixedPosition(415, 565, 300));
                if (includeItemAmounts)
                {
                    var itemsTable = new Table(new float[] { 2, 10, 3 });
                    itemsTable.SetFixedLayout();
                    itemsTable.UseAllAvailableWidth();
                    foreach (var item in selectedItems)
                    {
                        itemsTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToStringPreferIntegerFormat()).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER)).SetBorder(Border.NO_BORDER));
                        itemsTable.AddCell(new Cell().Add(new Paragraph(item.Description.Replace(Environment.NewLine, " ")).AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                        itemsTable.AddCell(new Cell().Add(new Paragraph(item.TotalAmount.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                    }
                    // Agrega número de orden de compra (si está registrado).
                    if (!string.IsNullOrWhiteSpace(sale.PurchaseOrderNumber))
                    {
                        itemsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        itemsTable.AddCell(new Cell().Add(new Paragraph("N° orden de compra: " + sale.PurchaseOrderNumber).AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                        itemsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    }
                    itemsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    itemsTable.AddCell(new Cell().Add(new Paragraph("(Todas las reparaciones de bombas, sellos mecánicos o motores tienen una garantía de 60 días corridos, a partir de la fecha de facturación o remito.)").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    itemsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    document.Add(itemsTable);
                }
                else
                {
                    var itemsTable = new Table(new float[] { 1, 8 });
                    itemsTable.SetFixedLayout();
                    itemsTable.UseAllAvailableWidth();
                    foreach (var item in selectedItems)
                    {
                        itemsTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToStringPreferIntegerFormat()).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER)).SetBorder(Border.NO_BORDER));
                        itemsTable.AddCell(new Cell().Add(new Paragraph(item.Description.Replace(Environment.NewLine, " ")).AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    }
                    // Agrega número de orden de compra (si está registrado).
                    if (!string.IsNullOrWhiteSpace(sale.PurchaseOrderNumber))
                    {
                        itemsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                        itemsTable.AddCell(new Cell().Add(new Paragraph("N° orden de compra: " + sale.PurchaseOrderNumber).AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    }
                    itemsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    itemsTable.AddCell(new Cell().Add(new Paragraph("(Todas las reparaciones de bombas, sellos mecánicos o motores tienen una garantía de 60 días corridos, a partir de la fecha de facturación o remito.)").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    document.Add(itemsTable);
                }
                document.Close();
            }
        }

        /// <summary>
        /// Exporta remito no registrado en formato PDF.
        /// </summary>
        /// <param name="items">Items asociados al remito.</param>
        /// <param name="customer">Cliente correspondiente.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        public static void ExportUnregisteredPdfDeliveryNote(List<EstimateItem> items, Customer customer, string pdfPath)
        {
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            Style StVerySmallBold = GetStyle(8, "Helvetica", ColorConstants.BLACK, true);
            var baseWriter = new PdfWriter(pdfPath);
            var baseDocument = new PdfDocument(baseWriter);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(370, 40, 140, 40);
                string formattedAddress = string.IsNullOrWhiteSpace(customer.Address) ? string.Empty : $"{customer.Address}, {customer.City}, {customer.District}";
                document.Add(new Paragraph(DateTime.Today.ToString("dd")).AddStyle(StNormalBold).SetFixedPosition(400, 738, 50));
                document.Add(new Paragraph(DateTime.Today.ToString("MM")).AddStyle(StNormalBold).SetFixedPosition(430, 738, 50));
                document.Add(new Paragraph(DateTime.Today.ToString("yy")).AddStyle(StNormalBold).SetFixedPosition(460, 738, 50));
                document.Add(new Paragraph(customer.CustomerName).AddStyle(StNormalBold).SetFixedPosition(100, 600, 200).SetFixedLeading(25).SetHeight(60));
                document.Add(new Paragraph(customer.TaxGroup).AddStyle(StNormalBold).SetFixedPosition(100, 590, 305));
                document.Add(new Paragraph(formattedAddress).AddStyle(StVerySmallBold).SetFixedPosition(350, 600, 200).SetFixedLeading(25).SetHeight(60));
                document.Add(new Paragraph(customer.IdentityNumber).AddStyle(StNormalBold).SetFixedPosition(415, 590, 300));
                var itemsTable = new Table(new float[] { 1, 8 });
                itemsTable.SetFixedLayout();
                itemsTable.UseAllAvailableWidth();
                foreach (var item in items)
                {
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToStringPreferIntegerFormat()).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER)).SetBorder(Border.NO_BORDER));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Description.Replace(Environment.NewLine, " ")).AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                }
                itemsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                itemsTable.AddCell(new Cell().Add(new Paragraph("(Todas las reparaciones de bombas, sellos mecánicos o motores tienen una garantía de 60 días corridos, a partir de la fecha de facturación o remito.)").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                document.Add(itemsTable);
                document.Close();
            }
        }

        /// <summary>
        /// Exporta tabla genérica en formato PDF.
        /// </summary>
        /// <param name="documentTitle">Título a colocar en la primera línea del documento.</param>
        /// <param name="columnWidths">Arreglo de decimales con los anchos de cada columna.</param>
        /// <param name="dataTable">Tabla con la información a exportar.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        /// <param name="drawTableBorders">Indica si se deben dibujar los bordes de la tabla.</param>
        public static void ExportPdfDataTable(string documentTitle, float[] columnWidths, DataTable dataTable, string pdfPath, bool drawTableBorders = true, bool landscape = false, string footer = "")
        {
            Style StNormal = GetStyle(12, "Helvetica", ColorConstants.BLACK, false);
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            var baseWriter = new PdfWriter(pdfPath);
            var baseDocument = new PdfDocument(baseWriter);
            var pageSize = iText.Kernel.Geom.PageSize.A4;
            if (landscape)
            {
                pageSize = pageSize.Rotate();
            }
            using (var document = new Document(baseDocument, pageSize))
            {
                document.SetMargins(20, 20, 20, 20);
                document.Add(new Paragraph(documentTitle).AddStyle(StNormalBold));
                var table = new Table(columnWidths);
                table.SetFixedLayout();
                table.UseAllAvailableWidth();
                table.SetMarginTop(10);
                // Encabezados
                int columnCount = dataTable.Columns.Count;
                foreach (DataColumn column in dataTable.Columns)
                {
                    var cell = new Cell().Add(new Paragraph(column.Caption).AddStyle(StNormalBold));
                    // Lógica para manejo de bordes.
                    if (drawTableBorders)
                    {
                        cell.SetBorderTop(Border.NO_BORDER);
                        if (column.Ordinal == 0) // Primera columna
                        {
                            cell.SetBorderLeft(Border.NO_BORDER);
                        }
                        else if (column.Ordinal == columnCount - 1) // Última columna
                        {
                            cell.SetBorderRight(Border.NO_BORDER);
                        }
                    }
                    else
                    {
                        cell.SetBorder(Border.NO_BORDER);
                    }
                    table.AddHeaderCell(cell);
                }
                // Filas
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0; i < columnCount; i++)
                    {
                        var cell = new Cell().SetKeepTogether(true);
                        object value = row[i];
                        string formattedValue;
                        // Lógica para formatear el valor según su tipo.
                        switch (value)
                        {
                            case DateTime date:
                                formattedValue = date.ToString("dd/MM/yy");
                                break;
                            case decimal number:
                                formattedValue = number.ToString("N2");
                                cell.SetTextAlignment(TextAlignment.RIGHT);
                                break;
                            default:
                                formattedValue = Convert.ToString(value);
                                break;
                        }
                        cell.Add(new Paragraph(formattedValue).AddStyle(StNormal));
                        // Lógica para manejo de bordes.
                        if (drawTableBorders)
                        {
                            if (i == 0) // Primera columna
                            {
                                cell.SetBorderLeft(Border.NO_BORDER);
                            }
                            else if (i == columnCount - 1) // Última columna
                            {
                                cell.SetBorderRight(Border.NO_BORDER);
                            }
                        }
                        else
                        {
                            cell.SetBorder(Border.NO_BORDER);
                        }
                        table.AddCell(cell);
                    }
                }
                document.Add(table);
                if (!string.IsNullOrWhiteSpace(footer))
                {
                    document.Add(new Paragraph(footer).AddStyle(StNormal));
                }
                document.Close();
            }
        }

        /// <summary>
        /// Exporta una orden de compra en formato PDF.
        /// </summary>
        /// <param name="order">Orden de compra a exportar.</param>
        /// <param name="items">Items de la orden.</param>
        /// <param name="business">Empresa.</param>
        /// <param name="provider">Proveedor.</param>
        /// <param name="contact">Contacto del proveedor.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        public static void ExportPdfPurchaseOrder(PurchaseOrder order, List<PurchaseOrderItem> items, Business business, Provider provider, ProviderContact contact, string pdfPath)
        {
            Style StNormal = GetStyle(12, "Helvetica", ColorConstants.BLACK, false);
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            var baseWriter = new PdfWriter(pdfPath,
                    new WriterProperties().SetStandardEncryption(null, System.Text.Encoding.UTF8.GetBytes(PdfEncryptionPassword), EncryptionConstants.ALLOW_PRINTING,
                    EncryptionConstants.ENCRYPTION_AES_128 | EncryptionConstants.DO_NOT_ENCRYPT_METADATA));
            var baseDocument = new PdfDocument(baseWriter);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(20, 20, 20, 20);
                #region Encabezado
                var businessLogo = new Image(ImageDataFactory.Create(business.BusinessLogo));
                businessLogo.ScaleAbsolute(245F, 144F);
                document.Add(businessLogo);
                var canvas = new PdfCanvas(baseDocument.GetPage(1));
                // Dibuja rectángulo de información de la orden de compra.
                canvas
                    .SetFillColor(new DeviceRgb(230, 185, 185))
                    .RoundRectangle(305, 755, 270, 65, 20)
                    .Fill();
                // Dibuja rectángulo de información del proveedor.
                canvas
                    .RoundRectangle(20, 580, 555, 90, 20)
                    .Stroke();
                // Dibuja rectángulo de recordatorio factura.
                canvas
                    .RoundRectangle(305, 678, 270, 55, 20)
                    .Stroke();
                string formattedAddress = string.IsNullOrWhiteSpace(provider.Address) ? "< No registrado >" : $"{provider.Address}, {provider.City}, {provider.District}, {provider.Country}";
                document.Add(new Paragraph("Orden de compra N°:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 792, 200));
                document.Add(new Paragraph("Fecha:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 770, 200));
                document.Add(new Paragraph("Emitir factura a nombre de:").AddStyle(StNormalBold).SetFontSize(12).SetFixedPosition(325, 710, 220));
                document.Add(new Paragraph("Sellosmec y Bombascen Argentina S.A.").AddStyle(StNormalBold).SetFontSize(12).SetFixedPosition(325, 695, 220));
                document.Add(new Paragraph("CUIT: 30-71698734-1").AddStyle(StNormalBold).SetFontSize(12).SetFixedPosition(325, 680, 220));
                document.Add(new Paragraph(order.PurchaseOrderID.ToString("D8")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 790, 200));
                document.Add(new Paragraph(order.Date.ToString("dd/MM/yy")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 765, 200));
                document.Add(new Paragraph("Proveedor:").AddStyle(StNormalBold).SetFixedPosition(40, 640, 100));
                document.Add(new Paragraph("Atención:").AddStyle(StNormalBold).SetFixedPosition(40, 615, 100));
                document.Add(new Paragraph("Dirección:").AddStyle(StNormalBold).SetFixedPosition(40, 590, 100));
                document.Add(new Paragraph("Teléfono:").AddStyle(StNormalBold).SetFixedPosition(300, 640, 100));
                document.Add(new Paragraph("Correo:").AddStyle(StNormalBold).SetFixedPosition(300, 615, 100));
                document.Add(new Paragraph(provider.ProviderName).AddStyle(StNormal).SetFixedPosition(100, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Greeting).AddStyle(StNormal).SetFixedPosition(100, 603, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(formattedAddress).AddStyle(StNormal).SetFixedPosition(100, 578, 460).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Phone).AddStyle(StNormal).SetFixedPosition(360, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Email).AddStyle(StNormal).SetFixedPosition(360, 603, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph("COTIZACIÓN:").AddStyle(StNormalBold).SetMarginTop(115));
                #endregion
                #region Detalle de ítems
                var itemsTable = new Table(new float[] { 1, 1, 8, 2, 2, 2 });
                itemsTable.SetFixedLayout();
                itemsTable.UseAllAvailableWidth();
                itemsTable.SetMarginTop(20);
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Item").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Cant.").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Descripción").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("IVA (%)").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                     .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("P. unitario").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                itemsTable.AddHeaderCell(new Cell().Add(new Paragraph("Subtotal").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                int itemNumber = 1;
                foreach (var item in items)
                {
                    itemsTable.AddCell(new Cell().Add(new Paragraph(itemNumber.ToString()).AddStyle(StNormal)).SetBorderLeft(Border.NO_BORDER));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Quantity.ToStringPreferIntegerFormat()).AddStyle(StNormal)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Description).AddStyle(StNormal)).SetKeepTogether(true));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.VatPercentage.ToStringPreferIntegerFormat()).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.Amount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)));
                    itemsTable.AddCell(new Cell().Add(new Paragraph(item.TotalAmount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)).SetBorderRight(Border.NO_BORDER));
                    itemNumber++;
                }
                document.Add(itemsTable);
                #endregion
                #region Totales
                var totalsTable = new Table(new float[] { 1, 1, 1, 1 });
                totalsTable.SetFixedLayout();
                totalsTable.UseAllAvailableWidth();
                totalsTable.SetMarginTop(20);
                totalsTable.SetKeepTogether(true);
                // Calcula totales.
                decimal totalBeforeTax = items.Sum(i => i.TotalAmount);
                decimal vatTotal = items.Sum(i => (i.TotalAmount * i.VatPercentage / 100M));
                decimal grandTotal = (totalBeforeTax + vatTotal);
                totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                totalsTable.AddCell(new Cell().Add(new Paragraph("Total").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                totalsTable.AddCell(new Cell().Add(new Paragraph(totalBeforeTax.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                foreach (var vatGroup in items.GroupBy(i => i.VatPercentage).Where(g => g.Key != 0))
                {
                    decimal vatAmount = vatGroup.Sum(i => (i.TotalAmount * i.VatPercentage / 100M));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph($"IVA {vatGroup.Key.ToStringPreferIntegerFormat()}%").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph(vatAmount.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                }
                totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                totalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                totalsTable.AddCell(new Cell().Add(new Paragraph("Total IVA incluido").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                totalsTable.AddCell(new Cell().Add(new Paragraph(grandTotal.ToString("N2")).AddStyle(StNormalBold).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                document.Add(totalsTable);
                #endregion
                #region Pie de página
                var footer = new Div().SetMarginTop(20).SetKeepTogether(true);
                footer.Add(new Paragraph($"Montos expresados en: {order.CurrencyName}").AddStyle(StNormal));
                footer.Add(new Paragraph("Por medio de la presente, se acepta la cotización detallada.").AddStyle(StNormal).SetMarginTop(50));
                footer.Add(new Paragraph("Aguardamos factura para emitir pago correspondiente.").AddStyle(StNormal));
                footer.Add(new Paragraph("Atención: las facturas no serán aceptadas si no figura el número de orden de compra correspondiente.").AddStyle(StNormalBold));
                footer.Add(new Paragraph($"Lo saluda atentamente, {business.BusinessName}").AddStyle(StNormal).SetMarginTop(20).SetTextAlignment(TextAlignment.RIGHT));
                document.Add(footer);
                #endregion
                document.Close();
            }
        }

        /// <summary>
        /// Exporta una orden de pago en formato PDF.
        /// </summary>
        /// <param name="order">Orden de pago a exportar.</param>
        /// <param name="invoices">Facturas asociadas a la orden.</param>
        /// <param name="payment">Pagos asociados a la orden.</param>
        /// <param name="business">Empresa.</param>
        /// <param name="provider">Proveedor.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        public static void ExportPdfPayOrder(PayOrder order, List<PurchaseInvoice> invoices, List<PayOrderPayment> payments, Business business, Provider provider, string pdfPath)
        {
            Style StNormal = GetStyle(12, "Helvetica", ColorConstants.BLACK, false);
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            var baseWriter = new PdfWriter(pdfPath,
                    new WriterProperties().SetStandardEncryption(null, System.Text.Encoding.UTF8.GetBytes(PdfEncryptionPassword), EncryptionConstants.ALLOW_PRINTING,
                    EncryptionConstants.ENCRYPTION_AES_128 | EncryptionConstants.DO_NOT_ENCRYPT_METADATA));
            var baseDocument = new PdfDocument(baseWriter);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(20, 20, 20, 20);
                #region Encabezado
                var businessLogo = new Image(ImageDataFactory.Create(business.BusinessLogo));
                businessLogo.ScaleAbsolute(245F, 144F);
                document.Add(businessLogo);
                var canvas = new PdfCanvas(baseDocument.GetPage(1));
                // Dibuja rectángulo de información de la orden de pago.
                canvas
                    .SetFillColor(new DeviceRgb(255, 242, 204))
                    .RoundRectangle(305, 755, 270, 67, 20)
                    .Fill();
                // Dibuja rectángulo de información del proveedor.
                canvas
                    .RoundRectangle(20, 630, 555, 40, 20)
                    .Stroke();
                string formattedAddress = string.IsNullOrWhiteSpace(provider.Address) ? "< No registrado >" : $"{provider.Address}, {provider.City}, {provider.District}, {provider.Country}";
                document.Add(new Paragraph("Orden de pago N°:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 790, 200));
                document.Add(new Paragraph("Fecha:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 765, 200));
                document.Add(new Paragraph(order.PayOrderID.ToString("D8")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 790, 200));
                document.Add(new Paragraph(order.Date.ToString("dd/MM/yy")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 765, 200));
                document.Add(new Paragraph("Proveedor:").AddStyle(StNormalBold).SetFixedPosition(40, 640, 100));
                document.Add(new Paragraph("CUIT/DNI:").AddStyle(StNormalBold).SetFixedPosition(300, 640, 100));
                document.Add(new Paragraph(provider.ProviderName).AddStyle(StNormal).SetFixedPosition(100, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(provider.IdentityNumber).AddStyle(StNormal).SetFixedPosition(360, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph("Facturas:").AddStyle(StNormalBold).SetMarginTop(60));
                #endregion
                #region Detalle de facturas
                var invoicesTable = new Table(new float[] { 1, 1, 1, 1 });
                invoicesTable.SetFixedLayout();
                invoicesTable.UseAllAvailableWidth();
                invoicesTable.SetMarginTop(10);
                invoicesTable.AddHeaderCell(new Cell().Add(new Paragraph("N° de factura").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                invoicesTable.AddHeaderCell(new Cell().Add(new Paragraph("Fecha").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                invoicesTable.AddHeaderCell(new Cell().Add(new Paragraph("Importe").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                invoicesTable.AddHeaderCell(new Cell().Add(new Paragraph("Moneda").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                foreach (var invoice in invoices)
                {
                    invoicesTable.AddCell(new Cell().Add(new Paragraph(invoice.InvoiceNumber).AddStyle(StNormal)).SetBorderLeft(Border.NO_BORDER));
                    invoicesTable.AddCell(new Cell().Add(new Paragraph(invoice.InvoiceDate.ToString("dd/MM/yyyy")).AddStyle(StNormal)));
                    invoicesTable.AddCell(new Cell().Add(new Paragraph(invoice.TotalAmount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)));
                    invoicesTable.AddCell(new Cell().Add(new Paragraph(invoice.CurrencySymbol).AddStyle(StNormal)).SetBorderRight(Border.NO_BORDER));
                }
                document.Add(invoicesTable);
                #endregion
                #region Totales facturas
                var invoicesTotalsTable = new Table(new float[] { 1, 1, 1, 1 });
                invoicesTotalsTable.SetFixedLayout();
                invoicesTotalsTable.UseAllAvailableWidth();
                invoicesTotalsTable.SetMarginTop(10);
                foreach (var group in invoices.GroupBy(i => i.CurrencySymbol))
                {
                    invoicesTotalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    invoicesTotalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    invoicesTotalsTable.AddCell(new Cell().Add(new Paragraph($"Total ({group.Key})").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    invoicesTotalsTable.AddCell(new Cell().Add(new Paragraph(group.Sum(i => i.TotalAmount).ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                }
                document.Add(invoicesTotalsTable);
                #endregion
                #region Detalle pagos
                document.Add(new Paragraph("Valores:").AddStyle(StNormalBold).SetMarginTop(20));
                var paymentsTable = new Table(new float[] { 1, 1, 1, 1 });
                paymentsTable.SetFixedLayout();
                paymentsTable.UseAllAvailableWidth();
                paymentsTable.SetMarginTop(10);
                paymentsTable.AddHeaderCell(new Cell().Add(new Paragraph("Medio de pago").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderLeft(Border.NO_BORDER));
                paymentsTable.AddHeaderCell(new Cell().Add(new Paragraph("Información adicional").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                paymentsTable.AddHeaderCell(new Cell().Add(new Paragraph("Importe").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER));
                paymentsTable.AddHeaderCell(new Cell().Add(new Paragraph("Moneda").AddStyle(StNormalBold).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderTop(Border.NO_BORDER).SetBorderRight(Border.NO_BORDER));
                foreach (var payment in payments)
                {
                    paymentsTable.AddCell(new Cell().Add(new Paragraph(payment.PaymentName).AddStyle(StNormal)).SetBorderLeft(Border.NO_BORDER));
                    paymentsTable.AddCell(new Cell().Add(new Paragraph(payment.AdditionalInformation).AddStyle(StNormal)));
                    paymentsTable.AddCell(new Cell().Add(new Paragraph(payment.TotalAmount.ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)));
                    paymentsTable.AddCell(new Cell().Add(new Paragraph(payment.CurrencySymbol).AddStyle(StNormal)).SetBorderRight(Border.NO_BORDER));
                }
                document.Add(paymentsTable);
                #endregion
                #region Totales pagos
                var paymentsTotalsTable = new Table(new float[] { 1, 1, 1, 1 });
                paymentsTotalsTable.SetFixedLayout();
                paymentsTotalsTable.UseAllAvailableWidth();
                paymentsTotalsTable.SetMarginTop(10);
                foreach (var group in payments.GroupBy(i => i.CurrencySymbol))
                {
                    paymentsTotalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    paymentsTotalsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                    paymentsTotalsTable.AddCell(new Cell().Add(new Paragraph($"Total ({group.Key})").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                    paymentsTotalsTable.AddCell(new Cell().Add(new Paragraph(group.Sum(i => i.TotalAmount).ToString("N2")).AddStyle(StNormal).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                }
                document.Add(paymentsTotalsTable);
                #endregion
                #region Pie de página
                var signaturesTable = new Table(new float[] { 3, 1, 3, 1, 3 });
                signaturesTable.SetFixedLayout();
                signaturesTable.UseAllAvailableWidth();
                signaturesTable.SetMarginTop(100);
                signaturesTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                signaturesTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                signaturesTable.AddCell(new Cell()
                    .Add(new Paragraph("Firma").AddStyle(StNormal).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderLeft(Border.NO_BORDER)
                    .SetBorderRight(Border.NO_BORDER)
                    .SetBorderBottom(Border.NO_BORDER));
                signaturesTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                signaturesTable.AddCell(new Cell()
                    .Add(new Paragraph("Aclaración").AddStyle(StNormal).SetTextAlignment(TextAlignment.CENTER))
                    .SetBorderLeft(Border.NO_BORDER)
                    .SetBorderRight(Border.NO_BORDER)
                    .SetBorderBottom(Border.NO_BORDER));
                document.Add(signaturesTable);
                #endregion
                document.Close();
            }
        }

        /// <summary>
        /// Exporta resumen de órdenes de pago en formato PDF.
        /// </summary>
        /// <param name="payments">Pagos a exportar.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        public static void ExportPdfPayOrdersReport(List<PayOrderPayment> payments, string pdfPath)
        {
            var baseWriter = new PdfWriter(pdfPath);
            var baseDocument = new PdfDocument(baseWriter);
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            Style StSmall = GetStyle(11, "Helvetica", ColorConstants.BLACK, false);
            Style StSmallBold = GetStyle(11, "Helvetica", ColorConstants.BLACK, true);
            var line = new SolidLine(0.7F);
            var thinLine = new SolidLine(0.5F);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(20, 20, 20, 20);
                document.Add(new Paragraph("Informe pagos").AddStyle(StNormalBold));
                foreach (var ordersGroup in payments.GroupBy(p => p.ProviderName).OrderBy(g => g.Key))
                {
                    document.Add(new Paragraph(ordersGroup.Key).AddStyle(StNormalBold).SetKeepWithNext(true));
                    foreach (var paymentsGroup in ordersGroup.GroupBy(g => g.PayOrderID))
                    {
                        document.Add(new Paragraph($"Orden de pago N°: {paymentsGroup.Key:D8}").AddStyle(StNormalBold).SetKeepWithNext(true));
                        var paymentsTable = new Table(new float[] { 1, 5, 2 });
                        paymentsTable.SetFixedLayout();
                        paymentsTable.UseAllAvailableWidth();
                        foreach (var payment in paymentsGroup.OrderByDescending(p => p.Date))
                        {
                            paymentsTable.AddCell(new Cell().Add(new Paragraph(payment.Date.ToString("dd/MM/yyyy")).AddStyle(StSmall)).SetBorder(Border.NO_BORDER));
                            paymentsTable.AddCell(new Cell().Add(new Paragraph($"{payment.PaymentName}. {payment.AdditionalInformation}").AddStyle(StSmall)).SetBorder(Border.NO_BORDER));
                            paymentsTable.AddCell(new Cell().Add(new Paragraph($"{payment.TotalAmount:N2} {payment.CurrencySymbol}").AddStyle(StSmall).SetTextAlignment(TextAlignment.RIGHT)).SetBorder(Border.NO_BORDER));
                        }
                        foreach (var currencySymbol in paymentsGroup.Select(p => p.CurrencySymbol).Distinct())
                        {
                            paymentsTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                            paymentsTable.AddCell(new Cell().Add(new Paragraph($"Total ({currencySymbol})").AddStyle(StSmallBold)).SetBorder(Border.NO_BORDER));
                            paymentsTable.AddCell(new Cell().Add(new Paragraph($"{paymentsGroup.Where(p => p.CurrencySymbol == currencySymbol).Sum(p => p.TotalAmount):N2} {currencySymbol}").AddStyle(StSmallBold)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
                        }
                        document.Add(paymentsTable);
                        var thinSeparator = new LineSeparator(thinLine);
                        thinSeparator.SetKeepWithNext(true);
                        document.Add(thinSeparator);
                    }
                    var separator = new LineSeparator(line);
                    separator.SetKeepWithNext(true);
                    document.Add(separator);
                }
                var totalsTable = new Table(new float[] { 1, 1 });
                totalsTable.SetFixedLayout();
                totalsTable.UseAllAvailableWidth();
                totalsTable.SetKeepTogether(true);
                totalsTable.SetMarginTop(20);
                foreach (var currencySymbol in payments.Select(p => p.CurrencySymbol).Distinct())
                {
                    totalsTable.AddCell(new Cell().Add(new Paragraph($"Total general ({currencySymbol})").AddStyle(StSmallBold)).SetBorder(Border.NO_BORDER));
                    totalsTable.AddCell(new Cell().Add(new Paragraph($"{payments.Where(p => p.CurrencySymbol == currencySymbol).Sum(p => p.TotalAmount):N2} {currencySymbol}").AddStyle(StSmallBold)).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
                }
                document.Add(totalsTable);
                document.Close();
            }
        }

        /// <summary>
        /// Exporta informe técnico en formato PDF.
        /// </summary>
        /// <param name="report">Informe a exportar.</param>
        /// <param name="business">Empresa asociada al informe.</param>
        /// <param name="customer">Cliente asociado al informe.</param>
        /// <param name="contact">Contacto del cliente.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        public static void ExportPdfTechReport(TechReport report, Business business, Customer customer, CustomerContact contact, string pdfPath)
        {
            Style StNormal = GetStyle(12, "Helvetica", ColorConstants.BLACK, false);
            Style StNormalBold = GetStyle(12, "Helvetica", ColorConstants.BLACK, true);
            var baseWriter = new PdfWriter(pdfPath,
                    new WriterProperties().SetStandardEncryption(null, System.Text.Encoding.UTF8.GetBytes(PdfEncryptionPassword), EncryptionConstants.ALLOW_PRINTING,
                    EncryptionConstants.ENCRYPTION_AES_128 | EncryptionConstants.DO_NOT_ENCRYPT_METADATA));
            var baseDocument = new PdfDocument(baseWriter);
            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(20, 20, 20, 20);
                #region Encabezado
                var businessLogo = new Image(ImageDataFactory.Create(business.BusinessLogo));
                businessLogo.ScaleAbsolute(245F, 144F);
                document.Add(businessLogo);
                var canvas = new PdfCanvas(baseDocument.GetPage(1));
                // Dibuja rectángulo de información del informe.
                canvas
                    .SetFillColor(new DeviceRgb(217, 226, 243))
                    .RoundRectangle(305, 755, 270, 67, 20)
                    .Fill();
                // Dibuja rectángulo de información del cliente.
                canvas
                    .RoundRectangle(20, 580, 555, 90, 20)
                    .Stroke();
                string formattedAddress = string.IsNullOrWhiteSpace(customer.Address) ? "< No registrado >" : $"{customer.Address}, {customer.City}, {customer.District}, {customer.Country}";
                document.Add(new Paragraph("Informe técnico N°:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 790, 200));
                document.Add(new Paragraph("Fecha:").AddStyle(StNormalBold).SetFontSize(13).SetFixedPosition(325, 765, 200));
                document.Add(new Paragraph(report.TechReportID.ToString("D8")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 790, 200));
                document.Add(new Paragraph(report.Date.ToString("dd/MM/yy")).AddStyle(StNormal).SetFontSize(13).SetFixedPosition(480, 765, 200));
                document.Add(new Paragraph("Cliente:").AddStyle(StNormalBold).SetFixedPosition(40, 640, 100));
                document.Add(new Paragraph("Atención:").AddStyle(StNormalBold).SetFixedPosition(40, 615, 100));
                document.Add(new Paragraph("Dirección:").AddStyle(StNormalBold).SetFixedPosition(40, 590, 100));
                document.Add(new Paragraph("Teléfono:").AddStyle(StNormalBold).SetFixedPosition(300, 640, 100));
                document.Add(new Paragraph("Correo:").AddStyle(StNormalBold).SetFixedPosition(300, 615, 100));
                document.Add(new Paragraph($"{customer.CustomerName} ({customer.IdentityNumber})").AddStyle(StNormal).SetFixedPosition(100, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Greeting).AddStyle(StNormal).SetFixedPosition(100, 603, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(formattedAddress).AddStyle(StNormal).SetFixedPosition(100, 578, 460).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Phone).AddStyle(StNormal).SetFixedPosition(360, 628, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph(contact.Email).AddStyle(StNormal).SetFixedPosition(360, 603, 200).SetFixedLeading(10).SetHeight(25));
                document.Add(new Paragraph().SetMarginTop(115).Add(new Text("INFORME TÉCNICO").AddStyle(StNormalBold)).SetTextAlignment(TextAlignment.CENTER));
                canvas.ResetFillColorRgb();
                #endregion
                #region Cuerpo HTML
                var properties = new ConverterProperties();
                var fontProvider = new FontProvider();
                fontProvider.AddStandardPdfFonts();
                properties.SetFontProvider(fontProvider);
                var elements = HtmlConverter.ConvertToElements(report.ReportBody, properties);
                foreach (var element in elements.OfType<IBlockElement>())
                {
                    document.Add(element);
                }
                #endregion
                document.Close();
            }
        }

        /// <summary>
        /// Exporta una orden de reparación en formato PDF. 
        /// </summary>
        /// <param name="order">Orden de reparación a exportar.</param>
        /// <param name="pdfPath">Ruta de destino del documento.</param>
        public static void ExportPdfRepairOrder(RepairOrder order, string pdfPath)
        {
            Style StNormal = GetStyle(11, "Helvetica", ColorConstants.BLACK, false);
            Style StNormalBold = GetStyle(11, "Helvetica", ColorConstants.BLACK, true);

            var baseWriter = new PdfWriter(pdfPath);
            var baseDocument = new PdfDocument(baseWriter);

            // Agrega un proveedor de fuentes para evitar errores de carga de fuentes
            var fontProvider = new FontProvider();
            fontProvider.AddStandardPdfFonts(); // Agrega las fuentes estándar

            using (var document = new Document(baseDocument, iText.Kernel.Geom.PageSize.A4))
            {
                document.SetMargins(40, 40, 40, 40);
                document.SetFontProvider(fontProvider); // Aplica el proveedor de fuentes

                var table1 = new Table(new float[] { 1, 1 });
                table1.SetFixedLayout();
                table1.UseAllAvailableWidth();
                table1.SetBackgroundColor(ColorConstants.GRAY, 0.25F, 10, 10, 10, 10);
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("ID Orden de reparación: ").AddStyle(StNormalBold)).Add(new Text(order.RepairOrderID.ToString("D8")).AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("N° de referencia: ").AddStyle(StNormalBold)).Add(new Text(order.RefNumber ?? "<>").AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("Fecha: ").AddStyle(StNormalBold)).Add(new Text(order.Date.ToString("dd/MM/yyyy")).AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("Cliente: ").AddStyle(StNormalBold)).Add(new Text(order.CustomerName ?? "<>").AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("Teléfono: ").AddStyle(StNormalBold)).Add(new Text(order.PhoneNumber ?? "<>").AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("N° de remito recepción: ").AddStyle(StNormalBold)).Add(new Text(order.DeliveryNoteNumberCustomer ?? "<>").AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("N° de remito entrega: ").AddStyle(StNormalBold)).Add(new Text(order.DeliveryNoteNumber ?? "<>").AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                table1.AddCell(new Cell().Add(new Paragraph().Add(new Text("N° de factura: ").AddStyle(StNormalBold)).Add(new Text(order.InvoiceNumber ?? "<>").AddStyle(StNormal))).SetBorder(Border.NO_BORDER));
                document.Add(table1);

                var table2 = new Table(new float[] { 1, 2 });
                table2.SetFixedLayout();
                table2.UseAllAvailableWidth();
                table2.SetMarginTop(20);
                table2.AddCell(new Cell().Add(new Paragraph("Tipo de equipo:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.MotorTypeName ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Marca:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.PumpBrand ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Modelo:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.PumpModel ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Tipo motor:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.IsSinglePhase ? "Monofásico" : "Trifásico").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Bobinado:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.AppliesForWinding ? (order.RequiresWinding ? "Si" : "No") : "N/A").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Potencia:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.EnginePower != null ? (order.EnginePower + " HP") : "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Rodamientos:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.Bearings ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Arenado:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresSandblast ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Retenes:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.Locks ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Pintura:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.PaintColor ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Sellos mecánicos:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.SealsApply ? (order.RequiresNewSeals ? "Nuevos" : "Reparar") : "N/A").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Modelos sellos:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.Seals ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Capacitor nuevo:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresNewCapacitor ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Voltaje:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.CapacitorVoltage.HasValue ? (order.CapacitorVoltage.Value + " V") : "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Capacitancia:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.CapacitorCapacity.HasValue ? (order.CapacitorCapacity.Value + " uF") : "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Encamisado eje:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresShaftCladding ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Fabricación eje nuevo:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresShaftManufacturing ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Largo cable:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.CableLenght.HasValue ? (order.CableLenght + " m") : "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Medida cable:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.CableSize ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Kit de O'rings:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresOrings ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Juntas:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresJoints ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Aceite dieléctrico:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresDielectricOil ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Impulsor nuevo:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresNewImpeller ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Chavetas:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresPins ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Empaquetaduras:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresPackings ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Grasa caja rulemanes:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.RequiresGrease ? "Si" : "No").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Faltantes:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.MissingParts ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Otros:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.Notes ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph("Ubicación:").AddStyle(StNormalBold)).SetBorder(Border.NO_BORDER));
                table2.AddCell(new Cell().Add(new Paragraph(order.StorageLocation ?? "<>").AddStyle(StNormal)).SetBorder(Border.NO_BORDER));

                document.Add(table2);

                document.Close();
            }
        }

        private static Style GetStyle(float size, string fontName, Color color, bool bold)
        {
            var style = new Style();
            style.SetFontSize(size);

            try
            {
                // Usar una fuente estándar de iText7
                var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
                style.SetFont(font);
            }
            catch (Exception ex)
            {
                // Manejo de errores de fuentes
                throw new Exception("Error cargando la fuente: " + ex.Message);
            }

            style.SetFontColor(color);

            if (bold)
            {
                style.SetBold();
            }

            return style;
        }




    }
}