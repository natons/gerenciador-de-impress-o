using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorDeImpressao.management
{
    static class DataManagerCompany
    {
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
            }
            catch (SQLiteException e)
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
                string sql = "SELECT * FROM Empresa WHERE nome = '" + name.Trim() + "'";
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
    }
}
