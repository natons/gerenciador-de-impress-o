using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    partial class Init
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Init));
            this.clbPrintersName = new System.Windows.Forms.CheckedListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.generateVa = new System.Windows.Forms.Label();
            this.generatePDF = new System.Windows.Forms.Label();
            this.gerenciar = new System.Windows.Forms.Label();
            this.selectDB = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // clbPrintersName
            // 
            this.clbPrintersName.BackColor = System.Drawing.Color.White;
            this.clbPrintersName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clbPrintersName.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbPrintersName.FormattingEnabled = true;
            this.clbPrintersName.Location = new System.Drawing.Point(20, 50);
            this.clbPrintersName.Name = "clbPrintersName";
            this.clbPrintersName.Size = new System.Drawing.Size(327, 255);
            this.clbPrintersName.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "All files|*.*";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Estamos monitorando as suas impressões.";
            this.notifyIcon1.BalloonTipTitle = "Gerenciador de Impressão";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Gerenciador de Impressão";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // generateVa
            // 
            this.generateVa.AutoSize = true;
            this.generateVa.BackColor = System.Drawing.Color.Transparent;
            this.generateVa.ForeColor = System.Drawing.Color.Transparent;
            this.generateVa.Image = global::GerenciadorDeImpressao.Properties.Resources.pdf_icon;
            this.generateVa.Location = new System.Drawing.Point(167, 299);
            this.generateVa.Name = "generateVa";
            this.generateVa.Size = new System.Drawing.Size(0, 13);
            this.generateVa.TabIndex = 6;
            // 
            // generatePDF
            // 
            this.generatePDF.BackColor = System.Drawing.Color.Transparent;
            this.generatePDF.Image = global::GerenciadorDeImpressao.Properties.Resources.pdf_icon;
            this.generatePDF.Location = new System.Drawing.Point(111, 322);
            this.generatePDF.Name = "generatePDF";
            this.generatePDF.Size = new System.Drawing.Size(52, 60);
            this.generatePDF.TabIndex = 7;
            this.generatePDF.Click += new System.EventHandler(this.btnGeneratePDF_Click);
            this.generatePDF.MouseLeave += new System.EventHandler(this.generatePDF_MouseLeave);
            this.generatePDF.MouseHover += new System.EventHandler(this.generatePDF_MouseHover);
            // 
            // gerenciar
            // 
            this.gerenciar.BackColor = System.Drawing.Color.Transparent;
            this.gerenciar.Image = global::GerenciadorDeImpressao.Properties.Resources.gerenciar_icon;
            this.gerenciar.Location = new System.Drawing.Point(190, 320);
            this.gerenciar.Name = "gerenciar";
            this.gerenciar.Size = new System.Drawing.Size(55, 62);
            this.gerenciar.TabIndex = 8;
            this.gerenciar.Click += new System.EventHandler(this.InsertCompanies_Click);
            this.gerenciar.MouseLeave += new System.EventHandler(this.confirm_MouseLeave);
            this.gerenciar.MouseHover += new System.EventHandler(this.confirm_MouseHover);
            // 
            // selectDB
            // 
            this.selectDB.BackColor = System.Drawing.Color.Transparent;
            this.selectDB.Image = global::GerenciadorDeImpressao.Properties.Resources.bancodedados_icon;
            this.selectDB.Location = new System.Drawing.Point(39, 323);
            this.selectDB.Name = "selectDB";
            this.selectDB.Size = new System.Drawing.Size(48, 60);
            this.selectDB.TabIndex = 9;
            this.selectDB.Click += new System.EventHandler(this.btnSelectArchive_Click);
            this.selectDB.MouseLeave += new System.EventHandler(this.selectDB_MouseLeave);
            this.selectDB.MouseHover += new System.EventHandler(this.selectDB_MouseHover);
            // 
            // confirm
            // 
            this.confirm.BackColor = System.Drawing.Color.Transparent;
            this.confirm.Image = global::GerenciadorDeImpressao.Properties.Resources.confirmar_icon;
            this.confirm.Location = new System.Drawing.Point(263, 323);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(66, 60);
            this.confirm.TabIndex = 10;
            this.confirm.Click += new System.EventHandler(this.btnConfirm_Click);
            this.confirm.MouseLeave += new System.EventHandler(this.confirm_MouseLeave_1);
            this.confirm.MouseHover += new System.EventHandler(this.confirm_MouseHover_1);
            // 
            // Init
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(356, 420);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.selectDB);
            this.Controls.Add(this.gerenciar);
            this.Controls.Add(this.generatePDF);
            this.Controls.Add(this.generateVa);
            this.Controls.Add(this.clbPrintersName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Init";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Init_FormClosing);
            this.Resize += new System.EventHandler(this.Init_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox clbPrintersName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private Label generateVa;
        private Label generatePDF;
        private Label gerenciar;
        private Label selectDB;
        private Label confirm;
    }
}

