using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    public partial class SelectCompany : Form
    {
        private string companySelect = "";
        private string pathArchive;
        private Job job = null;
        private bool cancel = true;

        public SelectCompany(string pathArchive, Job job)
        {
            InitializeComponent();

            this.pathArchive = pathArchive;
            this.job = job;

            lbDocumentName.Text = job.Name;
            numberOfCopies.Value = 1;

            InitializeComboBoxCompanies();
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            companySelect = cbCompanies.SelectedItem.ToString();
            cancel = false;
            Dispose();
        }

        private void InitializeComboBoxCompanies()
        {
            foreach(var item in DataManager.GetCompanies(pathArchive))
            {
                cbCompanies.Items.Add(item.name);
            }
        }

        private void SelectCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(cancel)
                PrintJobManager.ActionPrintJob(job.PrinterName, job.Id, PrintJobManager.CANCEL);
        }
        
        public string GetCompanySelect()
        {
            return companySelect;
        }

        public int GetNumberOfCopies()
        {
            return Int32.Parse(numberOfCopies.Value.ToString().Trim());
        }

        private void btnCancelPrint_Click(object sender, EventArgs e)
        {
            PrintJobManager.ActionPrintJob(job.PrinterName, job.Id, PrintJobManager.CANCEL);
            Dispose();
        }
    }
}
