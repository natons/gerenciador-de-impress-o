namespace GerenciadorDeImpressao
{
    partial class InsertCompanies
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertCompanies));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbMessage = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lbEditCompany = new System.Windows.Forms.Label();
            this.lbAddCompany = new System.Windows.Forms.Label();
            this.lbConfirmCompany = new System.Windows.Forms.Label();
            this.lbRemoveCompany = new System.Windows.Forms.Label();
            this.refreshCompanies = new System.Windows.Forms.Label();
            this.cbCompanies = new System.Windows.Forms.ComboBox();
            this.dgvCompanies = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.confirmPrinted = new System.Windows.Forms.Label();
            this.tbPrintedPages = new System.Windows.Forms.TextBox();
            this.tbMediaPages = new System.Windows.Forms.TextBox();
            this.tbPriceToner = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbPrinters = new System.Windows.Forms.ListBox();
            this.menuEditPrinted = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editPrinted = new System.Windows.Forms.ToolStripMenuItem();
            this.removePrinted = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbSearch = new System.Windows.Forms.Label();
            this.dtpFinishR = new System.Windows.Forms.DateTimePicker();
            this.dtpInitR = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPrintersR = new System.Windows.Forms.ComboBox();
            this.cbCompaniesR = new System.Windows.Forms.ComboBox();
            this.dgvR = new System.Windows.Forms.DataGridView();
            this.savePDF = new System.Windows.Forms.SaveFileDialog();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanies)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.menuEditPrinted.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvR)).BeginInit();
            this.SuspendLayout();
            // 
            // lbMessage
            // 
            this.lbMessage.BackColor = System.Drawing.Color.Transparent;
            this.lbMessage.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMessage.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lbMessage.Location = new System.Drawing.Point(41, 428);
            this.lbMessage.Name = "lbMessage";
            this.lbMessage.Size = new System.Drawing.Size(612, 91);
            this.lbMessage.TabIndex = 1;
            this.lbMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(42, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(611, 345);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lbEditCompany);
            this.tabPage1.Controls.Add(this.lbAddCompany);
            this.tabPage1.Controls.Add(this.lbConfirmCompany);
            this.tabPage1.Controls.Add(this.lbRemoveCompany);
            this.tabPage1.Controls.Add(this.refreshCompanies);
            this.tabPage1.Controls.Add(this.cbCompanies);
            this.tabPage1.Controls.Add(this.dgvCompanies);
            this.tabPage1.ForeColor = System.Drawing.Color.Black;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(603, 317);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Empresas";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lbEditCompany
            // 
            this.lbEditCompany.Image = global::GerenciadorDeImpressao.Properties.Resources.icon_edit;
            this.lbEditCompany.Location = new System.Drawing.Point(138, 28);
            this.lbEditCompany.Name = "lbEditCompany";
            this.lbEditCompany.Size = new System.Drawing.Size(16, 15);
            this.lbEditCompany.TabIndex = 3;
            this.lbEditCompany.Click += new System.EventHandler(this.lbEditCompany_Click);
            // 
            // lbAddCompany
            // 
            this.lbAddCompany.Image = ((System.Drawing.Image)(resources.GetObject("lbAddCompany.Image")));
            this.lbAddCompany.Location = new System.Drawing.Point(92, 25);
            this.lbAddCompany.Name = "lbAddCompany";
            this.lbAddCompany.Size = new System.Drawing.Size(17, 20);
            this.lbAddCompany.TabIndex = 7;
            this.lbAddCompany.Click += new System.EventHandler(this.lbAddCompany_Click);
            // 
            // lbConfirmCompany
            // 
            this.lbConfirmCompany.Image = global::GerenciadorDeImpressao.Properties.Resources.ok_icon;
            this.lbConfirmCompany.Location = new System.Drawing.Point(137, 26);
            this.lbConfirmCompany.Name = "lbConfirmCompany";
            this.lbConfirmCompany.Size = new System.Drawing.Size(17, 20);
            this.lbConfirmCompany.TabIndex = 6;
            this.lbConfirmCompany.Visible = false;
            this.lbConfirmCompany.Click += new System.EventHandler(this.lbConfirmCompany_Click);
            // 
            // lbRemoveCompany
            // 
            this.lbRemoveCompany.Image = ((System.Drawing.Image)(resources.GetObject("lbRemoveCompany.Image")));
            this.lbRemoveCompany.Location = new System.Drawing.Point(114, 25);
            this.lbRemoveCompany.Name = "lbRemoveCompany";
            this.lbRemoveCompany.Size = new System.Drawing.Size(18, 20);
            this.lbRemoveCompany.TabIndex = 5;
            this.lbRemoveCompany.Click += new System.EventHandler(this.lbRemoveCompany_Click);
            // 
            // refreshCompanies
            // 
            this.refreshCompanies.Image = ((System.Drawing.Image)(resources.GetObject("refreshCompanies.Image")));
            this.refreshCompanies.Location = new System.Drawing.Point(479, 26);
            this.refreshCompanies.Name = "refreshCompanies";
            this.refreshCompanies.Size = new System.Drawing.Size(20, 20);
            this.refreshCompanies.TabIndex = 3;
            this.refreshCompanies.Click += new System.EventHandler(this.refreshCompanies_Click);
            // 
            // cbCompanies
            // 
            this.cbCompanies.DisplayMember = "0";
            this.cbCompanies.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompanies.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.cbCompanies.ItemHeight = 15;
            this.cbCompanies.Location = new System.Drawing.Point(160, 25);
            this.cbCompanies.Name = "cbCompanies";
            this.cbCompanies.Size = new System.Drawing.Size(313, 23);
            this.cbCompanies.TabIndex = 2;
            this.cbCompanies.SelectedIndexChanged += new System.EventHandler(this.cbCompanies_SelectedIndexChanged);
            this.cbCompanies.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbCompanies_KeyPress);
            // 
            // dgvCompanies
            // 
            this.dgvCompanies.AccessibleName = "Dtalhe das impressões";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvCompanies.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCompanies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvCompanies.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvCompanies.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvCompanies.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvCompanies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompanies.Location = new System.Drawing.Point(0, 60);
            this.dgvCompanies.MultiSelect = false;
            this.dgvCompanies.Name = "dgvCompanies";
            this.dgvCompanies.ReadOnly = true;
            this.dgvCompanies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCompanies.Size = new System.Drawing.Size(603, 257);
            this.dgvCompanies.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.confirmPrinted);
            this.tabPage2.Controls.Add(this.tbPrintedPages);
            this.tabPage2.Controls.Add(this.tbMediaPages);
            this.tabPage2.Controls.Add(this.tbPriceToner);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.lbPrinters);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(603, 317);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Impressoras";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // confirmPrinted
            // 
            this.confirmPrinted.Image = global::GerenciadorDeImpressao.Properties.Resources.ok;
            this.confirmPrinted.Location = new System.Drawing.Point(423, 233);
            this.confirmPrinted.Name = "confirmPrinted";
            this.confirmPrinted.Size = new System.Drawing.Size(25, 18);
            this.confirmPrinted.TabIndex = 7;
            this.confirmPrinted.Click += new System.EventHandler(this.confirmPrinted_Click);
            // 
            // tbPrintedPages
            // 
            this.tbPrintedPages.Location = new System.Drawing.Point(431, 188);
            this.tbPrintedPages.Name = "tbPrintedPages";
            this.tbPrintedPages.ReadOnly = true;
            this.tbPrintedPages.Size = new System.Drawing.Size(143, 23);
            this.tbPrintedPages.TabIndex = 6;
            // 
            // tbMediaPages
            // 
            this.tbMediaPages.Location = new System.Drawing.Point(431, 147);
            this.tbMediaPages.Name = "tbMediaPages";
            this.tbMediaPages.ReadOnly = true;
            this.tbMediaPages.Size = new System.Drawing.Size(143, 23);
            this.tbMediaPages.TabIndex = 5;
            // 
            // tbPriceToner
            // 
            this.tbPriceToner.Location = new System.Drawing.Point(431, 108);
            this.tbPriceToner.Name = "tbPriceToner";
            this.tbPriceToner.ReadOnly = true;
            this.tbPriceToner.Size = new System.Drawing.Size(143, 23);
            this.tbPriceToner.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(298, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Páginas Impressas: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(298, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Valor por página: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(298, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Valor do Toner: ";
            // 
            // lbPrinters
            // 
            this.lbPrinters.ContextMenuStrip = this.menuEditPrinted;
            this.lbPrinters.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrinters.FormattingEnabled = true;
            this.lbPrinters.ItemHeight = 18;
            this.lbPrinters.Location = new System.Drawing.Point(84, 24);
            this.lbPrinters.Name = "lbPrinters";
            this.lbPrinters.Size = new System.Drawing.Size(155, 274);
            this.lbPrinters.TabIndex = 0;
            this.lbPrinters.SelectedIndexChanged += new System.EventHandler(this.lbPrinters_SelectedIndexChanged);
            // 
            // menuEditPrinted
            // 
            this.menuEditPrinted.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editPrinted,
            this.removePrinted});
            this.menuEditPrinted.Name = "menuEditPrinted";
            this.menuEditPrinted.Size = new System.Drawing.Size(109, 48);
            this.menuEditPrinted.Text = "Menu";
            // 
            // editPrinted
            // 
            this.editPrinted.Name = "editPrinted";
            this.editPrinted.Size = new System.Drawing.Size(108, 22);
            this.editPrinted.Text = "Editar";
            this.editPrinted.Click += new System.EventHandler(this.editPrinted_Click);
            // 
            // removePrinted
            // 
            this.removePrinted.Name = "removePrinted";
            this.removePrinted.Size = new System.Drawing.Size(108, 22);
            this.removePrinted.Text = "Excluir";
            this.removePrinted.Click += new System.EventHandler(this.removePrinted_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.btnExportPDF);
            this.tabPage3.Controls.Add(this.lbSearch);
            this.tabPage3.Controls.Add(this.dtpFinishR);
            this.tabPage3.Controls.Add(this.dtpInitR);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.cbPrintersR);
            this.tabPage3.Controls.Add(this.cbCompaniesR);
            this.tabPage3.Controls.Add(this.dgvR);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(603, 317);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Relatório";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbSearch
            // 
            this.lbSearch.Image = ((System.Drawing.Image)(resources.GetObject("lbSearch.Image")));
            this.lbSearch.Location = new System.Drawing.Point(570, 28);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(21, 23);
            this.lbSearch.TabIndex = 12;
            this.lbSearch.Click += new System.EventHandler(this.lbSearch_Click);
            // 
            // dtpFinishR
            // 
            this.dtpFinishR.Location = new System.Drawing.Point(447, 26);
            this.dtpFinishR.Name = "dtpFinishR";
            this.dtpFinishR.Size = new System.Drawing.Size(116, 23);
            this.dtpFinishR.TabIndex = 11;
            // 
            // dtpInitR
            // 
            this.dtpInitR.Location = new System.Drawing.Point(325, 26);
            this.dtpInitR.Name = "dtpInitR";
            this.dtpInitR.Size = new System.Drawing.Size(116, 23);
            this.dtpInitR.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(477, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 15);
            this.label5.TabIndex = 9;
            this.label5.Text = "Data fim";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(349, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Data início";
            // 
            // cbPrintersR
            // 
            this.cbPrintersR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrintersR.FormattingEnabled = true;
            this.cbPrintersR.Location = new System.Drawing.Point(165, 26);
            this.cbPrintersR.Name = "cbPrintersR";
            this.cbPrintersR.Size = new System.Drawing.Size(152, 23);
            this.cbPrintersR.TabIndex = 7;
            // 
            // cbCompaniesR
            // 
            this.cbCompaniesR.DisplayMember = "s";
            this.cbCompaniesR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCompaniesR.FormattingEnabled = true;
            this.cbCompaniesR.Location = new System.Drawing.Point(7, 26);
            this.cbCompaniesR.Name = "cbCompaniesR";
            this.cbCompaniesR.Size = new System.Drawing.Size(145, 23);
            this.cbCompaniesR.TabIndex = 6;
            this.cbCompaniesR.Tag = "";
            // 
            // dgvR
            // 
            this.dgvR.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvR.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvR.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvR.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvR.Location = new System.Drawing.Point(0, 72);
            this.dgvR.Name = "dgvR";
            this.dgvR.ReadOnly = true;
            this.dgvR.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvR.Size = new System.Drawing.Size(603, 213);
            this.dgvR.TabIndex = 3;
            // 
            // savePDF
            // 
            this.savePDF.FileName = "Relatorio_De_Impressoes";
            this.savePDF.Filter = "PDF Files|*.pdf";
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.Location = new System.Drawing.Point(439, 291);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(152, 23);
            this.btnExportPDF.TabIndex = 13;
            this.btnExportPDF.Text = "Exportar para PDF";
            this.btnExportPDF.UseVisualStyleBackColor = true;
            this.btnExportPDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // InsertCompanies
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::GerenciadorDeImpressao.Properties.Resources.fundoGerenciar;
            this.ClientSize = new System.Drawing.Size(711, 530);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbMessage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InsertCompanies";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InsertCompanies";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompanies)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.menuEditPrinted.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbMessage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvCompanies;
        private System.Windows.Forms.ListBox lbPrinters;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPrintedPages;
        private System.Windows.Forms.TextBox tbMediaPages;
        private System.Windows.Forms.TextBox tbPriceToner;
        private System.Windows.Forms.ContextMenuStrip menuEditPrinted;
        private System.Windows.Forms.ToolStripMenuItem editPrinted;
        private System.Windows.Forms.ToolStripMenuItem removePrinted;
        private System.Windows.Forms.Label confirmPrinted;
        private System.Windows.Forms.ComboBox cbCompanies;
        private System.Windows.Forms.Label refreshCompanies;
        private System.Windows.Forms.Label lbRemoveCompany;
        private System.Windows.Forms.Label lbConfirmCompany;
        private System.Windows.Forms.Label lbAddCompany;
        private System.Windows.Forms.Label lbEditCompany;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvR;
        private System.Windows.Forms.ComboBox cbPrintersR;
        private System.Windows.Forms.ComboBox cbCompaniesR;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpFinishR;
        private System.Windows.Forms.DateTimePicker dtpInitR;
        private System.Windows.Forms.Label lbSearch;
        private System.Windows.Forms.SaveFileDialog savePDF;
        private System.Windows.Forms.Button btnExportPDF;
    }
}