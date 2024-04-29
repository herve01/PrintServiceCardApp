using PrintServiceCardApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrintServiceCardApp.Dao
{
    public class ProvinceDao : Dao<Province>
    {
        public ProvinceDao(DbConnection connection = null): base(connection)
        {
            TableName = "province";
        }

        public override int Add(Province instance)
        {
            return 0;
        }
        public override int Delete(Province instance)
        {
            return 0;
        }
        public override int Update(Province instance, Province oldObj)
        {
            return 0;
        }
        protected override Dictionary<string, object> Map(DbDataReader reader)
        {
            return new Dictionary<string, object>()
           {
               { "id", reader["id"] },
               { "nom", reader["nom"] }
           };
        }

        private Province Create(Dictionary<string, object> row, bool withZones = false)
        {
            Province instance = new Province();

            instance.Id = int.Parse(row["id"].ToString());
            instance.Nom = row["nom"].ToString();

            if (withZones)
                instance.Zones = new ZoneDao(Connection).GetAll(instance);

            return instance;
        }

        public Province Get(string id)
        {
            Province instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from province " +
                    "where id = @v_id";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));

                using (Reader = Request.ExecuteReader())
                {
                    if (Reader.HasRows && Reader.Read())
                        _instance = Map(Reader);

                    Reader.Close();
                };

                if (_instance != null)
                    instance = Create(_instance);
            }
            catch (Exception)
            {
                if (Reader != null)
                    Reader.Close();
            }

            return instance;
        }

        public List<Province> GetAll()
        {
            var intances = new List<Province>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from province ";

                Reader = Request.ExecuteReader();

                if (Reader.HasRows)
                    while (Reader.Read())
                        _instances.Add(Map(Reader));

                Reader.Close();

                foreach (var item in _instances)
                {
                    intances.Add(Create(item));
                }
            }
            catch (Exception)
            {
                if (Reader != null)
                    Reader.Close();
            }

            return intances;
        }

        public async Task<List<Province>> GetAllAsync(CancellationToken token)
        {
            var intances = new List<Province>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from province ";

                using (Reader = await Request.ExecuteReaderAsync())
                {
                    if (Reader.HasRows)
                        while (Reader.Read())
                            _instances.Add(Map(Reader));

                    Reader.Close();
                }

                int i = 0;

                foreach (var item in _instances)
                {
                    if (token.IsCancellationRequested)
                        break;

                    i++;
                    var province = Create(item);
                    province.Number = i;

                    intances.Add(province);
                }
            }
            catch (Exception)
            {
                if (Reader != null)
                    Reader.Close();

                //System.Windows.Forms.MessageBox.Show(e.Message);
            }

            return intances;
        }
        public async Task<List<Province>> GetAllAsync()
        {
            var intances = new List<Province>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from province ";

                using (Reader = await Request.ExecuteReaderAsync())
                {
                    if (Reader.HasRows)
                        while (Reader.Read())
                            _instances.Add(Map(Reader));

                    Reader.Close();
                }

                foreach (var item in _instances)
                {
                    var province = Create(item, true);
                    intances.Add(province);
                }
            }
            catch (Exception)
            {
                if (Reader != null)
                    Reader.Close();

                //System.Windows.Forms.MessageBox.Show(e.Message);
            }

            return intances;
        }

        public async Task<List<Province>> GetAll2Async(CancellationToken token)
        {
            var intances = new List<Province>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from province ";

                using (Reader = await Request.ExecuteReaderAsync())
                {
                    if (Reader.HasRows)
                        while (Reader.Read())
                            _instances.Add(Map(Reader));

                    Reader.Close();
                }

                foreach (var item in _instances)
                {
                    if (token.IsCancellationRequested)
                        break;

                    intances.Add(Create(item));
                }
            }
            catch (Exception)
            {
                if (Reader != null)
                    Reader.Close();

                //System.Windows.Forms.MessageBox.Show(e.Message);
            }

            return intances;
        }
    }
}
