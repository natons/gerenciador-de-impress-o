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
       //MANAGE COMPANY
        public static List<Company> GetCompanies(string path)
        {
            SQLiteConnection connection = null;
            List<Company> prints = new List<Company>();
            try
            {
                connection = new Connection(path).CreateConnection();
                string sql = "SELECT * FROM Empresa";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prints.Add(
                        new Company(Int32.Parse(reader["id"].ToString()), reader["nome"].ToString())
                        );
                }
                reader.Close();
            } catch(SQLiteException e)
            {
                MessageBox.Show("Erro ao retornar impressoes!" + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return prints;
        }

        public static Company GetCompany(string path, string name)
        {
            SQLiteConnection connection = null;
            Company company = null;
            try
            {
                connection = new Connection(path).CreateConnection();
                string sql = "SELECT * FROM Empresa WHERE nome = '" + name.Trim() +"'";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {   
                    company = 
                        new Company(Int32.Parse(reader["id"].ToString()), reader["nome"].ToString());
                }
                reader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Erro ao retornar Empresas!" + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return company;
        }

        //MANAGE PRINT
        public static void InsertPrint(string pathArchive, Print print)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(pathArchive).CreateConnection();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText =
                    "INSERT INTO Impressao (id , nome_documento, qtd_paginas, custo, data, id_empresa, id_impressora) VALUES (null, '" + print.documentName + "', " 
                    + print.quantityPages + ", " + GetDoubleString(print.cost) +
                    ", DATETIME('NOW','LOCALTIME'), " + print.company.id + ", " + print.printer.id + ")";
                command.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Erro ao atualizar Banco de dados! " + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private static string GetDoubleString(double value)
        {
            if (value == 0)
                return value.ToString();
            string valueString = value.ToString().Substring(0, value.ToString().LastIndexOf(','));
            valueString += ".";
            valueString += value.ToString().Substring(value.ToString().LastIndexOf(',') + 1);

            return valueString;
        }


        //MANAGE PRINTER
        public static void InsertPrinter(string pathArchive, Printer printer)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(pathArchive).CreateConnection();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText =
                    "INSERT INTO Impressora VALUES (null, '" + printer.name + "', " + printer.priceToner +
                    ", " + 0 + ", "+ 0 + ", " + 0 + ")";
                command.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Erro ao atualizar Banco de dados! " + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        public static List<Printer> GetPrinters(string pathArchive)
        {
            SQLiteConnection connection = null;
            List<Printer> printers = new List<Printer>();
            try
            {
                connection = new Connection(pathArchive).CreateConnection();
                string sql = "SELECT * FROM Impressora";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    printers.Add(
                        new Printer(Int32.Parse(reader["id"].ToString()),
                        reader["nome"].ToString(),
                        reader.GetDouble(2),
                        reader.GetDouble(3),
                        reader.GetDouble(4),
                        Int32.Parse(reader["paginas_impressas"].ToString()))
                        );
                }
                reader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Erro ao retornar Impressoras!" + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return printers;
        }

        public static Printer GetPrinter(string pathArchive, string name, bool create)
        {
            SQLiteConnection connection = null;
            Printer printer = null;
            try
            {
                connection = new Connection(pathArchive).CreateConnection();
                string sql = "SELECT * FROM Impressora WHERE nome = '" + name.Trim() + "'";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    printer =
                        new Printer(Int32.Parse(reader["id"].ToString()),
                        reader["nome"].ToString(),
                        reader.GetDouble(2),
                        reader.GetDouble(3),
                        reader.GetDouble(4),
                        Int32.Parse(reader["paginas_impressas"].ToString()));
                    
                }
                else if(create == true)
                {
                    printer = new Printer();
                    printer.name = name;
                    printer.priceToner = 15.0;
                    printer.mediaPages = 20.0;

                    InsertPrinter(pathArchive, printer);
                }
                reader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Erro ao retornar Impressoras!" + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return printer;
        }

        public static void UpdatePrinter(string path, Printer printer)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(path).CreateConnection();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText =
                    @"UPDATE Impressora SET nome = @name, valor_toner = @priceToner," +
                    " media_paginas_anterior = @lastMediaPages, media_paginas = @mediaPages, paginas_impressas = @printedPages WHERE id = @id";
                command.Parameters.Add(new SQLiteParameter("@name", printer.name));
                command.Parameters.Add(new SQLiteParameter("@priceToner", printer.priceToner));
                printer.mediaPages = GetNewMediaPages(printer);
                command.Parameters.Add(new SQLiteParameter("@lastMediaPages", printer.lastMediaPages));
                command.Parameters.Add(new SQLiteParameter("@mediaPages", printer.mediaPages));
                command.Parameters.Add(new SQLiteParameter("@printedPages", printer.printedPages));
                command.Parameters.Add(new SQLiteParameter("@id", printer.id));
                command.ExecuteNonQuery();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Erro ao atualizar Banco de dados! " + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private static double GetNewMediaPages(Printer printer)
        {
            if (printer.printedPages == 0)
                return printer.mediaPages;

            return printer.priceToner / Double.Parse(printer.printedPages.ToString());
        }

        //ARCHIVE PRINTERS
        public static void SaveConfInArchive(bool flag, string pathDB)
        {
            if(!Directory.Exists(@"C:\GerenciadorDeImpressao\"))
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
