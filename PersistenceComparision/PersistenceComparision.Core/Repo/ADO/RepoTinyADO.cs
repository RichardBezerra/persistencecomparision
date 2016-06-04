using MySql.Data.MySqlClient;

namespace PersistenceComparision.Core.Repo.ADO
{
    public class RepoTinyADO : RepoBaseADO<TinyModel>
    {
        protected override TinyModel Convert(MySqlDataReader reader)
        {
            var returnValue = new TinyModel();
            returnValue.Id = reader.GetInt32("Id");
            returnValue.Descricao = reader.GetString("Descricao");

            return returnValue;
        }

        protected override MySqlCommand GenerateDeleteCommand(TinyModel model)
        {
            var command = new MySqlCommand("DELETE FROM TinyModel WHERE id = @id;");

            command.Parameters.AddWithValue("@id", model.Id);

            return command;
        }

        protected override MySqlCommand GenerateInsertCommand(TinyModel model)
        {
            var command = new MySqlCommand("INSERT INTO TinyModel (descricao) VALUES (@d); " +
                   "SELECT id FROM TinyModel WHERE row_count() > 0 AND id = last_insert_id();");

            command.Parameters.AddWithValue("@d", model.Descricao);

            return command;
        }

        protected override MySqlCommand GenerateSelectByIdCommand(int id)
        {
            var command = new MySqlCommand("SELECT Id, Descricao FROM TinyModel WHERE id = @id;");

            command.Parameters.AddWithValue("@id", id);

            return command;
        }

        protected override MySqlCommand GenerateUpdateCommand(TinyModel model)
        {
            var command = new MySqlCommand("UPDATE TinyModel SET descricao = @d WHERE id = @id;");

            command.Parameters.AddWithValue("@d", model.Descricao);

            command.Parameters.AddWithValue("@id", model.Id);

            return command;
        }
    }
}
