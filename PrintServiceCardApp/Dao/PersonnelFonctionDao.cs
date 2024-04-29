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
    public class PersonnelFonctionDao : Dao<PersonnelFonction>
    {
        public PersonnelFonctionDao(DbConnection connection = null): base(connection)
        {
            TableName = "personnel_fonction";
        }
        public override int Add(PersonnelFonction instance)
        {
            try
            {
                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into personnel_fonction(id, fonction_id, personnel_id, date, created_at, updated_at) " +
                    "values(@v_id, @v_fonction_id, @v_personnel_id, @v_date, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_fonction_id", DbType.String, instance.Fonction.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, instance.Personnel.Id));
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
        public int Add(PersonnelFonction instance, DbCommand command)
        {
            Request = command;
            Request.Parameters.Clear();

            return Add(instance);
        }
        public async Task<int> AddAsync(PersonnelFonction instance)
        {
            try
            {
                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into personnel_fonction(id, fonction_id, personnel_id, date, created_at, updated_at) " +
                    "values(@v_id, @v_fonction_id, @v_personnel_id, @v_date, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_fonction_id", DbType.String, instance.Fonction.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, instance.Personnel.Id));
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
        public async Task<int> AddAsync(PersonnelFonction instance, DbCommand command)
        {
            Request = command;
            Request.Parameters.Clear();

            return await AddAsync(instance);
        }

        public override int Delete(PersonnelFonction instance)
        {
            try
            {
                Request.CommandText = "delete from personnel_fonction where id = @v_id ";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public override int Update(PersonnelFonction instance, PersonnelFonction oldObj)
        {
            try
            {

                Request.CommandText = "update personnel_fonction " +
                    "set fonction_id = @v_fonction_id, " +
                    "personnel_id = @v_personnel_id, " +
                    "date = @v_date, " +
                    "updated_at = now() " +
                    "where id = @v_id;";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, instance.Personnel.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_fonction_id", DbType.String, instance.Fonction.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_date", DbType.Date, instance.Date));

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
               { "personnel_id", reader["personnel_id"] },
               { "fonction_id", reader["fonction_id"] },
               { "date", reader["date"] }
           };
        }

        private PersonnelFonction Create(Dictionary<string, object> row, bool withFonction = false, bool withPersonnel = false)
        {
            PersonnelFonction instance = new PersonnelFonction();

            instance.Id = row["id"].ToString();
            
            instance.Date =DateTime.Parse(row["date"].ToString());

            if (withFonction)
                instance.Fonction = new FonctionDao(Connection).Get(row["fonction_id"].ToString());

            if(withPersonnel)
                instance.Personnel = new PersonnelDao(Connection).Get(row["personnel_id"].ToString());

            return instance;
        }

        public PersonnelFonction Get(string id)
        {
            PersonnelFonction instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from personnel_fonction " +
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
        public PersonnelFonction Get(Personnel personnel)
        {
            PersonnelFonction instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from personnel_fonction " +
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
                    instance = Create(_instance, true);
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
        public List<PersonnelFonction> GetAll()
        {
            var intances = new List<PersonnelFonction>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from personnel_fonction ";

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

        public async Task<List<PersonnelFonction>> GetAllAsync(CancellationToken token)
        {
            var intances = new List<PersonnelFonction>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from personnel_fonction ";

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
                    var fonction = Create(item);
                    fonction.Number = i;

                    intances.Add(fonction);
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

        public async Task<List<PersonnelFonction>> GetAll2Async(CancellationToken token)
        {
            var intances = new List<PersonnelFonction>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from personnel_fonction ";

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
