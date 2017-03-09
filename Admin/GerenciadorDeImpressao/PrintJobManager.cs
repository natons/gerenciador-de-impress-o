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

        public static int PAUSE = 1;
        public static int RESUME = 2;
        public static int CANCEL = 3;
        
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

        public static Job GetPrintJob(string printerName)
        {
            StringCollection printJobCollection = new StringCollection();
            string searchQuery = "SELECT * FROM Win32_PrintJob";

            /*searchQuery can also be mentioned with where Attribute,
                but this is not working in Windows 2000 / ME / 98 machines 
                and throws Invalid query error*/
            ManagementObjectSearcher searchPrintJobs =
                      new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();

                //Job name would be of the format [Printer name], [Job ID]
                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                Job job = new Job();
                job.Name = prntJob.Properties["Document"].Value.ToString();
                job.Submitter = prntJob.Properties["Owner"].Value.ToString();
                job.Id = Convert.ToInt32(jobName.Split(splitArr)[1]);
                job.TotalOfPages = Convert.ToInt32(prntJob.Properties["TotalPages"].Value.ToString());
                job.Status = Convert.ToInt32(prntJob.Properties["StatusMask"].Value.ToString());
                job.PrinterName = printerName;
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    return job;
                }
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

        public static bool ActionPrintJob(string printerName, int printJobID, int action)
        {
            bool isActionPerformed = false;
            string searchQuery = "SELECT * FROM Win32_PrintJob";
            ManagementObjectSearcher searchPrintJobs =
                     new ManagementObjectSearcher(searchQuery);
            ManagementObjectCollection prntJobCollection = searchPrintJobs.Get();
            foreach (ManagementObject prntJob in prntJobCollection)
            {
                System.String jobName = prntJob.Properties["Name"].Value.ToString();
                //Job name would be of the format [Printer name], [Job ID]
                char[] splitArr = new char[1];
                splitArr[0] = Convert.ToChar(",");
                string prnterName = jobName.Split(splitArr)[0];
                int prntJobID = Convert.ToInt32(jobName.Split(splitArr)[1]);
                string documentName = prntJob.Properties["Document"].Value.ToString();
                if (String.Compare(prnterName, printerName, true) == 0)
                {
                    if (prntJobID == printJobID)
                    {
                        switch (action)
                        {
                            case 1 : 
                                prntJob.InvokeMethod("Pause", null);
                                break;
                            case 2 :
                                prntJob.InvokeMethod("Resume", null);
                                break;
                            case 3:
                                prntJob.Delete();
                                break;
                        }
                        isActionPerformed = true;
                        break;
                    }
                }
            }
            return isActionPerformed;
        }
    }
}
