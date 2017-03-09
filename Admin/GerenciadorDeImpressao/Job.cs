using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeImpressao
{
    public class Job
    {
        public const int PAUSED = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Submitter { get; set; }
        public int TotalOfPages { get; set; }
        public int Status { get; set; }
        public string PrinterName { get; set; }
    }
}
