using GerenciadorDeImpressao.management;
using GerenciadorDeImpressao.model;
using iTextSharp.text;
using System;
using System.Collections.Generic;
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
        private List<Thread> threads = new List<Thread>();
        private List<int> lasIds = new List<int>();

        public Init()
        {
            InitializeComponent();
            
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
            {
                threadContext.Abort();
                RemoveThreads();
            }
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
                StringCollection printersSelected = new StringCollection();
                Invoke(new Action(() =>
                {
                    printersSelected = DataManager.GetPrintersInArchive(path);
                }
                ));
                CreateThreads(printersSelected);

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

        private bool IsGo(int id)
        {
            foreach (var item in lasIds)
            {
                if (item == id)
                    return true;
            }

            return false;
        }

        private void CreateThreads(StringCollection printers)
        {
            threads = new List<Thread>();
            foreach (var print in printers)
            {
                Thread t = new Thread(() =>
                {
                    int lastJobId = 0;
                    while (true)
                    {
                        foreach (var job in PrintJobManager.GetPrintJobsCollection(TrimServerName(print), TrimPrinterName(print)))
                        {
                            if ((lastJobId == 0 || !IsGo(job.JobIdentifier)) && !job.IsPaused && String.Compare(job.Submitter, Environment.UserName, true) == 0)
                            {
                                this.idJob = job.JobIdentifier;
                                Thread th = new Thread(new ParameterizedThreadStart(DoWork));
                                Process process = new Process();
                                process.jobId = job.JobIdentifier;
                                process.print = print;
                                th.Start(process);

                                lastJobId = job.JobIdentifier;
                                if (lasIds.Count > 5)
                                    lasIds = new List<int>();
                                lasIds.Add(job.JobIdentifier);
                            }
                        }
                        Thread.Sleep(1);
                    }
                }
                );
                threads.Add(t);
                t.Start();
            }
        }

        private void RemoveThreads()
        {
            foreach (var item in threads)
            {
                item.Abort();
            }
        }

        private PrintSystemJobInfo GetJob(int id, string print)
        {
            foreach (var job in PrintJobManager.GetPrintJobsCollection(TrimServerName(print), TrimPrinterName(print)))
            {
                if (id == job.JobIdentifier)
                    return job;
            }

            return null;
        }

        private void DoWork(object obj)
        {
            Process p = (Process)obj;
            PrintSystemJobInfo job = GetJob(p.jobId, p.print);
            string printerName = TrimPrinterName(p.print);
            job.Refresh();
            job.Pause();
            job.Refresh();
            job.Refresh();
            job.Refresh();

            SelectCompany select = new SelectCompany(pathArchive, job);
            select.ShowDialog();
            select.BringToFront();
            select.TopMost = true;
            select.Focus();

            if (select.GetCompanySelect().Trim().Length == 0)
            {
                this.idJob = -1;
                return;
            }

            job.Refresh();
            DataManagerPrint.InsertPrint(pathArchive,
                GetPrint(job.NumberOfPages * select.GetNumberOfCopies(), select.GetCompanySelect(), printerName, job.Name.ToString()));

            if (job.IsPaused)
                job.Resume();
            job.Refresh();
            lasIds.Remove(job.JobIdentifier);
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

        private void Init_Load(object sender, EventArgs e)
        {
            if (DataManager.GetConfInArchive())
            {
                this.Opacity = 0;
                this.ShowInTaskbar = false;
            }
        }

        private void Init_Shown(object sender, EventArgs e)
        {
            if (DataManager.GetConfInArchive())
            {
                pathArchive = DataManager.GetPathDB();
                path = DataManager.GetPathDB().Substring(0, DataManager.GetPathDB().LastIndexOf('\\') + 1);
                this.Opacity = 0;
                this.ShowInTaskbar = false;
                Ready();
            }

        }
    }
}
