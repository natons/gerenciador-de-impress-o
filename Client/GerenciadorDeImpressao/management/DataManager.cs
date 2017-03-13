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
        public static void SaveConfInArchive(bool flag, string pathDB)
        {
            if (!Directory.Exists(@"C:\GerenciadorDeImpressao\"))
                Directory.CreateDirectory(@"C:\GerenciadorDeImpressao\");

            using (var writer = new StreamWriter(@"C:\GerenciadorDeImpressao\conf.txt"))
            {
                writer.WriteLine(flag.ToString().Trim());
                writer.WriteLine(pathDB.Trim());
            }
        }

        public static bool GetConfInArchive()
        {
            if (!Directory.Exists(@"C:\GerenciadorDeImpressao\") || !File.Exists(@"C:\GerenciadorDeImpressao\conf.txt"))
                return false;

            using (var reader = new StreamReader(@"C:\GerenciadorDeImpressao\conf.txt"))
            {
                return Convert.ToBoolean(reader.ReadLine().Trim());
            }
        }

        public static string GetPathDB()
        {
            if (!Directory.Exists(@"C:\GerenciadorDeImpressao\") || !File.Exists(@"C:\GerenciadorDeImpressao\conf.txt"))
                return "";
            string s = "";
            using (var reader = new StreamReader(@"C:\GerenciadorDeImpressao\conf.txt"))
            {
                while (!reader.EndOfStream)
                    s = reader.ReadLine().Trim();
            }

            return s;
        }

        public static StringCollection GetPrintersInArchive(string path)
        {
            StringCollection collection = new StringCollection();
            using (StreamReader reader = new StreamReader(path + "conf.txt"))
            {
                while (!reader.EndOfStream)
                    collection.Add(reader.ReadLine().Trim());
            }

            collection.RemoveAt(collection.Count - 1);

            return collection;
        }
    }
}
