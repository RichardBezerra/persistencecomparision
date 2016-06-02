using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace PersistenceComparision.Core.Repo
{
    public class RepoADO : IRepo
    {
        private string ConnString { get { return ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EFContext"].ConnectionString; } }

        private void Execute(Func<MySqlConnection, object> action)
        {
            using (var sql = new MySqlConnection(this.ConnString))
            {
                try
                {
                    sql.Open();

                    action(sql);
                }
                finally
                {
                    sql.Close();
                }
            }
        }

        public void Create(TinyModel model)
        {
            Execute((MySqlConnection conn) =>
            {
                var command = new MySqlCommand("INSERT INTO TinyModel (descricao) VALUES (@d); " +
                   "SELECT id FROM TinyModel WHERE row_count() > 0 AND id = last_insert_id();", conn);

                command.Parameters.AddWithValue("@d", model.Descricao);

                object r = command.ExecuteScalar();

                model.Id = (int)r;

                return model.Id;
            });
        }

        public void Create(OneModel one)
        {
            Execute((MySqlConnection conn) =>
            {
                var commandOne = new MySqlCommand("INSERT INTO OneModel (One) VALUES (@o); " +
                   "SELECT id FROM OneModel WHERE row_count() > 0 AND id = last_insert_id();", conn);

                commandOne.Parameters.AddWithValue("@o", one.One);

                one.Id = (int)commandOne.ExecuteScalar();

                one.Many.ForEach((m) =>
                {
                    var commandMany = new MySqlCommand("INSERT INTO ManyModel (Many, OneModelId) VALUES (@m, @oid); " +
                   "SELECT id FROM ManyModel WHERE row_count() > 0 AND id = last_insert_id();", conn);

                    commandMany.Parameters.AddWithValue("@m", m.Many);
                    commandMany.Parameters.AddWithValue("@oid", one.Id);

                    m.Id =  (int)commandMany.ExecuteScalar();
                });

                return one.Id;
            });
        }
        
        public void Delete(TinyModel model)
        {
            Execute((MySqlConnection conn) =>
            {
                var command = new MySqlCommand("DELETE FROM TinyModel WHERE id = @id;", conn);
                command.Parameters.AddWithValue("@id", model.Id);
                command.ExecuteNonQuery();

                return null;
            });
        }

        public TinyModel ReadTinyModel(int id)
        {
            TinyModel returnValue = null;

            Execute((MySqlConnection conn) =>
            {
                var command = new MySqlCommand("SELECT Id, Descricao FROM TinyModel WHERE id = @id;", conn);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    returnValue = new TinyModel();
                    returnValue.Id = reader.GetInt32("Id");
                    returnValue.Descricao = reader.GetString("Descricao");
                }

                return returnValue;
            });

            return returnValue;
        }

        public void Update(TinyModel model)
        {
            Execute((MySqlConnection conn) =>
            {
                var command = new MySqlCommand("UPDATE TinyModel SET descricao = @d WHERE id = @id;", conn);
                command.Parameters.AddWithValue("@d", model.Descricao);
                command.Parameters.AddWithValue("@id", model.Id);
                command.ExecuteNonQuery();

                return null;
            });
        }
        
        public OneModel ReadOneModel(int id)
        {
            OneModel returnValue = null;

            Execute((MySqlConnection conn) =>
            {
                var commandOne = new MySqlCommand("SELECT Id, One FROM OneModel WHERE id = @id;", conn);
                commandOne.Parameters.AddWithValue("@id", id);
                var readerOne = commandOne.ExecuteReader();

                while (readerOne.Read())
                {
                    returnValue = new OneModel();
                    returnValue.Id = readerOne.GetInt32("Id");
                    returnValue.One = readerOne.GetString("One");
                }

                return returnValue;
            });

            

            return returnValue;
        }

        public void Update(OneModel model)
        {
            Execute((MySqlConnection conn) =>
            {
                var commandOne = new MySqlCommand("UPDATE OneModel SET One = @o WHERE Id = @id;", conn);

                commandOne.Parameters.AddWithValue("@o", model.One);
                commandOne.Parameters.AddWithValue("@id", model.Id);

                object r = commandOne.ExecuteScalar();

                model.Many.ForEach((m) =>
                {
                    var commandMany = new MySqlCommand("UPDATE ManyModel SET Many = @m WHERE Id = @id;", conn);

                    commandMany.Parameters.AddWithValue("@m", m.Many);
                    commandMany.Parameters.AddWithValue("@id", m.Id);

                    commandMany.ExecuteScalar();
                });

                return model;
            });
        }

        public void Delete(OneModel model)
        {
            Execute((MySqlConnection conn) =>
            {
                model.Many.ForEach(m =>
                {
                    var commandMany = new MySqlCommand("DELETE FROM ManyModel WHERE id = @id;", conn);
                    commandMany.Parameters.AddWithValue("@id", m.Id);
                    commandMany.ExecuteNonQuery();
                });

                var commandOne = new MySqlCommand("DELETE FROM OneModel WHERE id = @id;", conn);
                commandOne.Parameters.AddWithValue("@id", model.Id);
                commandOne.ExecuteNonQuery();

                return null;
            });
        }
    }
}
