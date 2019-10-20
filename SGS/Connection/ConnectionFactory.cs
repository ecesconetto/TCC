using MySql.Data.MySqlClient;
using SGS.Models;
using System.Data.Common;
using System.Data.SqlClient;

namespace SGS.Connection
{
    //Abertura da conexão com o banco de dados via dapper (micro ORM melhor que entity framework)
    public class ConnectionFactory
    {
        public static DbConnection OpenConnection()
        {
            var connection = new MySqlConnection(ConnectionStrings.Connection);

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            return connection;
        }
    }
}