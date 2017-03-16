using GerenciadorDeImpressao.management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    public partial class SelectCompany : Form
    {
        private string companySelect = "";
        private bool cancel = true;
        private string pathArchive;
        private PrintSystemJobInfo job = null;

        public SelectCompany(string pathArchive,  PrintSystemJobInfo job)
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
            if (cbCompanies.Text.Trim().Length > 0)
            {
                companySelect = cbCompanies.SelectedItem.ToString();
                cancel = false;
                Dispose();
            }
            else
            {
                MessageBox.Show("Selecione uma empresa");
            }
        }

        private void InitializeComboBoxCompanies()
        {
            foreach(var item in DataManagerCompany.GetCompanies(pathArchive))
            {
                cbCompanies.Items.Add(item.name);
            }
        }

        private void SelectCompany_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(cancel)
                job.Cancel();
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
            Dispose();
        }

        private static class User32
        {
            [DllImport("User32.dll")]
            internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            internal static readonly IntPtr InvalidHandleValue = IntPtr.Zero;
            internal const int SW_MAXIMIZE = 3;
        }
        public void Activate()
        {
            Process currentProcess = Process.GetCurrentProcess();
            IntPtr hWnd = currentProcess.MainWindowHandle;
            if (hWnd != User32.InvalidHandleValue)
            {
                User32.SetForegroundWindow(hWnd);
                User32.ShowWindow(hWnd, User32.SW_MAXIMIZE);
            }
        }
    }
}
