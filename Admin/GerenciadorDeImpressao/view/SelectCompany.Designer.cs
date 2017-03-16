﻿namespace GerenciadorDeImpressao
{
    partial class SelectCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectCompany));
            this.cbCompanies = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numberOfCopies = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDocumentName = new System.Windows.Forms.Label();
            this.btnCancelPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCopies)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCompanies
            // 
            this.cbCompanies.AccessibleDescription = "";
            this.cbCompanies.AccessibleRole = System.Windows.Forms.AccessibleRole.Grip;
            this.cbCompanies.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbCompanies.DisplayMember = "Sele";
            this.cbCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompanies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbCompanies.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCompanies.ForeColor = System.Drawing.SystemColors.MenuText;
            this.cbCompanies.FormattingEnabled = true;
            this.cbCompanies.Location = new System.Drawing.Point(46, 116);
            this.cbCompanies.Name = "cbCompanies";
            this.cbCompanies.Size = new System.Drawing.Size(321, 24);
            this.cbCompanies.TabIndex = 0;
            this.cbCompanies.Tag = "";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(108, 170);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(114, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Confirmar";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(41, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selecione a empresa: ";
            // 
            // numberOfCopies
            // 
            this.numberOfCopies.Location = new System.Drawing.Point(389, 117);
            this.numberOfCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numberOfCopies.Name = "numberOfCopies";
            this.numberOfCopies.Size = new System.Drawing.Size(42, 20);
            this.numberOfCopies.TabIndex = 3;
            this.numberOfCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(386, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cópias";
            // 
            // lbDocumentName
            // 
            this.lbDocumentName.BackColor = System.Drawing.Color.Transparent;
            this.lbDocumentName.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDocumentName.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbDocumentName.Location = new System.Drawing.Point(43, 51);
            this.lbDocumentName.Name = "lbDocumentName";
            this.lbDocumentName.Size = new System.Drawing.Size(387, 23);
            this.lbDocumentName.TabIndex = 5;
            this.lbDocumentName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancelPrint
            // 
            this.btnCancelPrint.Location = new System.Drawing.Point(256, 170);
            this.btnCancelPrint.Name = "btnCancelPrint";
            this.btnCancelPrint.Size = new System.Drawing.Size(113, 23);
            this.btnCancelPrint.TabIndex = 6;
            this.btnCancelPrint.Text = "Cancelar Impressão";
            this.btnCancelPrint.UseVisualStyleBackColor = true;
            this.btnCancelPrint.Click += new System.EventHandler(this.btnCancelPrint_Click);
            // 
            // SelectCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::GerenciadorDeImpressao.Properties.Resources.fundoSelecionarEmpresa;
            this.ClientSize = new System.Drawing.Size(456, 215);
            this.Controls.Add(this.btnCancelPrint);
            this.Controls.Add(this.lbDocumentName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numberOfCopies);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cbCompanies);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(504, 254);
            this.Name = "SelectCompany";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectCompany";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectCompany_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numberOfCopies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCompanies;
        private System.Windows.Forms.NumericUpDown numberOfCopies;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbDocumentName;
        private System.Windows.Forms.Button btnCancelPrint;
    }
}