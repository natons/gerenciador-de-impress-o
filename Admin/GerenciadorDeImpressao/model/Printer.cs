using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeImpressao
{
    class Printer
    {
        public int id { get; set; }
        public string name { get; set; }
        public double priceToner { get; set; }
        public double lastMediaPages { get; set; }
        public double mediaPages { get; set; }
        public int printedPages { get; set; }

        public Printer()
        {

        }

        public Printer(int id, string name, double priceToner, double lastMediaPages, double mediaPages, int printedPages)
        {
            this.id = id;
            this.name = name;
            this.priceToner = priceToner;
            this.lastMediaPages = lastMediaPages;
            this.mediaPages = mediaPages;
            this.printedPages = printedPages;
        }
    }
}
