using Clover.DbLayer;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace Clover.Gestion
{
    public static class ExcelGeneration
    {
        /// <summary>
        /// Exporta una o mas tablas en formato Excel.
        /// </summary>
        /// <param name="excelPath">Ruta de destino del documento.</param>
        /// <param name="dataTables">Tablas con la información a exportar.</param>
        public static void ExportExcelDataTable(string excelPath, params DataTable[] dataTables)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Informe");
                int lastTableRow = 0;
                foreach (var dataTable in dataTables)
                {
                    // Encabezado
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        worksheet.Cells[lastTableRow + 1, i + 1].Value = dataTable.Columns[i].Caption;
                    }
                    var headerRange = worksheet.Cells[lastTableRow + 1, 1, lastTableRow + 1, dataTable.Columns.Count];
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(255, 220, 230, 240);
                    // Filas
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataTable.Columns.Count; j++)
                        {
                            object value = dataTable.Rows[i][j];
                            worksheet.Cells[lastTableRow + i + 2, j + 1].Value = value;

                            switch (value)
                            {
                                case DateTime _:
                                    worksheet.Cells[lastTableRow + i + 2, j + 1].Style.Numberformat.Format = "dd/MM/yyyy";
                                    break;
                                case decimal _:
                                    worksheet.Cells[lastTableRow + i + 2, j + 1].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[lastTableRow + i + 2, j + 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    break;
                            }
                        }
                    }
                    lastTableRow += dataTable.Rows.Count + 2;
                }
                worksheet.Cells.AutoFitColumns();
                package.SaveAs(new FileInfo(excelPath));
            }
        }

        /// <summary>
        /// Exporta resumen de órdenes de pago en formato Excel.
        /// </summary>
        /// <param name="excelPath">Ruta de destino del documento.</param>
        /// <param name="payments">Pagos a exportar.</param>
        public static void ExportExcelPayOrdersReport(string excelPath, List<PayOrderPayment> payments)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Informe");
                int rowCount = 1;
                foreach (var ordersGroup in payments.GroupBy(p => p.ProviderName).OrderBy(g => g.Key))
                {
                    worksheet.Cells[rowCount, 1, rowCount, 4].Merge = true;
                    worksheet.Cells[rowCount, 1].Value = ordersGroup.Key;
                    worksheet.Cells[rowCount, 1].Style.Font.Bold = true;
                    worksheet.Cells[rowCount, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[rowCount, 1].Style.Fill.BackgroundColor.SetColor(255, 220, 230, 240);
                    rowCount++;
                    foreach (var paymentsGroup in ordersGroup.GroupBy(g => g.PayOrderID))
                    {
                        worksheet.Cells[rowCount, 1, rowCount, 4].Merge = true;
                        worksheet.Cells[rowCount, 1].Value = $"Orden de pago N°: {paymentsGroup.Key:D8}";
                        worksheet.Cells[rowCount, 1].Style.Font.Bold = true;
                        rowCount++;
                        foreach (var payment in paymentsGroup.OrderByDescending(p => p.Date))
                        {
                            worksheet.Cells[rowCount, 1].Value = payment.Date;
                            worksheet.Cells[rowCount, 1].Style.Numberformat.Format = "dd/MM/yyyy";
                            worksheet.Cells[rowCount, 2].Value = $"{payment.PaymentName}. {payment.AdditionalInformation}";
                            worksheet.Cells[rowCount, 3].Value = payment.TotalAmount;
                            worksheet.Cells[rowCount, 3].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[rowCount, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                            worksheet.Cells[rowCount, 4].Value = payment.CurrencySymbol;
                            rowCount++;
                        }
                        foreach (var currencySymbol in paymentsGroup.Select(p => p.CurrencySymbol).Distinct())
                        {
                            worksheet.Cells[rowCount, 2].Value = $"Total ({currencySymbol})";
                            worksheet.Cells[rowCount, 2].Style.Font.Bold = true;
                            worksheet.Cells[rowCount, 3].Value = paymentsGroup.Where(p => p.CurrencySymbol == currencySymbol).Sum(p => p.TotalAmount);
                            worksheet.Cells[rowCount, 3].Style.Font.Bold = true;
                            worksheet.Cells[rowCount, 3].Style.Numberformat.Format = "#,##0.00";
                            worksheet.Cells[rowCount, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                            worksheet.Cells[rowCount, 4].Value = currencySymbol;
                            worksheet.Cells[rowCount, 4].Style.Font.Bold = true;
                            rowCount++;
                        }
                    }
                }
                rowCount++;
                foreach (var currencySymbol in payments.Select(p => p.CurrencySymbol).Distinct())
                {
                    worksheet.Cells[rowCount, 2].Value = $"Total general ({currencySymbol})";
                    worksheet.Cells[rowCount, 2].Style.Font.Bold = true;
                    worksheet.Cells[rowCount, 3].Value = payments.Where(p => p.CurrencySymbol == currencySymbol).Sum(p => p.TotalAmount);
                    worksheet.Cells[rowCount, 3].Style.Font.Bold = true;
                    worksheet.Cells[rowCount, 3].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[rowCount, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                    worksheet.Cells[rowCount, 4].Value = currencySymbol;
                    worksheet.Cells[rowCount, 4].Style.Font.Bold = true;
                    rowCount++;
                }
                worksheet.Cells.AutoFitColumns();
                package.SaveAs(new FileInfo(excelPath));
            }
        }
    }
}
