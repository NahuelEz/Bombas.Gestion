using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Data;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public static class PdfExporter
    {
        public static void ExportarDataGridViewAPdf(DataGridView dgv, string filePath)
        {
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);

                    // Título del documento
                    document.Add(new Paragraph("Informe Generado").SetBold().SetFontSize(16));

                    // Tabla para los datos
                    Table table = new Table(dgv.ColumnCount);

                    // Encabezados
                    foreach (DataGridViewColumn column in dgv.Columns)
                    {
                        table.AddHeaderCell(new Cell().Add(new Paragraph(column.HeaderText).SetBold()));
                    }

                    // Filas
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                table.AddCell(new Paragraph(cell.Value?.ToString() ?? string.Empty));
                            }
                        }
                    }

                    // Agregar tabla al documento
                    document.Add(table);
                    document.Close();
                }
            }
        }
    }
}
