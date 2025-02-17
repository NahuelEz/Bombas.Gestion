using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Clover.HtmlEditor
{
    public partial class HtmlEditor : UserControl
    {
        private dynamic document;

        public HtmlEditor()
        {
            // Inicializa UI.
            InitializeComponent();
            // Pone documento en modo edición.
            wbrHtmlBox.DocumentText = "<HTML><BODY></BODY></HTML>";
            document = wbrHtmlBox.Document.DomDocument;
            document.designMode = "On";
            Application.DoEvents();
            // Formato predefinido.
            wbrHtmlBox.Document.ExecCommand("FontName", false, "Arial");
            wbrHtmlBox.Document.ExecCommand("FontSize", false, 3);
        }

        public string GetDocumentHtml()
        {
            return wbrHtmlBox.DocumentText;
        }
        public void SetDocumentHtml(string input)
        {
            wbrHtmlBox.Document.OpenNew(true);
            wbrHtmlBox.Document.Write(input);
            document = wbrHtmlBox.Document.DomDocument;
            document.designMode = "On";
        }
        
        private void btnFontFamily_Click(object sender, EventArgs e)
        {
            using (var dialog = new FontSelector())
            {
                // Determina fuente actual.
                string font = Convert.ToString(document.queryCommandValue("FontName"));
                string size = Convert.ToString(document.queryCommandValue("FontSize"));
                if (!string.IsNullOrWhiteSpace(font) && !string.IsNullOrWhiteSpace(size))
                {
                    dialog.SelectedFontFamily = font;
                    dialog.SelectedFontSize = Convert.ToInt32(size);
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    wbrHtmlBox.Document.ExecCommand("FontName", false, dialog.SelectedFontFamily);
                    wbrHtmlBox.Document.ExecCommand("FontSize", false, dialog.SelectedFontSize);
                }
            }
        }
        private void btnBold_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("Bold", false, null);
        }
        private void btnItalic_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("Italic", false, null);
        }
        private void btnUnderline_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("Underline", false, null);
        }
        private void btnSuperscript_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("Superscript", false, null);
        }
        private void btnSubscript_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("Subscript", false, null);
        }
        private void btnFontColor_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                // Determina color actual.
                string color = Convert.ToString(document.queryCommandValue("ForeColor"));
                if (!string.IsNullOrWhiteSpace(color))
                {
                    dialog.Color = ColorTranslator.FromOle(Convert.ToInt32(color));
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    color = string.Format("#{0:X2}{1:X2}{2:X2}", dialog.Color.R, dialog.Color.G, dialog.Color.B);
                    wbrHtmlBox.Document.ExecCommand("ForeColor", false, color);
                }
            }
        }
        private void btnBackColor_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                // Determina color actual.
                string color = Convert.ToString(document.queryCommandValue("BackColor"));
                if (!string.IsNullOrWhiteSpace(color))
                {
                    dialog.Color = ColorTranslator.FromOle(Convert.ToInt32(color));
                }
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    color = string.Format("#{0:X2}{1:X2}{2:X2}", dialog.Color.R, dialog.Color.G, dialog.Color.B);
                    wbrHtmlBox.Document.ExecCommand("BackColor", false, color);
                }
            }
        }
        private void btnJustifyLeft_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("JustifyLeft", false, null);
        }
        private void btnJustifyCenter_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("JustifyCenter", false, null);
        }
        private void btnJustifyRight_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("JustifyRight", false, null);
        }
        private void btnOrderedList_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("InsertOrderedList", false, null);
        }
        private void btnInsertUnorderedList_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("InsertUnorderedList", false, null);
        }
        private void btnCopy_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("Copy", false, null);
        }
        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                var dataObject = Clipboard.GetDataObject();
                if (dataObject == null)
                {
                    // Portapapeles vacío.
                    return;
                }
                if (dataObject.GetDataPresent(DataFormats.FileDrop))
                {
                    var filenames = (string[])dataObject.GetData(DataFormats.FileDrop);
                    if (filenames.Length > 0)
                    {
                        string filename = filenames[0];
                        var fi = new FileInfo(filename);
                        string[] allowedExtensions = { ".bmp", ".png", ".jpg" };
                        if (fi.Exists && allowedExtensions.Contains(fi.Extension.ToLower()))
                        {
                            // El archivo existe y es una imagen.
                            using (var image = Image.FromFile(filename))
                            {
                                using (var reducedImage = ReduceImageSize(image, 300))
                                {
                                    var imageBytes = (byte[])new ImageConverter().ConvertTo(reducedImage, typeof(byte[]));
                                    var src = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                                    wbrHtmlBox.Document.ExecCommand("InsertImage", false, src);
                                }
                            }
                        }
                    }
                }
                else if (dataObject.GetDataPresent(DataFormats.Bitmap))
                {
                    using (var image = (Bitmap)dataObject.GetData(DataFormats.Bitmap))
                    {
                        using (var reducedImage = ReduceImageSize(image, 300))
                        {
                            var imageBytes = (byte[])new ImageConverter().ConvertTo(reducedImage, typeof(byte[]));
                            var src = $"data:image/png;base64,{Convert.ToBase64String(imageBytes)}";
                            wbrHtmlBox.Document.ExecCommand("InsertImage", false, src);
                        }
                    }
                }
                else
                {
                    wbrHtmlBox.Document.ExecCommand("Paste", false, null);
                }
            }
            catch
            {
                MessageBox.Show("Error al pegar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCut_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("Cut", false, null);
        }
        private void btnInsertLine_Click(object sender, EventArgs e)
        {
            wbrHtmlBox.Document.ExecCommand("InsertHorizontalRule", false, null);
        }

        private void wbrHtmlBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.X:
                        {
                            btnCut.PerformClick();
                            break;
                        }
                    case Keys.C:
                        {
                            btnCopy.PerformClick();
                            break;
                        }
                    case Keys.V:
                        {
                            btnPaste.PerformClick();
                            break;
                        }
                }
            }
        }
        
        // Funciones para manejo de imágenes.
        private Image ReduceImageSize(Image image, int maxDimension)
        {
            if (image.Size.Height <= maxDimension && image.Size.Width <= maxDimension)
            {
                return image;
            }
            else
            {
                // Lógica para determinar el nuevo tamaño manteniendo relación de aspecto.
                double ratio = image.Width / image.Height;
                int destWidth;
                int destHeight;
                if (ratio > 1)
                {
                    destWidth = maxDimension;
                    destHeight = (int)Math.Round(maxDimension / ratio, 0);
                }
                else if (ratio < 1)
                {
                    destHeight = maxDimension;
                    destWidth = (int)Math.Round(maxDimension * ratio, 0);
                }
                else
                {
                    destWidth = maxDimension;
                    destHeight = maxDimension;
                }
                return ResizeImage(image, destWidth, destHeight);
            }
        }
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
    }
}