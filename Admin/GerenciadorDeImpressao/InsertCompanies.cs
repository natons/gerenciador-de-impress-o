using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    public partial class InsertCompanies : Form
    {
        private string pathArchive;
        private Label labelSelectCompany = new Label();
        private bool editAction = false;
        private Company companySelect = new Company();

        public InsertCompanies(string pathArchive)
        {
            InitializeComponent();
            this.pathArchive = pathArchive;
            InitializeListCompanies();
            InitializeListPrinters();
        }

        private void InitializeListCompanies()
        {
            cbCompanies.Items.Clear();
            cbCompaniesR.Items.Clear();
            List<Company> companies = DataManager.GetCompanies(pathArchive);
            cbCompaniesR.Items.Add("Todas às Empresas");
            foreach (var item in companies)
            {
                cbCompanies.Items.Add(item.name);
                cbCompaniesR.Items.Add(item.name);
            }
            if (companies.Count > 0)
            {
                cbCompanies.SelectedIndex = 0;
                cbCompaniesR.SelectedIndex = 0;
            }
        }
        
        private void InitializeListPrinters()
        {
            lbPrinters.Items.Clear();
            cbPrintersR.Items.Clear();
            List<Printer> printers = DataManager.GetPrinters(pathArchive);
            cbPrintersR.Items.Add("Todas às Impressoras");
            foreach (var item in printers)
            {
                cbPrintersR.Items.Add(item.name);
                lbPrinters.Items.Add(item.name);
            }

            if (printers.Count > 0)
            {
                cbPrintersR.SelectedIndex = 0;
                lbPrinters.SelectedIndex = 0;
            }
        }

        private void cbCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompanies.SelectedIndex > -1)
            {
                InitializeGrid();
                cbCompanies.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        private void InitializeGrid()
        {
            if(cbCompanies.Items.Count > 0)
            {
                int idCompany = DataManager.GetCompany(pathArchive, cbCompanies.SelectedItem.ToString().Trim()).id;
                if (DataManager.GetDataSetToGrid(pathArchive, idCompany).Tables[0].DefaultView.Count > 0)
                {
                    dgvCompanies.DataSource = DataManager.GetDataSetToGrid(pathArchive, idCompany).Tables[0].DefaultView;
                    DefineHeadersNameGrid(dgvCompanies);
                } else
                {
                    dgvCompanies.DataSource = null;
                    //lbMessage.Text = "Não existem registros de impressões da empresa " + cbCompanies.SelectedItem.ToString();
                } 
            }
        }

        private bool ExistsInCB(ComboBox cb, string item)
        {
            foreach(var it in cb.Items)
            {
                if (String.Compare(item, it.ToString(), true) == 0)
                    return true;
            }

            return false;
        }
        
        private void DefineHeadersNameGrid(DataGridView dgv)
        {
            dgv.Columns[0].HeaderText = "Empresa";
            dgv.Columns[1].HeaderText = "Impressora";
            dgv.Columns[2].HeaderText = "Documento";
            dgv.Columns[3].HeaderText = "N° páginas";
            dgv.Columns[4].HeaderText = "Data";
            dgv.Columns[5].HeaderText = "R$";
        }

        private void SaveCompany()
        {
            if (cbCompanies.Text.Trim().Length > 0)
            {
                if (editAction == true)
                {
                    companySelect.name = cbCompanies.Text;

                    lbMessage.Text = "Empresa editada com sucesso!";
                    editAction = false;
                    
                    DataManager.UpdateCompany(pathArchive, companySelect);
                }
                else
                {
                    if (DataManager.GetCompany(pathArchive, cbCompanies.Text.Trim()) == null)
                    {
                        Company company = new Company();
                        company.name = cbCompanies.Text.Trim();
                        DataManager.InsertCompany(pathArchive, company);
                        lbMessage.Text = "Empresa Salva com sucesso!";
                    } else
                        lbMessage.Text = "Empresa Já existe!";
                }

                InitializeListCompanies();
                InitializeGrid();
                lbConfirmCompany.Visible = false;
                lbRemoveCompany.Visible = true;
                lbEditCompany.Visible = true;
                lbAddCompany.Visible = true;
                cbCompanies.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            else
            {
                lbMessage.Text = "O campo deve ser preenchido!";
            }
        }

        private void cbCompanies_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SaveCompany();
            }
            
        }

        private void removePrinted_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Deseja realmente excluir a Impressora '" + lbPrinters.SelectedItem + "'?", "Excluir Impressora", MessageBoxButtons.YesNoCancel)
                == DialogResult.Yes)
            {
                Printer printer = DataManager.GetPrinter(pathArchive, lbPrinters.SelectedItem.ToString().Trim(), false);
                DataManager.RemovePrinter(pathArchive, printer.id);
                lbMessage.Text = "Impressora apagada com sucesso!";

                InitializeListPrinters();
                ClearTextBox();
            }
            else
            {
                lbMessage.Text = "Operação cancelada!";
            }
            
        }

        private void editPrinted_Click(object sender, EventArgs e)
        {
            StateTextBoxPrinted(false);
        }

        private void StateTextBoxPrinted(bool state)
        {
            tbPriceToner.ReadOnly = state;
            tbMediaPages.ReadOnly = state;
            tbPrintedPages.ReadOnly = state;
        }

        private void lbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            StateTextBoxPrinted(true);
            Printer printer = DataManager.GetPrinter(pathArchive, lbPrinters.SelectedItem.ToString().Trim(), false);
            tbMediaPages.Text = printer.lastMediaPages.ToString("0.00");
            tbPriceToner.Text = printer.priceToner.ToString("0.00");
            tbPrintedPages.Text = printer.printedPages.ToString();
        }

        private void confirmPrinted_Click(object sender, EventArgs e)
        {
            Printer printer = DataManager.GetPrinter(pathArchive, lbPrinters.SelectedItem.ToString().Trim(), false);
            printer.lastMediaPages = Double.Parse(tbMediaPages.Text.ToString().Trim());
            printer.priceToner = Double.Parse(tbPriceToner.Text.ToString().Trim());
            printer.printedPages = Int32.Parse(tbPrintedPages.Text.ToString().Trim());
            DataManager.UpdatePrinter(pathArchive, printer);
            StateTextBoxPrinted(true);
            InitializeListPrinters();
        }

        private void ClearTextBox()
        {
            tbMediaPages.Clear();
            tbPriceToner.Clear();
            tbPrintedPages.Clear();
        }

        private void refreshCompanies_Click(object sender, EventArgs e)
        {
            InitializeListCompanies();
            lbConfirmCompany.Visible = false;
            lbRemoveCompany.Visible = true;
            lbEditCompany.Visible = true;
            lbAddCompany.Visible = true;
            cbCompanies.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void lbEditCompany_Click(object sender, EventArgs e)
        {
            companySelect = DataManager.GetCompany(pathArchive, cbCompanies.SelectedItem.ToString().Trim());
            cbCompanies.DropDownStyle = ComboBoxStyle.Simple;
            editAction = true;
            lbConfirmCompany.Visible = true;
            lbRemoveCompany.Visible = false;
            lbEditCompany.Visible = false;
            lbAddCompany.Visible = false;
        }

        private void lbRemoveCompany_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                "Deseja realmente excluir a empresa '" + cbCompanies.SelectedItem + "'?", "Excluir empresa", MessageBoxButtons.YesNoCancel)
                == DialogResult.Yes)
            {
                DataManager.RemoveCompany(pathArchive, DataManager.GetCompany(pathArchive, cbCompanies.SelectedItem.ToString().Trim()).id);
                lbMessage.Text = "Empresa apagada com sucesso!";

                InitializeListCompanies();
                InitializeGrid();
            }
            else
            {
                lbMessage.Text = "Operação cancelada!";
            }
        }

        private void lbConfirmCompany_Click(object sender, EventArgs e)
        {
            SaveCompany();
        }

        private void lbAddCompany_Click(object sender, EventArgs e)
        {
            editAction = false;
            cbCompanies.DropDownStyle = ComboBoxStyle.Simple;
            cbCompanies.SelectedIndex = -1;
            cbCompanies.ResetText();
            lbConfirmCompany.Visible = true;
            lbRemoveCompany.Visible = false;
            lbEditCompany.Visible = false;
            lbAddCompany.Visible = false;
        }

        private void InitializeGridR()
        {
            if (cbCompaniesR.Items.Count > 0)
            {
                Company company = DataManager.GetCompany(pathArchive, cbCompaniesR.SelectedItem.ToString());
                Printer printer = DataManager.GetPrinter(pathArchive, cbPrintersR.SelectedItem.ToString(), false);
                int idCompany = company == null ? 0 : company.id;
                int idPrinter = printer == null ? 0 : printer.id;

                DataView dv = DataManager.GetDataSetToGrid(
                    pathArchive, idCompany, idPrinter, dtpInitR.Value.ToString("yyyy/MM/dd"), dtpFinishR.Value.ToString("yyyy/MM/dd"))
                    .Tables[0].DefaultView;
                if (dv.Count > 0)
                {
                    dtpFinishR.Value.ToString("yyyy/MM/dd");
                    dgvR.DataSource = dv;
                    DefineHeadersNameGrid(dgvR);
                    lbMessage.Text = "";
                }
                else
                {
                    dgvR.DataSource = null;
                    lbMessage.Text = "Não existem registros de impressões para esta seleção!";
                }
            }
        }

        private void lbSearch_Click(object sender, EventArgs e)
        {
            InitializeGridR();
        }

        public void SetSelectTab()
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void ShowOpenSaveDialog()
        {
            Thread thread = new Thread(() => SelectPDF());
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
            while (thread.IsAlive) ;
        }
        [STAThread]
        private void SelectPDF()
        {
            if (pathArchive.Length > 0)
            {
                if (savePDF.ShowDialog() == DialogResult.OK)
                {
                    PDFGenereator.GenerateTable(dgvR, savePDF.FileName);
                    
                }
            }
            else
                lbMessage.Text = "É necessário selecionar o arquivo de Saída!";
        }
        [STAThread]
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            if (dgvR.ColumnCount > 0)
            {
                ShowOpenSaveDialog();
            }
            else
            {
                lbMessage.Text = "É necessário que uma busca tenha sido feita!";
            }
        }
    }
}
