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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Init));
            this.selectDB = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // selectDB
            // 
            this.selectDB.BackColor = System.Drawing.Color.Transparent;
            this.selectDB.Image = global::GerenciadorDeImpressao.Properties.Resources.bancodedados_icon;
            this.selectDB.Location = new System.Drawing.Point(86, 33);
            this.selectDB.Name = "selectDB";
            this.selectDB.Size = new System.Drawing.Size(48, 60);
            this.selectDB.TabIndex = 9;
            this.selectDB.Click += new System.EventHandler(this.btnSelectArchive_Click);
            this.selectDB.MouseLeave += new System.EventHandler(this.selectDB_MouseLeave);
            this.selectDB.MouseHover += new System.EventHandler(this.selectDB_MouseHover);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Init
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::GerenciadorDeImpressao.Properties.Resources.fundoSelecionarEmpresa;
            this.ClientSize = new System.Drawing.Size(219, 119);
            this.Controls.Add(this.selectDB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Init";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Init_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private Label selectDB;
        private OpenFileDialog openFileDialog1;
    }
}

