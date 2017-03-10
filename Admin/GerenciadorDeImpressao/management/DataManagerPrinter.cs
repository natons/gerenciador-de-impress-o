using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorDeImpressao.management
{
    static class DataManagerPrinter
    {
        public static void InsertPrinter(string pathArchive, Printer printer)
        {
            SQLiteConnection connection = null;
            try
            {
                connection = new Connection(pathArchive).CreateConnection();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText =
                    "INSERT INTO Impressora VALUES (null, '" + printer.name + "', " + printer.priceToner +
                    ", " + 0 + ", " + 0 + ", " + 0 + ")";
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
                else if (create == true)
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
    }
}
