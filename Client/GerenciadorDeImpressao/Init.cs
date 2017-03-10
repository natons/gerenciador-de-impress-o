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
        private Thread threadContext = null;
        private Thread threadFileDialog = null;
        private string pathArchive = "";
        private string path = "";
        private int idJob = -1;
        private int X = 0;
        private int Y = 0;

        public Init()
        {
            InitializeComponent();

            if (DataManager.GetConfInArchive())
            {
                pathArchive = DataManager.GetPathDB();
                path = DataManager.GetPathDB().Substring(0, DataManager.GetPathDB().LastIndexOf('\\') + 1);
                this.Opacity = 0;
                this.ShowInTaskbar = false;
                
                Ready();
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
            } else
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
                this.path = pathArchive.Substring(0, pathArchive.LastIndexOf('\\') + 1);

                Invoke(new Action(() =>
                {
                    Hide();
                }
                ));

                Ready();
            }
        }

        private void Ready()
        {
            
            DataManager.SaveConfInArchive(true, pathArchive);

            if (threadContext != null)
                threadContext.Abort();
            threadContext = new Thread(() => InitLoopVerifyJobs());
            threadContext.IsBackground = false;
            threadContext.Start();
        }

        private void Init_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
        
        private void InitLoopVerifyJobs()
        {
            try
            {
                int lastJobId = 0;
                StringCollection printersSelected = new StringCollection();
                Invoke(new Action(() =>
                {
                    printersSelected = DataManager.GetPrintersInArchive(path);
                }
                ));
                while (true)
                {
                    foreach (var print in printersSelected)
                    {
                        foreach (var job in PrintJobManager.GetPrintJobsCollection(TrimServerName(print), TrimPrinterName(print)))
                        {
                            if ((lastJobId == 0 || lastJobId != job.JobIdentifier) && String.Compare(job.Submitter, Environment.UserName, true) == 0 )
                            {
                                this.idJob = job.JobIdentifier;
                                DoWork(job, TrimPrinterName(print));
                                lastJobId = job.JobIdentifier;
                            }
                        }
                    }
                }
            }
            catch(ThreadAbortException te)
            {
                Console.WriteLine("Programa fechado! Thread Abortada " + te.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ao executar verificação de trabalhos na(s) impressora(s) "+ e.Message);
            }
        }

        private void VerifyOtherJobInQueue()
        {
            StringCollection printersSelected = new StringCollection();
            int id = -1;
            Invoke(new Action(() =>
            {
                printersSelected = DataManager.GetPrintersInArchive(path);
                id = this.idJob;
            }
            ));
            while (true)
            {
                foreach (var print in printersSelected)
                {
                    foreach (var item in PrintJobManager.GetPrintJobsCollection(TrimServerName(print), TrimPrinterName(print)))
                    {
                        if (item.JobIdentifier != id)
                        {
                            MessageBox.Show("Já existe um trabalho de impressão!");
                            item.Cancel();
                        }
                    }
                }
            }
        }

        private void VerifyJobState()
        {
            StringCollection printersSelected = new StringCollection();
            int id = -1;
            Invoke(new Action(() =>
            {
                printersSelected = DataManager.GetPrintersInArchive(path);
                id = this.idJob;
            }
            ));
            while (true)
            {
                foreach (var print in printersSelected)
                {
                    foreach (var item in PrintJobManager.GetPrintJobsCollection(TrimServerName(print), TrimPrinterName(print)))
                    {
                        if (item.JobIdentifier == id && !item.IsPaused)
                        {
                            item.Pause();
                        }
                    }
                }
            }
        }

        private void DoWork(PrintSystemJobInfo job, string printerName)
        {
            job.Refresh();
            job.Pause();
            job.Refresh();

            Thread t1 = new Thread(() => VerifyOtherJobInQueue());
            Thread t2 = new Thread(() => VerifyOtherJobInQueue());
            t1.Start();
            t2.Start();

            SelectCompany select = new SelectCompany(pathArchive, job);
            select.ShowDialog();

            if (select.GetCompanySelect().Trim().Length == 0)
            {
                this.idJob = -1;
                t1.Abort();
                t2.Abort();
                return;
            }

            Console.WriteLine(select.GetCompanySelect());
            job.Refresh();
            DataManager.InsertPrint(pathArchive,
                GetPrint(job.NumberOfPages * select.GetNumberOfCopies(), select.GetCompanySelect(), printerName, job.Name.ToString()));

            job.Refresh();
            if (job.JobStatus == PrintJobStatus.Paused)
            {
                job.Refresh();
                job.Resume();
                job.Refresh();
            }

            t1.Abort();
            t2.Abort();
            this.idJob = -1;
        }

        private Print GetPrint(int qtdPaginas, string company, string printerName, string documentName)
        {
            Print print = new Print();
            print.date = DateTime.Now;
            print.quantityPages = PrintJobManager.LowToner(printerName) == true ? 0 : qtdPaginas;
            print.documentName = documentName;
            print.company = DataManager.GetCompany(pathArchive, company);

            Printer printer = DataManager.GetPrinter(pathArchive, printerName, true);
            printer.printedPages += qtdPaginas;
            printer.lastMediaPages = PrintJobManager.LowToner(printerName) == true ? printer.mediaPages : printer.lastMediaPages;
            DataManager.UpdatePrinter(pathArchive, printer);
            printer = DataManager.GetPrinter(pathArchive, printer.name, false);
            print.printer = printer;
            print.cost = printer.lastMediaPages * (double) qtdPaginas;

            return print;
        }

        private void SelectCompanyIsShow(SelectCompany form)
        {
            while (!form.GetClose());
        }

        private void selectDB_MouseHover(object sender, EventArgs e)
        {
            selectDB.Image = Properties.Resources.bd;
        }

        private void selectDB_MouseLeave(object sender, EventArgs e)
        {
            selectDB.Image = Properties.Resources.bd;
        }

        private void miniminize_Click(object sender, EventArgs e)
        {
            if(WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
            }
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

        private void miniminize_MouseHover(object sender, EventArgs e)
        {
            miniminize.Image = Properties.Resources.hide_hover;
        }

        private void miniminize_MouseLeave(object sender, EventArgs e)
        {
            miniminize.Image = Properties.Resources.hide;
        }
    }
}
