using Clover.DbLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clover.Gestion
{
    public partial class FormCreateUser : Form
    {
        public FormCreateUser()
        {
            InitializeComponent();
            LoadAccessLevels();
            LoadDepartments();
        }

        // Método para cargar los niveles de acceso al ComboBox
        private void LoadAccessLevels()
        {
            cboAccessLevel.Items.Add(new { Text = "Admin", Value = 1 });
            cboAccessLevel.Items.Add(new { Text = "Manager", Value = 2 });
            cboAccessLevel.Items.Add(new { Text = "Supervisor", Value = 3 });
            cboAccessLevel.Items.Add(new { Text = "Employee", Value = 4 });
            cboAccessLevel.Items.Add(new { Text = "Guest", Value = 5 });

            cboAccessLevel.DisplayMember = "Text";
            cboAccessLevel.ValueMember = "Value";
            cboAccessLevel.SelectedIndex = 0; // Selecciona el primer elemento por defecto
        }

        // Método para cargar los departamentos al ComboBox
        private void LoadDepartments()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT DepartmentID FROM user";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int departmentID = reader.GetInt32("DepartmentID");
                        cboDepartmentID.Items.Add(new { Text = departmentID.ToString(), Value = departmentID });
                    }

                    // Set the DisplayMember and ValueMember for the ComboBox
                    cboDepartmentID.DisplayMember = "Text";
                    cboDepartmentID.ValueMember = "Value";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los departamentos: " + ex.Message);
            }
        }

        // Evento que se dispara al hacer clic en el botón de crear usuario
        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            int accessLevel = (int)((dynamic)cboAccessLevel.SelectedItem).Value;
            int departmentID = (int)((dynamic)cboDepartmentID.SelectedItem).Value;
            string notifications = txtNotifications.Text.Trim();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "El nombre de usuario y la contraseña no pueden estar vacíos.";
                return;
            }

            string encodedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));

            try
            {
                using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO user (UserName, Password, AccessLevel, DepartmentID, Notifications) VALUES (@UserName, @Password, @AccessLevel, @DepartmentID, @Notifications)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", encodedPassword);
                    cmd.Parameters.AddWithValue("@AccessLevel", accessLevel);
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);
                    cmd.Parameters.AddWithValue("@Notifications", notifications);
                    cmd.ExecuteNonQuery();
                }

                lblMessage.Text = "Usuario creado correctamente.";
                txtUserName.Clear();
                txtPassword.Clear();
                txtNotifications.Clear();
                cboAccessLevel.SelectedIndex = 0;
                cboDepartmentID.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al crear el usuario: " + ex.Message;
            }
        }
    }
}
