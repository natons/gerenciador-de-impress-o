using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeImpressao
{
    class Print
    {
        public int id { get; set; }
        public string documentName { get; set; }
        public int quantityPages { get; set; }
        public double cost { get; set; }
        public DateTime date { get; set; }
        public Company company { get; set; }
        public Printer printer { get; set; }

        public Print()
        {

        }

        public Print(int id, string documentName, int quantityPages, double cost, DateTime date, Printer printer)
        {
            this.id = id;
            this.documentName = documentName;
            this.quantityPages = quantityPages;
            this.cost = cost;
            this.date = date;
            this.printer = printer;
        }

        public Print(int id, string documentName, int quantityPages, double cost, DateTime date)
        {
            this.id = id;
            this.documentName = documentName;
            this.quantityPages = quantityPages;
            this.cost = cost;
            this.date = date;
            this.printer = new Printer();
        }

    }
}
