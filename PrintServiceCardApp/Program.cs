using PrintServiceCardApp.Dao;
using PrintServiceCardApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintServiceCardApp
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DbConfig.DbName = PrintServiceCardApp.Properties.Settings.Default.local_dbname;
            DbConfig.DbUser = PrintServiceCardApp.Properties.Settings.Default.local_user;
            DbConfig.DbPassword = PrintServiceCardApp.Properties.Settings.Default.local_pwd;
            DbConfig.ServerName = PrintServiceCardApp.Properties.Settings.Default.local_server;
            DbConfig.DbPort = PrintServiceCardApp.Properties.Settings.Default.local_port;

            if (Dao.Helper.ConnectionHelper.GetConnection() != null)
            {
                var name = new Dao.SettingDao().GetSetting("chef_signature");

                if(name != null)
                {
                    AppConfig.ChefSignature = name.Value.ToString();
                }         
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
