using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

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

        public TinyModel Read(int id)
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
                    returnValue.Id = (int)reader["Id"];
                    returnValue.Descricao = (string)reader["Descricao"];
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
    }
}
