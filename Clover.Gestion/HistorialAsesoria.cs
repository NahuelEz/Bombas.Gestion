using System;
using System.Data;
using Clover.DbLayer;
using MySql.Data.MySqlClient;

namespace Clover.Gestion
{
    internal static class HistorialAsesoriaDb
    {
        // Método para obtener el historial de asesorías de un lead específico
        public static DataTable ObtenerHistorialAsesoria(int leadID)
        {
            string query = "SELECT * FROM HistorialAsesoria WHERE LeadID = @LeadID ORDER BY Fecha DESC";
            DataTable historial = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadID);
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(historial);
                    }
                }
            }

            return historial;
        }

        // Método para insertar una nueva asesoría en el historial
        public static void InsertarAsesoria(int leadID, string asesor, string descripcion)
        {
            string query = @"INSERT INTO HistorialAsesoria (LeadID, Fecha, Asesor, Descripcion) 
                             VALUES (@LeadID, @Fecha, @Asesor, @Descripcion)";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", leadID);
                    cmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Asesor", asesor);
                    cmd.Parameters.AddWithValue("@Descripcion", descripcion);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
