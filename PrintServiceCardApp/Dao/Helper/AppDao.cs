using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Dao.Helper
{
    public class AppDao
    {
        public static object CreateConnection(string server, string user, string pwd, string port)
        {
            try
            {

                DbConnection connection = null ;

                string connectionString = String.Format("server={0};user={1};password={2};port={3};", server, user, pwd, port);

                connection = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateConnection();
                connection.ConnectionString = connectionString;

                connection.Open();

                return connection;
            }
            catch (Exception ex)
            {
                return "La connexion a échoué. Veuillez vérifier vos paramètres.\n" + ex.Message;
            }
        }

        public static async Task<List<string>> GetDatabases(DbConnection connection = null)
        {
            var list = new List<string>();

            try
            {
                var requete = connection.CreateCommand();
                requete.CommandText = "show databases";

                var reader = await requete.ExecuteReaderAsync();

                if (reader.HasRows)
                    while (reader.Read())
                        list.Add(reader["Database"].ToString());

                reader.Close();
            }
            catch (Exception)
            {
            }

            return list;
        }

        public static bool TestDatabase(DbConnection connection = null, string dbname = null)
        {
            try
            {
                var requete = connection.CreateCommand();
                requete.CommandText = "select * from " + dbname + ".gestion_frais_academique_db_info";

                var reader = requete.ExecuteReader();

                var exists = reader.HasRows;

                reader.Close();

                return exists;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
