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
    public class GradeDao : Dao<Grade>
    {
        public GradeDao(DbConnection connection = null): base(connection)
        {
            TableName = "grade";
        }

        public override int Add(Grade instance)
        {
            try
            {
                Request.CommandText = "insert into grade(id, intitule, type, niveau, description, created_at, updated_at) " +
                    "values(@v_id, @v_intitule, @v_type, @v_niveau, @v_description, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_intitule", DbType.String, instance.Intitule));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_type", DbType.String, instance.Type));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_niveau", DbType.Int32, instance.Niveau));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_description", DbType.String, instance.Description));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        public override int Delete(Grade instance)
        {
            try
            {
                Request.CommandText = "delete from Grade where id = @v_id ";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public override int Update(Grade instance, Grade oldObj)
        {
            try
            {

                Request.CommandText = "update grade " +
                    "set type = @v_type, " +
                    "intitule = @v_intitule, " +
                    "niveau = @v_niveau, " +
                    "description = @v_description, " +
                    "updated_at = now() " +
                    "where id = @v_id;";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_intitule", DbType.String, instance.Intitule));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_type", DbType.String, instance.Type));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_niveau", DbType.Int32, instance.Niveau));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_description", DbType.String, instance.Description));

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
               { "intitule", reader["intitule"] },
               { "type", reader["type"] },
               { "niveau", reader["niveau"] },
               { "description", reader["description"] }
           };
        }

        private Grade Create(Dictionary<string, object> row)
        {
            Grade instance = new Grade();

            instance.Id = row["id"].ToString();
            instance.Intitule = row["intitule"].ToString();
            instance.Type = row["type"].ToString();
            instance.Niveau = int.Parse(row["niveau"].ToString());
            instance.Description = row["description"].ToString();

            return instance;
        }

        public Grade Get(string id)
        {
            Grade instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from grade " +
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

        public List<Grade> GetAll()
        {
            var intances = new List<Grade>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from grade ";

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
        public async Task<List<Grade>> GetAllAsync()
        {
            var intances = new List<Grade>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from grade ";

                using (Reader = await Request.ExecuteReaderAsync())
                {
                    if (Reader.HasRows)
                        while (Reader.Read())
                            _instances.Add(Map(Reader));

                    Reader.Close();
                }

                foreach (var item in _instances)
                {
                    var grade = Create(item);
                    intances.Add(grade);
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
        public async Task<List<Grade>> GetAllAsync(CancellationToken token)
        {
            var intances = new List<Grade>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from grade " +
                                    "order by niveau desc";

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
                    var grade = Create(item);
                    grade.Number = i;

                    intances.Add(grade);
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

        public async Task<List<Grade>> GetAll2Async(CancellationToken token)
        {
            var intances = new List<Grade>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from grade ";

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
