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
    public class AffectationDao : Dao<Affectation>
    {
        public AffectationDao(DbConnection connection = null): base(connection)
        {
            TableName = "affectation";
        }

        public override int Add(Affectation instance)
        {
            try
            {
                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into affectation(id, personnel_id, zone_id, date, created_at, updated_at) " +
                    "values(@v_id, @v_personnel_id, @v_zone_id, @v_date, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, instance.Personnel.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_zone_id", DbType.Int32, instance.Zone.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_date", DbType.Date, instance.Date));

                var feed = Request.ExecuteNonQuery();

                if (feed > 0)
                    instance.Id = id;

                return feed;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public int Add(Affectation instance, DbCommand command)
        {
            Request = command;
            Request.Parameters.Clear();

            return Add(instance);
        }
        public async Task<int> AddAsync(Affectation instance)
        {
            try
            {
                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into affectation(id, personnel_id, zone_id, date, created_at, updated_at) " +
                    "values(@v_id, @v_personnel_id, @v_zone_id, @v_date, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, instance.Personnel.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_zone_id", DbType.Int32, instance.Zone.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_date", DbType.Date, instance.Date));

                var feed = await Request.ExecuteNonQueryAsync();

                if (feed > 0)
                    instance.Id = id;

                return feed;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public async Task<int> AddAsync(Affectation instance, DbCommand command)
        {
            Request = command;
            Request.Parameters.Clear();

            return await AddAsync(instance);
        }
        public override int Delete(Affectation instance)
        {
            try
            {
                Request.CommandText = "delete from affectation where id = @v_id ";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public override int Update(Affectation instance, Affectation oldObj)
        {
            try
            {

                Request.CommandText = "update affectation " +
                    "set personnel_id = @v_personnel_id, " +
                    "zone_id = @v_zone_id, " +
                    "date = @v_date, " +
                    "updated_at = now() " +
                    "where id = @v_id;";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, instance.Personnel.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_zone_id", DbType.Int32, instance.Zone.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_date", DbType.Date, instance.Date));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        protected override Dictionary<string, object> Map(DbDataReader reader)
        {
            return new Dictionary<string, object>()
           {
               { "id", reader["id"] },
               { "zone_id", reader["zone_id"] },
               { "personnel_id", reader["personnel_id"] },
               { "date", reader["date"] }
           };
        }

        private Affectation Create(Dictionary<string, object> row, bool withPersonnel = false, bool withZone = false)
        {
            Affectation instance = new Affectation();

            instance.Id = row["id"].ToString();
            instance.Date = DateTime.Parse(row["date"].ToString());

            if (withPersonnel)
                instance.Personnel = new PersonnelDao(Connection).Get(row["personnel_id"].ToString());

            if (withZone)
                instance.Zone = new ZoneDao(Connection).Get(row["zone_id"].ToString());

            return instance;
        }

        public Affectation Get(string id)
        {
            Affectation instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from affectation " +
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
        public Affectation Get(Personnel personnel)
        {
            Affectation instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from affectation " +
                    "where personnel_id = @v_personnel_id";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, personnel.Id));

                using (Reader = Request.ExecuteReader())
                {
                    if (Reader.HasRows && Reader.Read())
                        _instance = Map(Reader);

                    Reader.Close();
                };

                if (_instance != null)
                {
                    instance = Create(_instance, false, true);
                    instance.Personnel = personnel;
                }
                   
            }
            catch (Exception)
            {
                if (Reader != null)
                    Reader.Close();
            }

            return instance;
        }
        public List<Affectation> GetAll()
        {
            var intances = new List<Affectation>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from affectation ";

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

        public async Task<List<Affectation>> GetAllAsync(CancellationToken token)
        {
            var intances = new List<Affectation>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from affectation ";

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
                    var affectation = Create(item);
                    affectation.Number = i;

                    intances.Add(affectation);
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

        public async Task<List<Affectation>> GetAll2Async(CancellationToken token)
        {
            var intances = new List<Affectation>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from affectation ";

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
