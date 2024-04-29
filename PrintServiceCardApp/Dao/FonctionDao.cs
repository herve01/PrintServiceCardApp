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
    public class FonctionDao : Dao<Fonction>
    {
        public FonctionDao(DbConnection connection = null): base(connection)
        {
            TableName = "fonction";
        }
        public override int Add(Fonction instance)
        {
            try
            {
                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into fonction(id, grade_id, intitule, description, created_at, updated_at) " +
                    "values(@v_id, @v_grade_id, @v_intitule, @v_description, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_grade_id", DbType.String, instance.Grade.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_intitule", DbType.String, instance.Intitule));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_description", DbType.String, instance.Description));

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
        public override int Delete(Fonction instance)
        {
            try
            {
                Request.CommandText = "delete from fonction where id = @v_id ";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public override int Update(Fonction instance, Fonction oldObj)
        {
            try
            {

                Request.CommandText = "update fonction " +
                    "set grade_id = @v_grade_id, " +
                    "intitule = @v_intitule, " +
                    "description = @v_description, " +
                    "updated_at = now() " +
                    "where id = @v_id;";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_intitule", DbType.String, instance.Intitule));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_grade_id", DbType.String, instance.Grade.Id));
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
               { "grade_id", reader["grade_id"] },
               { "description", reader["description"] }
           };
        }

        private Fonction Create(Dictionary<string, object> row, bool withGrade = false)
        {
            Fonction instance = new Fonction();

            instance.Id = row["id"].ToString();
            instance.Intitule = row["intitule"].ToString();
            instance.Description = row["description"].ToString();

            if (withGrade)
                instance.Grade = new GradeDao(Connection).Get(row["grade_id"].ToString());

            return instance;
        }

        public Fonction Get(string id)
        {
            Fonction instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from fonction " +
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

        public List<Fonction> GetAll()
        {
            var intances = new List<Fonction>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from fonction ";

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
        public async Task<List<Fonction>> GetAllAsync(CancellationToken token)
        {
            var intances = new List<Fonction>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from fonction ";

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
                    var fonction = Create(item, true);
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
        public async Task<List<Fonction>> GetAllAsync(Grade grade)
        {
            var intances = new List<Fonction>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from fonction " +
                    "where grade_id = @v_grade_id ";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_grade_id", DbType.String, grade.Id));

                using (Reader = await Request.ExecuteReaderAsync())
                {
                    if (Reader.HasRows)
                        while (Reader.Read())
                            _instances.Add(Map(Reader));

                    Reader.Close();
                }

                foreach (var item in _instances)
                {
                    var fonction = Create(item);
                    fonction.Grade = grade;
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
        public async Task<List<Fonction>> GetAll2Async(CancellationToken token)
        {
            var intances = new List<Fonction>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from fonction ";

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
