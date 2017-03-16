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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.miniminize = new System.Windows.Forms.Label();
            this.selectDB = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.miniminize);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(219, 22);
            this.panel1.TabIndex = 12;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // miniminize
            // 
            this.miniminize.BackColor = System.Drawing.Color.Transparent;
            this.miniminize.Image = global::GerenciadorDeImpressao.Properties.Resources.hide;
            this.miniminize.Location = new System.Drawing.Point(193, 1);
            this.miniminize.Name = "miniminize";
            this.miniminize.Size = new System.Drawing.Size(21, 20);
            this.miniminize.TabIndex = 11;
            this.miniminize.Click += new System.EventHandler(this.miniminize_Click);
            this.miniminize.MouseLeave += new System.EventHandler(this.miniminize_MouseLeave);
            this.miniminize.MouseHover += new System.EventHandler(this.miniminize_MouseHover);
            // 
            // selectDB
            // 
            this.selectDB.BackColor = System.Drawing.Color.Transparent;
            this.selectDB.Image = global::GerenciadorDeImpressao.Properties.Resources.bd;
            this.selectDB.Location = new System.Drawing.Point(89, 40);
            this.selectDB.Name = "selectDB";
            this.selectDB.Size = new System.Drawing.Size(48, 60);
            this.selectDB.TabIndex = 9;
            this.selectDB.Click += new System.EventHandler(this.btnSelectArchive_Click);
            this.selectDB.MouseLeave += new System.EventHandler(this.selectDB_MouseLeave);
            this.selectDB.MouseHover += new System.EventHandler(this.selectDB_MouseHover);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Estamos monitorando as suas impressões.";
            this.notifyIcon1.BalloonTipTitle = "Gerenciador de Impressão";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Gerenciador de Impressão";
            this.notifyIcon1.Visible = true;
            // 
            // Init
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(219, 119);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.selectDB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Init";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Init_FormClosing);
            this.Load += new System.EventHandler(this.Init_Load);
            this.Shown += new System.EventHandler(this.Init_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Label selectDB;
        private OpenFileDialog openFileDialog1;
        private Label miniminize;
        private Panel panel1;
        private NotifyIcon notifyIcon1;
    }
}

