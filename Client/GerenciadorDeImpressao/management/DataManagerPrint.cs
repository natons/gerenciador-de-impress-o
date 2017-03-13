using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorDeImpressao.management
{
    static class DataManagerPrint
    {
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
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(value.ToString().Substring(0, value.ToString().LastIndexOf(',')));
            stringBuilder.Append(".");
            stringBuilder.Append(value.ToString().Substring(value.ToString().LastIndexOf(',') + 1));

            return stringBuilder.ToString();
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
            List<Company> prints = DataManagerCompany.GetCompanies(pathArchive);
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
            List<Company> prints = DataManagerCompany.GetCompanies(pathArchive);
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

            if (date.LastIndexOf('\\') > 0 || date.LastIndexOf('/') > 0)
            {
                var newDate = new StringBuilder();
                newDate.Append(date.Substring(0, 4));
                newDate.Append("-");
                newDate.Append(date.Substring(5, 2));
                newDate.Append("-");
                newDate.Append(date.Substring(8, 2));
                return newDate.ToString();
            }

            return date;
        }
    }
}
