using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GerenciadorDeImpressao
{
    class Connection
    {
        string pathDataBase;
       

        public Connection(string pathDataBase)
        {
            this.pathDataBase = pathDataBase;
        }

        public SQLiteConnection CreateConnection()
        {
            SQLiteConnection m_dbConnection = null;
            try
            {
                m_dbConnection = new SQLiteConnection(@"Data Source=" + pathDataBase);
                m_dbConnection.Open();
            } catch(SQLiteException e)
            {
                MessageBox.Show("Erro ao se conectar ao banco de dados! " + e.Message);
            }

            return m_dbConnection;
        }
    }
}
