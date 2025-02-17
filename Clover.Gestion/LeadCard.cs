using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Clover.DbLayer;
using MySql.Data.MySqlClient;

namespace Clover.Gestion
{
    public partial class LeadCard : UserControl
    {
        public int LeadID { get; set; }

        public string LeadName
        {
            get => lblLeadName.Text;
            set => lblLeadName.Text = "Nombre: " + value;
        }

        public string LeadDate
        {
            get => lblLeadStatus.Text;
            set => lblLeadStatus.Text = "Fecha: " + value;
        }

        public LeadCard()
        {
            InitializeComponent();
            toolTip = new ToolTip();


            this.Size = new Size(188, 100);


            this.Paint += LeadCard_Paint;


            this.MouseDown += LeadCard_MouseDown;

            ConfigurarBotones();
        }

        private ToolTip toolTip;

        private void ConfigurarBotones()
        {
            Button btnEditar = CrearBoton("Editar", @"\Resources\lapiz.png", new Point(5, this.Height - 45), BtnEditar_Click, "Editar Lead");
            Button btnDelegar = CrearBoton("Delegar", @"\Resources\delegart.png", new Point(40, this.Height - 45), BtnDelegar_Click, "Delegar");
            Button btnCasilla = CrearBoton("Casilla", @"\Resources\evaluacion.png", new Point(75, this.Height - 45), BtnCasilla_Click, "Calificar");
            Button btnHistorialCompras = CrearBoton("Historial", @"\Resources\historial-de-compras.png", new Point(110, this.Height - 45), BtnHistorialCompras_Click, "Historial de Compras");
            Button btnCerrarLead = CrearBoton("Cerrar", @"\Resources\cierre.png", new Point(145, this.Height - 45), BtnCerrarLead_Click, "Cerrar Lead");

            this.Controls.Add(btnEditar);
            this.Controls.Add(btnDelegar);
            this.Controls.Add(btnCasilla);
            this.Controls.Add(btnHistorialCompras);
            this.Controls.Add(btnCerrarLead);
        }

        private Button CrearBoton(string name, string imagePath, Point location, EventHandler clickEvent, string toolTipText)
        {
            Button button = new Button
            {
                Name = "btn" + name,
                Width = 30,
                Height = 30,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                Location = location,
                Image = Image.FromFile(Application.StartupPath + imagePath),
                ImageAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button.Click += clickEvent;
            toolTip.SetToolTip(button, toolTipText);

            return button;
        }

        private void LeadCard_Paint(object sender, PaintEventArgs e)
        {
            int cornerRadius = 15;
            GraphicsPath path = CreateRoundedRectanglePath(this.ClientRectangle, cornerRadius);


            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (Brush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }


            using (Pen pen = new Pen(Color.Black, 2))
            {
                e.Graphics.DrawPath(pen, path);
            }


            Region roundedRegion = new Region(CreateRoundedRectanglePath(new Rectangle(0, 0, this.Width, this.Height), cornerRadius));
            this.Region = roundedRegion;
        }

        private GraphicsPath CreateRoundedRectanglePath(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void LeadCard_MouseDown(object sender, MouseEventArgs e)
        {
            DoDragDrop(this, DragDropEffects.Move);
        }

        private void BtnCerrarLead_Click(object sender, EventArgs e)
        {
            using (CierreForm form = new CierreForm(LeadID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Lead cerrado correctamente.", "Cierre", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Parent.Controls.Remove(this);
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            using (LeadForm form = new LeadForm(LeadID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RecargarDatos();
                }
            }
        }

        private void BtnDelegar_Click(object sender, EventArgs e)
        {
            using (DelegarForm form = new DelegarForm(LeadID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Lead delegado correctamente.", "Delegación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnCasilla_Click(object sender, EventArgs e)
        {
            using (CalificacionDetalleForm form = new CalificacionDetalleForm(LeadID))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    RecargarDatos();
                }
            }
        }

        private void BtnHistorialCompras_Click(object sender, EventArgs e)
        {
            using (HistorialComprasForm form = new HistorialComprasForm(LeadID))
            {
                form.ShowDialog();
            }
        }

        public void RecargarDatos()
        {
            string query = "SELECT Nombre, NivelUrgencia, DATE_FORMAT(FechaCreacion, '%d/%m/%Y') AS FechaCreacion FROM Leads WHERE LeadID = @LeadID";

            using (MySqlConnection conn = new MySqlConnection(DbLayerSettings.ConnectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LeadID", LeadID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LeadName = reader["Nombre"].ToString();
                            LeadDate = reader["FechaCreacion"].ToString();
                            AplicarColorPorNivelUrgencia(reader["NivelUrgencia"].ToString());
                        }
                    }
                }
            }
        }

        public void AplicarColorPorNivelUrgencia(string nivelUrgencia)
        {
            int transparencia = 130;
            switch (nivelUrgencia.ToUpper())
            {
                case "ALTO":
                    this.BackColor = Color.FromArgb(transparencia, Color.Red);
                    break;
                case "MEDIO":
                    this.BackColor = Color.FromArgb(transparencia, Color.Orange);
                    break;
                case "BAJO":
                    this.BackColor = Color.FromArgb(transparencia, Color.LightGreen);
                    break;
                default:
                    this.BackColor = Color.FromArgb(transparencia, Color.Gray);
                    break;
            }


            this.Invalidate();
        }

    }
}