using GerenciadorDeImpressao.management;
using System;
using System.Collections.Specialized;
using System.Drawing.Printing;
using System.Printing;
using System.Threading;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    public partial class Init : Form
    {
        private ContextMenu contextMenu = new ContextMenu();
        private MenuItem configuracoes = new MenuItem();
        private MenuItem sair = new MenuItem();
        private MenuItem novoEditar = new MenuItem();
        private Thread threadContext = null;
        private Thread threadFileDialog = null;
        private InsertCompanies insertCompanies = null;
        private string pathArchive = "";
        private int idJob = -1;
        private int X = 0;
        private int Y = 0;


        public Init()
        {
            InitializeComponent();
            InitializeCbPrintersName();
            InitializeContextMenu();
            InitializeHide();
        }

        private void InitializeHide()
        {
            if (DataManager.GetPrintersInArchive().Count > 0)
            {
                StringCollection printers = DataManager.GetPrintersInArchive();
                for (int i = 0; i < printers.Count - 1; i++)
                {
                    clbPrintersName.SelectedItems.Add(printers[i]);
                    clbPrintersName.SetItemChecked(clbPrintersName.Items.IndexOf(printers[i]), true);
                }
                pathArchive = printers[printers.Count - 1].ToString();
                clbPrintersName.Update();
                clbPrintersName.Refresh();
                btnConfirm_Click(pathArchive, new EventArgs());

                this.Opacity = 0;
                this.ShowInTaskbar = false;
            }
        }

        private object ExistsInList(string name)
        {
            foreach (var item in clbPrintersName.Items)
            {
                if (String.Compare(item.ToString().Trim(), name, true) == 0)
                {
                    return item;
                }
            }

            return false;
        }

        private void InitializeContextMenu()
        {
            contextMenu.MenuItems.Add(novoEditar);
            contextMenu.MenuItems.Add(configuracoes);
            contextMenu.MenuItems.Add(sair);
            configuracoes.Text = "Configurações";
            sair.Text = "Sair";
            novoEditar.Text = "Inserir/Editar Empresas";
            sair.Click += new EventHandler(FinalizeApplication_Click);
            configuracoes.Click += new EventHandler(ShowApplication_Click);
            novoEditar.Click += new EventHandler(InsertCompanies_Click);
            notifyIcon1.ContextMenu = contextMenu;
        }

        private void InitializeCbPrintersName()
        {
            foreach (var item in PrintJobManager.GetPrintersCollection())
            {
                clbPrintersName.Items.Add(item);
            }
        }

        private string TrimPrinterName(string name)
        {
            if (name.LastIndexOf('\\') > 0)
                return name.Substring(name.LastIndexOf('\\') + 1);

            return name;
        }

        private string TrimServerName(string name)
        {
            if (name.LastIndexOf('\\') > 0)
                return name.Substring(0, name.LastIndexOf('\\'));

            return null;
        }
        [STAThread]
        private void btnSelectArchive_Click(object sender, EventArgs e)
        {
            ShowOpenFileDialog();
        }

        private void ShowOpenFileDialog()
        {
            if (threadFileDialog == null || !threadFileDialog.IsAlive)
            {
                threadFileDialog = new Thread(() => SelectArchive());
                threadFileDialog.SetApartmentState(ApartmentState.STA);
                threadFileDialog.IsBackground = true;
                threadFileDialog.Start();
            }
            else
            {
                MessageBox.Show("Já existe uma janela aberta!");
            }
        }
        [STAThread]
        private void SelectArchive()
        {
            string path = "";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                path = openFileDialog1.FileName.ToString();
                pathArchive = path;
            }
        }

        private StringCollection GetSelectedPrinters()
        {
            StringCollection selectedPrinters = new StringCollection();
            foreach (var item in clbPrintersName.SelectedItems)
            {
                selectedPrinters.Add(item.ToString());
            }

            return selectedPrinters;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ValidateArchivePath() && ValidatePrinters() && ValidateArchiveCompanies())
            {
                Hide();
                if (threadContext != null)
                    threadContext.Abort();
                threadContext = new Thread(() => InitLoopVerifyJobs());
                threadContext.IsBackground = false;
                threadContext.Start();

                DataManager.SavePrintersInArchive(GetPrintersChecked(), pathArchive);
            }
        }

        private StringCollection GetPrintersChecked()
        {
            StringCollection printers = new StringCollection();
            foreach (var item in clbPrintersName.CheckedItems)
            {
                printers.Add(item.ToString());
            }

            return printers;
        }

        private bool ValidatePrinters()
        {
            if (clbPrintersName.CheckedItems.Count > 0)
                return true;
            MessageBox.Show("É necessário selecionar ao menos uma impressora!");
            return false;
        }

        private void Init_Resize(object sender, System.EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Opacity == 0)
            {
                this.Opacity = 100;
                this.ShowInTaskbar = true;
            }
            else
                Show();
        }

        private void Init_FormClosing(object sender, FormClosingEventArgs e)
        {

            Hide();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void ShowApplication_Click(object sender, EventArgs e)
        {
            if (this.Opacity == 0)
            {
                this.Opacity = 100;
                this.ShowInTaskbar = true;
            }
            else
                Show();
        }

        private void FinalizeApplication_Click(object sender, EventArgs e)
        {
            if (threadContext != null)
                threadContext.Abort();
            Dispose();
        }

        private void InsertCompanies_Click(object sender, EventArgs e)
        {
            if (pathArchive.Length > 0)
            {
                if (insertCompanies == null || insertCompanies.IsDisposed)
                    insertCompanies = new InsertCompanies(pathArchive);
                insertCompanies.Show();
            }
            else
                MessageBox.Show("É necessário selecionar o arquivo de banco de dados!");
        }

        private void InitLoopVerifyJobs()
        {
            try
            {
                int lastJobId = 0;
                StringCollection printersSelected = new StringCollection();
                Invoke(new Action(() =>
                {
                    printersSelected = GetSelectedPrinters();
                }
                ));
                while (true)
                {
                    foreach (var print in printersSelected)
                    {
                        foreach (var job in PrintJobManager.GetPrintJobsCollection(TrimServerName(print), TrimPrinterName(print)))
                        {
                            if ((lastJobId == 0 || lastJobId != job.JobIdentifier))// && String.Compare(job.Submitter, Environment.UserName, true) == 0)
                            {
                                this.idJob = job.JobIdentifier;
                                DoWork(job, TrimPrinterName(print));
                                lastJobId = job.JobIdentifier;
                            }
                        }
                    }
                    Thread.Sleep(50);
                }
            }
            catch (ThreadAbortException te)
            {
                Console.WriteLine("Programa fechado! Thread Abortada " + te.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao executar verificação de trabalhos na(s) impressora(s) " + e.Message);
            }
        }

        private void DoWork(PrintSystemJobInfo job, string printerName)
        {
            job.Refresh();
            job.Pause();
            job.Refresh();
            job.Refresh();

            SelectCompany select = new SelectCompany(pathArchive, job);
            select.ShowDialog();

            if (select.GetCompanySelect().Trim().Length == 0)
            {
                this.idJob = -1;
                return;
            }
            
            job.Refresh();
            DataManagerPrint.InsertPrint(pathArchive,
                GetPrint(job.NumberOfPages * select.GetNumberOfCopies(), select.GetCompanySelect(), printerName, job.Name.ToString()));

            job.Refresh();
            if (job.JobStatus == PrintJobStatus.Paused)
            {
                job.Refresh();
                job.Resume();
                job.Refresh();
            }

            this.idJob = -1;
        }

        private Print GetPrint(int qtdPaginas, string company, string printerName, string documentName)
        {
            Print print = new Print();
            print.date = DateTime.Now;
            print.quantityPages = PrintJobManager.LowToner(printerName) == true ? 0 : qtdPaginas;
            print.documentName = documentName;
            print.company = DataManagerCompany.GetCompany(pathArchive, company);

            Printer printer = DataManagerPrinter.GetPrinter(pathArchive, TrimPrinterName(printerName), true);
            printer.printedPages += qtdPaginas;
            printer.lastMediaPages = PrintJobManager.LowToner(printerName) == true ? printer.mediaPages : printer.lastMediaPages;
            DataManagerPrinter.UpdatePrinter(pathArchive, printer);
            printer = DataManagerPrinter.GetPrinter(pathArchive, printer.name, false);
            print.printer = printer;
            print.cost = printer.lastMediaPages * (double)qtdPaginas;

            return print;
        }

        private bool ValidateArchivePath()
        {
            if (pathArchive.Length > 0)
                return true;
            MessageBox.Show("É necessário selecionar o arquivo de saída!");
            return false;
        }

        private bool ValidateArchiveCompanies()
        {
            if (DataManagerCompany.GetCompanies(pathArchive).Count > 0)
                return true;
            MessageBox.Show("É necessário cadastrar ao menos uma empresa!");

            return false;
        }

        public string GetPathArchive()
        {
            return pathArchive;
        }

        private void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            if (pathArchive.Length > 0)
            {
                if (insertCompanies == null || insertCompanies.IsDisposed)
                {
                    insertCompanies = new InsertCompanies(pathArchive);
                    insertCompanies.SetSelectTab();
                }
                insertCompanies.Show();
            }
            else
                MessageBox.Show("É necessário selecionar o arquivo de banco de dados!");
        }

        private void generatePDF_MouseHover(object sender, EventArgs e)
        {
        }

        private void generatePDF_MouseLeave(object sender, EventArgs e)
        {
        }

        private void confirm_MouseHover(object sender, EventArgs e)
        {
        }

        private void confirm_MouseLeave(object sender, EventArgs e)
        {
        }

        private void selectDB_MouseHover(object sender, EventArgs e)
        {
        }

        private void selectDB_MouseLeave(object sender, EventArgs e)
        {
        }

        private void confirm_MouseHover_1(object sender, EventArgs e)
        {
        }

        private void confirm_MouseLeave_1(object sender, EventArgs e)
        {
        }

        private void close_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized && this.Opacity > 0)
            {
                this.Opacity = 100;
                this.ShowInTaskbar = true;
                WindowState = FormWindowState.Minimized;
            }
        }

        private void miniminize_MouseHover(object sender, EventArgs e)
        {
            miniminize.Image = Properties.Resources.hide_hover;
        }

        private void miniminize_MouseLeave(object sender, EventArgs e)
        {
            miniminize.Image = Properties.Resources.hide;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            X = this.Left - MousePosition.X;
            Y = this.Top - MousePosition.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            this.Left = X + MousePosition.X;
            this.Top = Y + MousePosition.Y;
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            close.Image = Properties.Resources.close_hover;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            close.Image = Properties.Resources.close;
        }
    }
}
