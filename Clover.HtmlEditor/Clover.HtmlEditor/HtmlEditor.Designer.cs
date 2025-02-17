namespace Clover.HtmlEditor
{
    partial class HtmlEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HtmlEditor));
            this.spcMainContainer = new System.Windows.Forms.SplitContainer();
            this.tspCommands = new System.Windows.Forms.ToolStrip();
            this.btnFontFamily = new System.Windows.Forms.ToolStripButton();
            this.btnBold = new System.Windows.Forms.ToolStripButton();
            this.btnItalic = new System.Windows.Forms.ToolStripButton();
            this.btnUnderline = new System.Windows.Forms.ToolStripButton();
            this.btnSuperscript = new System.Windows.Forms.ToolStripButton();
            this.btnSubscript = new System.Windows.Forms.ToolStripButton();
            this.btnFontColor = new System.Windows.Forms.ToolStripButton();
            this.btnBackColor = new System.Windows.Forms.ToolStripButton();
            this.btnJustifyLeft = new System.Windows.Forms.ToolStripButton();
            this.btnJustifyCenter = new System.Windows.Forms.ToolStripButton();
            this.btnJustifyRight = new System.Windows.Forms.ToolStripButton();
            this.btnOrderedList = new System.Windows.Forms.ToolStripButton();
            this.btnInsertUnorderedList = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnInsertLine = new System.Windows.Forms.ToolStripButton();
            this.wbrHtmlBox = new System.Windows.Forms.WebBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.spcMainContainer)).BeginInit();
            this.spcMainContainer.Panel1.SuspendLayout();
            this.spcMainContainer.Panel2.SuspendLayout();
            this.spcMainContainer.SuspendLayout();
            this.tspCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // spcMainContainer
            // 
            this.spcMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spcMainContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spcMainContainer.IsSplitterFixed = true;
            this.spcMainContainer.Location = new System.Drawing.Point(0, 0);
            this.spcMainContainer.Name = "spcMainContainer";
            this.spcMainContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spcMainContainer.Panel1
            // 
            this.spcMainContainer.Panel1.Controls.Add(this.tspCommands);
            // 
            // spcMainContainer.Panel2
            // 
            this.spcMainContainer.Panel2.Controls.Add(this.wbrHtmlBox);
            this.spcMainContainer.Size = new System.Drawing.Size(500, 300);
            this.spcMainContainer.SplitterDistance = 30;
            this.spcMainContainer.TabIndex = 0;
            // 
            // tspCommands
            // 
            this.tspCommands.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tspCommands.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnFontFamily,
            this.btnBold,
            this.btnItalic,
            this.btnUnderline,
            this.btnSuperscript,
            this.btnSubscript,
            this.btnFontColor,
            this.btnBackColor,
            this.btnJustifyLeft,
            this.btnJustifyCenter,
            this.btnJustifyRight,
            this.btnOrderedList,
            this.btnInsertUnorderedList,
            this.btnCopy,
            this.btnPaste,
            this.btnCut,
            this.btnInsertLine});
            this.tspCommands.Location = new System.Drawing.Point(0, 0);
            this.tspCommands.Name = "tspCommands";
            this.tspCommands.Size = new System.Drawing.Size(500, 30);
            this.tspCommands.TabIndex = 0;
            this.tspCommands.Text = "Barra de comandos";
            // 
            // btnFontFamily
            // 
            this.btnFontFamily.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFontFamily.Image = ((System.Drawing.Image)(resources.GetObject("btnFontFamily.Image")));
            this.btnFontFamily.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFontFamily.Name = "btnFontFamily";
            this.btnFontFamily.Size = new System.Drawing.Size(23, 27);
            this.btnFontFamily.ToolTipText = "Fuente";
            this.btnFontFamily.Click += new System.EventHandler(this.btnFontFamily_Click);
            // 
            // btnBold
            // 
            this.btnBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBold.Image = ((System.Drawing.Image)(resources.GetObject("btnBold.Image")));
            this.btnBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBold.Name = "btnBold";
            this.btnBold.Size = new System.Drawing.Size(23, 27);
            this.btnBold.ToolTipText = "Negrita";
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnItalic.Image = ((System.Drawing.Image)(resources.GetObject("btnItalic.Image")));
            this.btnItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItalic.Name = "btnItalic";
            this.btnItalic.Size = new System.Drawing.Size(23, 27);
            this.btnItalic.ToolTipText = "Cursiva";
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btnUnderline
            // 
            this.btnUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("btnUnderline.Image")));
            this.btnUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnderline.Name = "btnUnderline";
            this.btnUnderline.Size = new System.Drawing.Size(23, 27);
            this.btnUnderline.ToolTipText = "Subrayado";
            this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
            // 
            // btnSuperscript
            // 
            this.btnSuperscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSuperscript.Image = ((System.Drawing.Image)(resources.GetObject("btnSuperscript.Image")));
            this.btnSuperscript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSuperscript.Name = "btnSuperscript";
            this.btnSuperscript.Size = new System.Drawing.Size(23, 27);
            this.btnSuperscript.ToolTipText = "Superíndice";
            this.btnSuperscript.Click += new System.EventHandler(this.btnSuperscript_Click);
            // 
            // btnSubscript
            // 
            this.btnSubscript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSubscript.Image = ((System.Drawing.Image)(resources.GetObject("btnSubscript.Image")));
            this.btnSubscript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubscript.Name = "btnSubscript";
            this.btnSubscript.Size = new System.Drawing.Size(23, 27);
            this.btnSubscript.ToolTipText = "Subíndice";
            this.btnSubscript.Click += new System.EventHandler(this.btnSubscript_Click);
            // 
            // btnFontColor
            // 
            this.btnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFontColor.Image = ((System.Drawing.Image)(resources.GetObject("btnFontColor.Image")));
            this.btnFontColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(23, 27);
            this.btnFontColor.ToolTipText = "Color del texto";
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBackColor.Image = ((System.Drawing.Image)(resources.GetObject("btnBackColor.Image")));
            this.btnBackColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(23, 27);
            this.btnBackColor.ToolTipText = "Color del fondo";
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // btnJustifyLeft
            // 
            this.btnJustifyLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnJustifyLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnJustifyLeft.Image")));
            this.btnJustifyLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnJustifyLeft.Name = "btnJustifyLeft";
            this.btnJustifyLeft.Size = new System.Drawing.Size(23, 27);
            this.btnJustifyLeft.ToolTipText = "Alinear izquierda";
            this.btnJustifyLeft.Click += new System.EventHandler(this.btnJustifyLeft_Click);
            // 
            // btnJustifyCenter
            // 
            this.btnJustifyCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnJustifyCenter.Image = ((System.Drawing.Image)(resources.GetObject("btnJustifyCenter.Image")));
            this.btnJustifyCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnJustifyCenter.Name = "btnJustifyCenter";
            this.btnJustifyCenter.Size = new System.Drawing.Size(23, 27);
            this.btnJustifyCenter.ToolTipText = "Alinear centrado";
            this.btnJustifyCenter.Click += new System.EventHandler(this.btnJustifyCenter_Click);
            // 
            // btnJustifyRight
            // 
            this.btnJustifyRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnJustifyRight.Image = ((System.Drawing.Image)(resources.GetObject("btnJustifyRight.Image")));
            this.btnJustifyRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnJustifyRight.Name = "btnJustifyRight";
            this.btnJustifyRight.Size = new System.Drawing.Size(23, 27);
            this.btnJustifyRight.ToolTipText = "Alinear derecha";
            this.btnJustifyRight.Click += new System.EventHandler(this.btnJustifyRight_Click);
            // 
            // btnOrderedList
            // 
            this.btnOrderedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOrderedList.Image = ((System.Drawing.Image)(resources.GetObject("btnOrderedList.Image")));
            this.btnOrderedList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOrderedList.Name = "btnOrderedList";
            this.btnOrderedList.Size = new System.Drawing.Size(23, 27);
            this.btnOrderedList.ToolTipText = "Lista ordenada";
            this.btnOrderedList.Click += new System.EventHandler(this.btnOrderedList_Click);
            // 
            // btnInsertUnorderedList
            // 
            this.btnInsertUnorderedList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInsertUnorderedList.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertUnorderedList.Image")));
            this.btnInsertUnorderedList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsertUnorderedList.Name = "btnInsertUnorderedList";
            this.btnInsertUnorderedList.Size = new System.Drawing.Size(23, 27);
            this.btnInsertUnorderedList.ToolTipText = "Lista no ordenada";
            this.btnInsertUnorderedList.Click += new System.EventHandler(this.btnInsertUnorderedList_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 27);
            this.btnCopy.ToolTipText = "Copiar";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = ((System.Drawing.Image)(resources.GetObject("btnPaste.Image")));
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 27);
            this.btnPaste.ToolTipText = "Pegar";
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = ((System.Drawing.Image)(resources.GetObject("btnCut.Image")));
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 27);
            this.btnCut.ToolTipText = "Cortar";
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnInsertLine
            // 
            this.btnInsertLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInsertLine.Image = ((System.Drawing.Image)(resources.GetObject("btnInsertLine.Image")));
            this.btnInsertLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInsertLine.Name = "btnInsertLine";
            this.btnInsertLine.Size = new System.Drawing.Size(23, 27);
            this.btnInsertLine.ToolTipText = "Insertar separador";
            this.btnInsertLine.Click += new System.EventHandler(this.btnInsertLine_Click);
            // 
            // wbrHtmlBox
            // 
            this.wbrHtmlBox.AllowNavigation = false;
            this.wbrHtmlBox.AllowWebBrowserDrop = false;
            this.wbrHtmlBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbrHtmlBox.IsWebBrowserContextMenuEnabled = false;
            this.wbrHtmlBox.Location = new System.Drawing.Point(0, 0);
            this.wbrHtmlBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbrHtmlBox.Name = "wbrHtmlBox";
            this.wbrHtmlBox.Size = new System.Drawing.Size(500, 266);
            this.wbrHtmlBox.TabIndex = 0;
            this.wbrHtmlBox.WebBrowserShortcutsEnabled = false;
            this.wbrHtmlBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.wbrHtmlBox_PreviewKeyDown);
            // 
            // HtmlEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spcMainContainer);
            this.Name = "HtmlEditor";
            this.Size = new System.Drawing.Size(500, 300);
            this.spcMainContainer.Panel1.ResumeLayout(false);
            this.spcMainContainer.Panel1.PerformLayout();
            this.spcMainContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spcMainContainer)).EndInit();
            this.spcMainContainer.ResumeLayout(false);
            this.tspCommands.ResumeLayout(false);
            this.tspCommands.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spcMainContainer;
        private System.Windows.Forms.ToolStrip tspCommands;
        private System.Windows.Forms.WebBrowser wbrHtmlBox;
        private System.Windows.Forms.ToolStripButton btnBold;
        private System.Windows.Forms.ToolStripButton btnItalic;
        private System.Windows.Forms.ToolStripButton btnUnderline;
        private System.Windows.Forms.ToolStripButton btnFontColor;
        private System.Windows.Forms.ToolStripButton btnFontFamily;
        private System.Windows.Forms.ToolStripButton btnJustifyLeft;
        private System.Windows.Forms.ToolStripButton btnJustifyCenter;
        private System.Windows.Forms.ToolStripButton btnJustifyRight;
        private System.Windows.Forms.ToolStripButton btnOrderedList;
        private System.Windows.Forms.ToolStripButton btnInsertUnorderedList;
        private System.Windows.Forms.ToolStripButton btnBackColor;
        private System.Windows.Forms.ToolStripButton btnCopy;
        private System.Windows.Forms.ToolStripButton btnPaste;
        private System.Windows.Forms.ToolStripButton btnCut;
        private System.Windows.Forms.ToolStripButton btnInsertLine;
        private System.Windows.Forms.ToolStripButton btnSuperscript;
        private System.Windows.Forms.ToolStripButton btnSubscript;
    }
}
