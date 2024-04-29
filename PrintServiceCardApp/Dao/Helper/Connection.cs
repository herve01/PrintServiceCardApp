using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Dao.Helper
{
    public class ConnectionHelper
    {
        private static DbConnection _connection;

        public static DbConnection GetConnection()
        {
            if (_connection == null)
            {
                var connectionString = string.Format("server={0};user={1};password={2};database={3};port={4}",
                    DbConfig.ServerName,
                    DbConfig.DbUser,
                    DbConfig.DbPassword,
                    DbConfig.DbName,
                    DbConfig.DbPort);

                try
                {
                    _connection = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateConnection();
                    _connection.ConnectionString = connectionString;
                    _connection.Open();

                }
                catch (Exception)
                {
                    _connection = null;
                }
            }

            return _connection;
        }

        public static DbConnection GetNewInstance()
        {
            var connectionString = string.Format("server={0};user={1};password={2};database={3};port={4}",
                    DbConfig.ServerName,
                    DbConfig.DbUser,
                    DbConfig.DbPassword,
                    DbConfig.DbName,
                    DbConfig.DbPort);

            try
            {
                var connection = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateConnection();
                connection.ConnectionString = connectionString;
                connection.Open();

                return connection;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
