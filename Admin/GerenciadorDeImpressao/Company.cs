using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorDeImpressao
{
    class Company
    {
        public int id { get; set; }
        public string name { get; set; }
            
        public Company()
        {

        }
        public Company(int id, string company)
        {
            this.id = id;
            this.name = company;
        }

        public Company(string name)
        {
            this.name = name;
        }
    }
}
