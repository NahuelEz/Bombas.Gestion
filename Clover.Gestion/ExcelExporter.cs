using OfficeOpenXml;
using System.Data;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public static class ExcelExporter
    {
        public static void ExportarDataGridViewAExcel(DataGridView dgv, string filePath)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Informe");

                // Encabezados
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1].Value = dgv.Columns[i].HeaderText;
                }

                // Filas
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.ColumnCount; j++)
                    {
                        worksheet.Cells[i + 2, j + 1].Value = dgv.Rows[i].Cells[j].Value?.ToString() ?? string.Empty;
                    }
                }

                package.SaveAs(new System.IO.FileInfo(filePath));
            }
        }
    }
}
