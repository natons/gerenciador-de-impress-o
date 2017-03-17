using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Management;
using System.Printing;
using System.Threading;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    static class PrintJobManager
    {

        public static StringCollection GetPrintersCollection()
        {
            StringCollection printerNameCollection = new StringCollection();
            string searchQuery = "SELECT * FROM Win32_Printer";
            ManagementObjectSearcher searchPrinters =
                  new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection printerCollection = searchPrinters.Get();
            foreach (ManagementObject printer in printerCollection)
            {
                printerNameCollection.Add(printer.Properties["Name"].Value.ToString());
            }
            return printerNameCollection;
        }

        public static PrintJobInfoCollection GetPrintJobsCollection(string serverName, string printerName)
        {
            PrintServer ps = null;
            PrintQueue printQueue = null;
            try
            {
                if (serverName == null)
                    ps = new PrintServer();
                else
                    ps = new PrintServer(@"" + serverName);
                printQueue = ps.GetPrintQueue(printerName);

                return printQueue.GetPrintJobInfoCollection();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao se conectar ao servidor de impressão " + serverName + e.Message);
                Thread.CurrentThread.Abort();
            }

            return null;
        }

        public static PrintSystemJobInfo GetPrintJob(string serverName, string printerName, int jobId)
        {
            PrintServer ps = null;
            PrintQueue printQueue = null;
            try
            {
                if (serverName == null)
                    ps = new PrintServer();
                else
                    ps = new PrintServer(@"" + serverName);
                printQueue = ps.GetPrintQueue(printerName);

                return printQueue.GetJob(jobId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erro ao se conectar ao servidor de impressão {serverName} {e.Message}");
                Thread.CurrentThread.Abort();
            }

            return null;
        }

        public static bool LowToner(string namePrinter)
        {
            string searchQuery = "SELECT * FROM Win32_Printer WHERE Name LIKE '%" + namePrinter + "%'";
            ManagementObjectSearcher searchPrinters =
                  new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection printerCollection = searchPrinters.Get();
            foreach (ManagementObject printer in printerCollection)
            {
                return Int32.Parse(printer.Properties["DetectedErrorState"].Value.ToString()) == 5 ? true : false;
            }

            return false;
        }
    }
}
