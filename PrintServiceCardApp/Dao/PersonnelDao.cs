using PrintServiceCardApp.Dao.Helper;
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
    public class PersonnelDao : Dao<Personnel>
    {
        public PersonnelDao(DbConnection connection = null) : base(connection)
        {
            TableName = "personnel";

        }

        public override int Add(Personnel instance)
        {
            try
            {
                Request.Transaction = Connection.BeginTransaction();

                var id = TableKeyHelper.GenerateKey(TableName);

                Request.CommandText = "insert into personnel(id, matricule, nom, postnom, prenom, sexe, photo, telephone, numero_qr_code, created_at, updated_at) " +
                    "values(@v_id, @v_matricule, @v_nom, @v_postnom, @v_prenom, @v_sexe, @v_photo, @v_telephone, @v_numero_qr_code, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_matricule", DbType.String, instance.Matricule));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_nom", DbType.String, instance.Nom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_postnom", DbType.String, instance.Postnom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_prenom", DbType.String, instance.Prenom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_sexe", DbType.String, instance.Sexe));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_telephone", DbType.String, instance.Telephone));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_photo", DbType.Binary, instance.Photo));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_numero_qr_code", DbType.String, instance.QrCodeStr));

                var feed = Request.ExecuteNonQuery();

                if (feed <= 0)
                {
                    Request.Transaction.Rollback();
                    return -1;
                }

                instance.Id = id;

                if (new PersonnelGradeDao().Add(instance.CurrentGrade, Request) <= 0)
                {
                    Request.Transaction.Rollback();
                    return -2;
                }

                if (new PersonnelFonctionDao().Add(instance.CurrentFonction, Request) <= 0)
                {
                    Request.Transaction.Rollback();
                    return -3;
                }

                if (new AffectationDao().Add(instance.Affectation, Request) <= 0)
                {
                    Request.Transaction.Rollback();
                    return -4;
                }
                Request.Transaction.Commit();

                return feed;
            }
            catch (Exception)
            {
                Request.Transaction.Rollback();
                return -1;
            }
        }

        public async Task<int> AddAsync(Personnel instance)
        {
            try
            {
                Request.Transaction = Connection.BeginTransaction();

                var id = TableKeyHelper.GenerateKey(TableName);


                Request.CommandText = "insert into personnel(id, matricule, nom, postnom, prenom, sexe, photo, telephone, numero_qr_code, created_at, updated_at) " +
                    "values(@v_id, @v_matricule, @v_nom, @v_postnom, @v_prenom, @v_sexe, @v_photo, @v_telephone, @v_numero_qr_code, now(), now())";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_matricule", DbType.String, instance.Matricule));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_nom", DbType.String, instance.Nom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_postnom", DbType.String, instance.Postnom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_prenom", DbType.String, instance.Prenom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_sexe", DbType.String, instance.Sexe));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_telephone", DbType.String, instance.Telephone));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_photo", DbType.Binary, instance.Photo));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_numero_qr_code", DbType.String, instance.QrCodeStr));

                var feed = await Request.ExecuteNonQueryAsync();

                if (feed <= 0)
                {
                    Request.Transaction.Rollback();
                    return -1;
                }

                instance.Id = id;

                if (await new PersonnelGradeDao().AddAsync(instance.CurrentGrade, Request) <= 0)
                {
                    Request.Transaction.Rollback();
                    return -2;
                }

                if (await new  PersonnelFonctionDao().AddAsync(instance.CurrentFonction, Request) <= 0)
                {
                    Request.Transaction.Rollback();
                    return -3;
                }

                if (await new AffectationDao().AddAsync(instance.Affectation, Request) <= 0)
                {
                    Request.Transaction.Rollback();
                    return -4;
                }
                Request.Transaction.Commit();

                return feed;
            }
            catch (Exception)
            {
                Request.Transaction.Rollback();
                return -10;
            }
        }

        public override int Delete(Personnel instance)
        {
            try
            {
                Request.CommandText = "delete from  personnel where id = @v_id ";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));

                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {

                return -1;
            }
           
        }
        
        public override int Update(Personnel instance, Personnel oldObj)
        {
            try
            {

                Request.CommandText = "update personnel " +
                    "set nom = @v_nom, " +
                    "matricule = @v_matricule, " +
                    "postnom = @v_postnom, " +
                    "prenom = @v_prenom, " +
                    "sexe = @v_sexe, " +
                    "telephone = @v_telephone, " +
                    "photo = @v_photo," +
                    "numero_qr_code = @v_numero_qr_code " +
                    "where id = @v_id";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, instance.Id));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_matricule", DbType.String, instance.Matricule));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_nom", DbType.String, instance.Nom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_postnom", DbType.String, instance.Postnom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_prenom", DbType.String, instance.Prenom));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_sexe", DbType.String, instance.Sexe));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_telephone", DbType.String, instance.Telephone));
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_photo", DbType.Binary, instance.Photo));
                              Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_numero_qr_code", DbType.String, instance.QrCodeStr));


                var feed = Request.ExecuteNonQuery();

                return feed;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        protected override Dictionary<string, object> Map(DbDataReader row)
        {
            return new Dictionary<string, object>()
            {
                { "id", row["id"] },
                { "matricule", row["matricule"] },
                { "nom", row["nom"] },
                { "postnom", row["postnom"] },
                { "prenom", row["prenom"] },
                { "sexe", row["sexe"] },
                { "telephone", row["telephone"] },
                { "photo", row["photo"] },
                { "numero_qr_code", row["numero_qr_code"] },
            };
        }
        
        private Personnel Create(Dictionary<string, object> row)
        {
            Personnel instance = new Personnel();

            instance.Id = row["id"].ToString();
            instance.Nom = row["nom"].ToString();
            instance.Postnom = row["postnom"].ToString();
            instance.Prenom = row["prenom"].ToString();
            instance.Matricule = row["matricule"].ToString();
            instance.Sexe = Util.ToSexeType(row["sexe"].ToString());
            instance.Telephone = row["telephone"].ToString();
            instance.QrCodeStr = row["numero_qr_code"].ToString();

            if (!(row["photo"] is DBNull))
                instance.Photo = (byte[])row["photo"];

            instance.CurrentFonction = new PersonnelFonctionDao(Connection).Get(instance);
            instance.CurrentGrade = new PersonnelGradeDao(Connection).Get(instance);
            instance.Affectation = new AffectationDao(Connection).Get(instance);

            return instance;
        }
        
        public Personnel Get(string id)
        {
            Personnel instance = null;
            Dictionary<string, object> _instance = null;

            try
            {
                Request.CommandText = "select * " +
                    "from personnel " +
                    "where id = @v_id";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_id", DbType.String, id));

                Reader = Request.ExecuteReader();

                if (Reader.HasRows && Reader.Read())
                    _instance = Map(Reader);

                Reader.Close();

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

        public string GetCurrentNumero()
        {
            try
            {
                Request.CommandText = "select id_senaceepef " +
                    "from personnel " +
                    "order create_at desc " +
                    "limit 1";

                return Request.ExecuteScalar().ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Personnel> GetAll()
        {
            var intances = new List<Personnel>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select * from personnel ";

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

        public async Task<List<Personnel>> GetAllAsync(Zone zone, CancellationToken token)
        {
            var intances = new List<Personnel>();
            var _instances = new List<Dictionary<string, object>>();

            try
            {
                Request.CommandText = "select P.* from personnel P " +
                    "inner join affectation A " +
                    "on P.id = A.personnel_id " +
                    "where A.zone_id = @v_zone_id";

                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_zone_id", DbType.String, zone.Id));

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

                    var personnel = Create(item);
                    intances.Add(personnel);
                }
            }
            catch (Exception)
            {
                if (Reader != null)
                    Reader.Close();
            }

            return intances;
        }

        public async Task<DataTable> GetCarteServiceReportAsync(Zone zone)
        {
            var list = new List<Dictionary<string, object>>();
            try
            {
                Request.CommandText = "get_carte_services";
                Request.CommandType = CommandType.StoredProcedure;
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_zone_id", DbType.Int32, zone.Id));

                Reader = await Request.ExecuteReaderAsync();

                if (Reader.HasRows)
                    while (Reader.Read())
                        list.Add(new Dictionary<string, object>()
                        {
                            { "matricule", Reader["matricule"] },
                            { "nom", Reader["nom"] },
                            { "postnom", Reader["postnom"] },
                            { "prenom", Reader["prenom"] },
                            { "sexe", Reader["sexe"] },
                            { "grade", Reader["grade"] },
                            { "fonction", Reader["fonction"] },
                            { "affectation", Reader["affectation"] },                        
                            { "photo", Reader["photo"] },
                            { "qrcode", null },
                            { "labelQr", Reader["labelQr"] },
                        });

                Reader.Close();

                list.ForEach(d => d["qrcode"] = Model.Helper.ImageUtil.GetCodeQR(d["labelQr"].ToString()));

                return DbUtil.DicToTable(list);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public async Task<DataTable> GetCarteServiceReportAsync(Personnel personnel)
        {
            var list = new List<Dictionary<string, object>>();
            try
            {
                Request.CommandText = "get_carte_personnel_services";
                Request.CommandType = CommandType.StoredProcedure;
                Request.Parameters.Add(DbUtil.CreateParameter(Request, "@v_personnel_id", DbType.String, personnel.Id));

                Reader = await Request.ExecuteReaderAsync();

                if (Reader.HasRows)
                    while (Reader.Read())
                        list.Add(new Dictionary<string, object>()
                        {
                            { "matricule", Reader["matricule"] },
                            { "nom", Reader["nom"] },
                            { "postnom", Reader["postnom"] },
                            { "prenom", Reader["prenom"] },
                            { "sexe", Reader["sexe"] },
                            { "grade", Reader["grade"] },
                            { "fonction", Reader["fonction"] },
                            { "affectation", Reader["affectation"] },
                            { "photo", Reader["photo"] },
                            { "qrcode", null },
                            { "labelQr", Reader["labelQr"] },
                        });

                Reader.Close();

                list.ForEach(d => d["qrcode"] = Model.Helper.ImageUtil.GetCodeQR(d["labelQr"].ToString()));

                return DbUtil.DicToTable(list);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
    }
}
