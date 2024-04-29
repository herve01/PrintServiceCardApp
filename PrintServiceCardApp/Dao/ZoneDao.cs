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
    public class ZoneDao : Dao<Zone>
    {
        public ZoneDao(DbConnection connection = null): base(connection)
        {
            TableName = "zone";
        }

        public override int Add(Zone instance)
        {
            return 0;
        }
        public override int Delete(Zone instance)
        {
            return 0;
        }
        public override int Update(Zone instance, Zone oldObj)
        {
            return 0;
        }
        protected override Dictionary<string, object> Map(DbDataReader reader)
        {
            return new Dictionary<string, object>()
           {
               { "id", reader["id"] },
               { "nom", reader["nom"] },
               { "type", reader["type"] },
               { "province_id", reader["province_id"] },
           };
        }

        private Zone Create(Dictionary<string, object> row, bool withProvince = false)
        {
            Zone instance = new Zone();

            instance.Id = int.Parse(row["id"].ToString());
            instance.Nom = row["nom"].ToString();
            instance.Type = row["type"].ToString();

            if (withProvince)
                instance.Province = new ProvinceDao(Connection).Get(row["province_id"].ToString());

            return instance;
        }

        public Zone Get(string id)
        {
            Zone instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from zone " +
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

        public List<Zone> GetAll()
        {
            var intances = new List<Zone>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from zone ";

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

        public List<Zone> GetAll(Province province)
        {
            var intances = new List<Zone>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from zone " +
                    "where province_id = @v_province_id";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_province_id", DbType.String, province.Id));

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

        public async Task<List<Zone>> GetAllAsync(CancellationToken token)
        {
            var intances = new List<Zone>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from zone ";

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
                    var zone = Create(item);
                    zone.Number = i;

                    intances.Add(zone);
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
        public async Task<List<Zone>> GetAllAsync()
        {
            var intances = new List<Zone>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from zone ";

                using (Reader = await Request.ExecuteReaderAsync())
                {
                    if (Reader.HasRows)
                        while (Reader.Read())
                            _instances.Add(Map(Reader));

                    Reader.Close();
                }

                foreach (var item in _instances)
                {
                    var zone = Create(item);
                    intances.Add(zone);
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

        public async Task<List<Zone>> GetAll2Async(CancellationToken token)
        {
            var intances = new List<Zone>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from zone ";

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
