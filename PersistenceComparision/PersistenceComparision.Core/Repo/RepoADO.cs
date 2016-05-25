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
        public void Create(TinyModel model)
        {
            var sql = new MySqlConnection(ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EFContext"].ConnectionString);

            try
            {
                sql.Open();

                var command = new MySqlCommand("INSERT INTO TinyModels (descricao) VALUES (@d); " +
                "SELECT id FROM TinyModels WHERE row_count() > 0 AND id = last_insert_id();", sql);
                command.Parameters.AddWithValue("@d", model.Descricao);

                object r = command.ExecuteScalar();

                model.Id = (int)r;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sql.Close();
            }
        }

        public void Delete(TinyModel model)
        {
            var sql = new MySqlConnection(ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EFContext"].ConnectionString);

            try
            {
                sql.Open();

                var command = new MySqlCommand("DELETE FROM TinyModels WHERE id = @id;", sql);
                command.Parameters.AddWithValue("@id", model.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sql.Close();
            }
        }

        public TinyModel Read(int id)
        {
            TinyModel returnValue = null;

            var sql = new MySqlConnection(ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EFContext"].ConnectionString);

            try
            {
                sql.Open();

                var command = new MySqlCommand("SELECT Id, Descricao FROM TinyModels WHERE id = @id;", sql);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    returnValue = new TinyModel();
                    returnValue.Id = (int)reader["Id"];
                    returnValue.Descricao = (string)reader["Descricao"];
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sql.Close();
            }

            return returnValue;
        }

        public void Update(TinyModel model)
        {
            var sql = new MySqlConnection(ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EFContext"].ConnectionString);

            try
            {
                sql.Open();

                var command = new MySqlCommand("UPDATE TinyModels SET descricao = @d WHERE id = @id;", sql);
                command.Parameters.AddWithValue("@d", model.Descricao);
                command.Parameters.AddWithValue("@id", model.Id);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sql.Close();
            }
        }
    }
}
