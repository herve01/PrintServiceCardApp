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
    public class PersonnelGradeDao : Dao<PersonnelGrade>
    {
        public PersonnelGradeDao(DbConnection connection = null): base(connection)
        {
            TableName = "personnel_grade";
        }

        public override int Add(PersonnelGrade instance)
        {
            try
            {
                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into personnel_grade(id, grade_id, personnel_id, date, created_at, updated_at) " +
                    "values(@v_id, @v_grade_id, @v_personnel_id, @v_date, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_grade_id", DbType.String, instance.Grade.Id));
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
        public int Add(PersonnelGrade instance, DbCommand command)
        {
            Request = command;
            Request.Parameters.Clear();

            return Add(instance);
        }
        public async Task<int> AddAsync(PersonnelGrade instance)
        {
            try
            {
                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into personnel_grade(id, grade_id, personnel_id, date, created_at, updated_at) " +
                    "values(@v_id, @v_grade_id, @v_personnel_id, @v_date, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_grade_id", DbType.String, instance.Grade.Id));
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
        public async Task<int> AddAsync(PersonnelGrade instance, DbCommand command)
        {
            Request = command;
            Request.Parameters.Clear();

            return await AddAsync(instance);
        }
        public override int Delete(PersonnelGrade instance)
        {
            try
            {
                Request.CommandText = "delete from personnel_grade where id = @v_id ";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {

                return -1;
            }

        }
        public override int Update(PersonnelGrade instance, PersonnelGrade oldObj)
        {
            try
            {

                Request.CommandText = "update personnel_grade " +
                    "set grade_id = @v_grade_id, " +
                    "personnel_id = @v_personnel_id, " +
                    "date = @v_date, " +
                    "updated_at = now() " +
                    "where id = @v_id;";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, instance.Personnel.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_grade_id", DbType.String, instance.Grade.Id));
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
               { "grade_id", reader["grade_id"] },
               { "date", reader["date"] }
           };
        }

        private PersonnelGrade Create(Dictionary<string, object> row, bool withGrade = false, bool withPersonnel = false)
        {
            PersonnelGrade instance = new PersonnelGrade();

            instance.Id = row["id"].ToString();
            instance.Date = DateTime.Parse(row["date"].ToString());

            if (withGrade)
                instance.Grade = new GradeDao(Connection).Get(row["grade_id"].ToString());

            if (withPersonnel)
                instance.Personnel = new PersonnelDao(Connection).Get(row["personnel_id"].ToString());

            return instance;
        }

        public PersonnelGrade Get(string id)
        {
            PersonnelGrade instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from personnel_grade " +
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
        public PersonnelGrade Get(Personnel personnel)   
        {
            PersonnelGrade instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from personnel_grade " +
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
        public List<PersonnelGrade> GetAll()
        {
            var intances = new List<PersonnelGrade>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from personnel_grade ";

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

        public async Task<List<PersonnelGrade>> GetAllAsync(CancellationToken token)
        {
            var intances = new List<PersonnelGrade>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from personnel_grade ";

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

        public async Task<List<PersonnelGrade>> GetAll2Async(CancellationToken token)
        {
            var intances = new List<PersonnelGrade>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from personnel_grade ";

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
