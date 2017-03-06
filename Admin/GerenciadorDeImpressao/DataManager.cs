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

        public static void UpdateCompany(string path, Company company)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(path).CreateConnection();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = 
                    @"UPDATE Empresa SET nome = @name WHERE id = @id";
                command.Parameters.Add(new SQLiteParameter("@name", company.name));
                command.Parameters.Add(new SQLiteParameter("@id", company.id));
                command.ExecuteNonQuery();
            }
            catch(SQLiteException e)
            {
                MessageBox.Show("Erro ao atualizar Banco de dados! " + e.Message);
            } finally
            {
                if (connection != null)
                    connection.Close();
            }
        }
                
        public static void RemoveCompany(string path, int id)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(path).CreateConnection();
                SQLiteCommand command = new SQLiteCommand("DELETE FROM Empresa WHERE id = " + id, connection);
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

        public static void InsertCompany(string pathArchive, Company company)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(pathArchive).CreateConnection();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText =
                    "INSERT INTO Empresa VALUES(null,'" + company.name + "')";
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
            catch (Exception e)
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
        
        public static List<Print> GetPrintsToCompany(string path, int id)
        {
            SQLiteConnection connection = null;
            List<Print> prints = new List<Print>();
            try
            {
                connection = new Connection(path).CreateConnection();
                string sql =
                    "SELECT imp.id, imp.nome_documento, imp.qtd_paginas, imp.custo, imp.data " +
                    "FROM Impressao imp INNER JOIN Empresa e ON e.id = imp.id_empresa AND" + " e.id = " + id +
                    " INNER JOIN Impressora i ON i.id = imp.id_impressora AND strftime('%m', imp.data) = strftime('%m', DATETIME('NOW'))";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    prints.Add(
                        new Print(Int32.Parse(reader["id"].ToString()),
                        reader["nome_documento"].ToString(),
                        Int32.Parse(reader["qtd_paginas"].ToString()),
                        reader.GetDouble(3),
                        reader.GetDateTime(4))
                        );
                }
                reader.Close();
            }
            catch (SQLiteException e)
            {
                MessageBox.Show("Erro ao retornar Impressoes!" + e.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return prints;
        }

        public static DataSet GetDataSetToGrid(string pathArchive, int idCompany)
        {
            List<Company> prints = DataManager.GetCompanies(pathArchive);
            DataSet dataSet = new DataSet();
            string sql =
                "SELECT e.nome, i.nome, imp.nome_documento, imp.qtd_paginas, imp.data, imp.custo " +
                "FROM Impressao imp INNER JOIN Empresa e ON e.id = imp.id_empresa AND" + " e.id = " + idCompany +
                " INNER JOIN Impressora i ON i.id = imp.id_impressora";
            SQLiteDataAdapter dataAdapter =
                new SQLiteDataAdapter(sql, new Connection(pathArchive).CreateConnection());
            dataAdapter.Fill(dataSet);

            return dataSet;
        }

        public static DataSet GetDataSetToGrid(string pathArchive, int idCompany, int idPrinter, string dateInit, string dateFinish)
        {
            List<Company> prints = DataManager.GetCompanies(pathArchive);
            DataSet dataSet = new DataSet();
            string company = idCompany == 0 ? "" : "AND e.id = " + idCompany;
            string printer = idPrinter == 0 ? "" : "AND i.id = " + idPrinter; 
            string sql =
                "SELECT e.nome, i.nome, imp.nome_documento, imp.qtd_paginas, imp.data, imp.custo " +
                "FROM Impressao imp INNER JOIN Empresa e ON e.id = imp.id_empresa " + company +
                " INNER JOIN Impressora i ON i.id = imp.id_impressora " + printer + " AND (imp.data BETWEEN '" + VerifyDate(dateInit) 
                + "' AND '" + VerifyDate(dateFinish) + "') ORDER BY e.nome ASC";
            SQLiteDataAdapter dataAdapter =
                new SQLiteDataAdapter(sql, new Connection(pathArchive).CreateConnection());
            dataAdapter.Fill(dataSet);

            return dataSet;
        }

        private static string VerifyDate(String date)
        {

            if(date.LastIndexOf('\\') > 0 || date.LastIndexOf('/') > 0)
            {
                string newDate = date.Substring(0, 4);
                newDate += "-";
                newDate += date.Substring(5, 2);
                newDate += "-";
                newDate += date.Substring(8, 2);
                return newDate;
            }

            return date;
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

        public static void RemovePrinter(string path, int id)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(path).CreateConnection();
                SQLiteCommand command = new SQLiteCommand("DELETE FROM Impressora WHERE id = " + id, connection);
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
