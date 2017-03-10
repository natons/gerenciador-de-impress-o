using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    static class DataManager
    {        
        //ARCHIVE PRINTERS
        public static void SavePrintersInArchive(StringCollection printers, string pathDB)
        {
            if(!Directory.Exists(@"C:\GerenciadorDeImpressao\"))
                Directory.CreateDirectory(@"C:\GerenciadorDeImpressao\");

            using (var writer = new StreamWriter(@"C:\GerenciadorDeImpressao\conf.txt"))
            {
                foreach(var item in printers)
                {
                    writer.WriteLine(item.ToString().Trim());
                }
                writer.WriteLine(pathDB.Trim());
            }
        }

        public static StringCollection GetPrintersInArchive()
        {
            if (!Directory.Exists(@"C:\GerenciadorDeImpressao\") || !File.Exists(@"C:\GerenciadorDeImpressao\conf.txt"))
                return new StringCollection();

            StringCollection collection = new StringCollection();
            using (StreamReader reader = new StreamReader(@"C:\GerenciadorDeImpressao\conf.txt"))
            {
                while(!reader.EndOfStream)
                    collection.Add(reader.ReadLine().Trim());
            }

            return collection;
        }
    }
}
