using PrintServiceCardApp.Dao.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace PrintServiceCardApp.Dao
{
    public abstract class Dao<T>
    {
        public abstract int Add(T instance);
        public abstract int Delete(T instance);
        public abstract int Update(T newObj, T oldObj);

        protected abstract Dictionary<string, object> Map(DbDataReader row);

        protected DbConnection Connection;
        protected DbCommand Request;
        protected DbDataAdapter Adapter;
        protected DbDataReader Reader;
        protected DataTable Table;
        protected bool OwnAction = true;
        protected string TableName = string.Empty;

        public Dao(DbConnection connection = null)
        {
            try
            {
                Connection = connection ?? ConnectionHelper.GetConnection();
                InitProperties();
            }
            catch (Exception)
            {

            }
        }

        void InitProperties()
        {
            Request = Connection.CreateCommand();
            Adapter = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateDataAdapter();
            Table = new DataTable();

            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }

        public void NewConnection()
        {
            try
            {
                Connection = ConnectionHelper.GetNewInstance();
                InitProperties();
            }
            catch (Exception)
            {

            }
        }
    }

    public class DbUtil
    {
        public static DbParameter CreateParameter(DbCommand cmd, string paramName, DbType type, object value, ParameterDirection direction = ParameterDirection.Input)
        {
            var param = cmd.CreateParameter();
            param.ParameterName = paramName;
            param.DbType = type;
            param.Value = value;
            param.Direction = direction;

            return param;
        }

        public static DataTable DicToTable(List<Dictionary<string, object>> list)
        {
            DataTable result = new DataTable();
            if (list.Count == 0)
                return result;

            foreach (var entry in list[0])
                result.Columns.Add(new DataColumn(entry.Key, entry.Value.GetType()));

            foreach (var dic in list)
            {
                var row = new object[dic.Count];

                int k = 0;
                foreach (var entry in dic)
                    row[k++] = entry.Value;

                result.Rows.Add(row);
            }

            return result;
        }
    }

    public class TableKeyHelper
    {
        public static string GenerateKey(string tableName, string add = "")
        {
            var random = new Random();
            var key = "Herve" + tableName + DateTime.Now.ToUniversalTime().ToLongDateString() + " " + DateTime.Now.ToUniversalTime().ToLongTimeString() + add + random.Next(0, 10000);
            key += GetMacAddress();
            var md5 = MD5.Create();

            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString().ToUpper();
        }

        static string GetMacAddress()
        {
            byte[] macAddress = null;

            foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    macAddress = nic.GetPhysicalAddress().GetAddressBytes();
                    break;
                }
            }
            return string.Join(":", macAddress.Select(m => m.ToString("X2")));
        }

        static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
