using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using OfficeOpenXml; // Biblioteca EPPlus (para Excel)
using System.IO;
using Clover.DbLayer;

namespace Clover.Gestion
{
    public partial class InformesForm : Form
    {
        public InformesForm()
        {
            InitializeComponent();
            CargarTiposDeInforme();
        }

        // Carga los tipos de informe en el ComboBox
        private void CargarTiposDeInforme()
        {
            cmbTipoInforme.Items.Add("Leads");
            cmbTipoInforme.Items.Add("Asesorías");
            cmbTipoInforme.Items.Add("Negociaciones");
            cmbTipoInforme.Items.Add("Cierres");
            cmbTipoInforme.SelectedIndex = 0; // Seleccionar el primer elemento
        }

        // Evento para generar informe
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            string tipoInforme = cmbTipoInforme.SelectedItem.ToString();
            DateTime desde = dtpDesde.Value.Date;
            DateTime hasta = dtpHasta.Value.Date;

            GenerarInforme(tipoInforme, desde, hasta);
        }

        private void GenerarInforme(string tipoInforme, DateTime desde, DateTime hasta)
        {
            string query = string.Empty;

            switch (tipoInforme)
            {
                case "Leads":
                    query = "SELECT * FROM Leads WHERE FechaCreacion BETWEEN @Desde AND @Hasta";
                    break;
                case "Asesorías":
                    query = "SELECT * FROM Leads WHERE Estado = 'Asesoría' AND FechaCreacion BETWEEN @Desde AND @Hasta";
                    break;
                case "Negociaciones":
                    query = "SELECT * FROM Leads WHERE Estado = 'Negociación' AND FechaCreacion BETWEEN @Desde AND @Hasta";
                    break;
                case "Cierres":
                    query = "SELECT * FROM Leads WHERE Estado = 'Cierre' AND FechaCreacion BETWEEN @Desde AND @Hasta";
                    break;
                default:
                    MessageBox.Show("Tipo de informe no válido.");
                    return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Desde", desde);
                        cmd.Parameters.AddWithValue("@Hasta", hasta);

                        DataTable dataTable = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }

                        dgvResultados.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el informe: " + ex.Message);
            }
        }


        // Evento para exportar a PDF
        private void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = "Informe.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    PdfExporter.ExportarDataGridViewAPdf(dgvResultados, path); // Llama a la clase PdfExporter
                    MessageBox.Show("Informe exportado correctamente a PDF.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar el informe a PDF: " + ex.Message);
            }
        }

        // Evento para exportar a Excel
        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    FileName = "Informe.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = saveFileDialog.FileName;
                    ExcelExporter.ExportarDataGridViewAExcel(dgvResultados, path); // Llama a la clase ExcelExporter
                    MessageBox.Show("Informe exportado correctamente a Excel.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar el informe a Excel: " + ex.Message);
            }
        }
    }
}