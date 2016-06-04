using MySql.Data.MySqlClient;
using System;

namespace PersistenceComparision.Core.Repo.ADO
{
    public class RepoOneToManyADO : RepoBaseADO<OneModel>
    {
        protected override OneModel Convert(MySqlDataReader reader)
        {
            var returnValue = new OneModel();
            returnValue.Id = reader.GetInt32("Id");
            returnValue.One = reader.GetString("One");

            return returnValue;
        }

        protected override MySqlCommand GenerateDeleteCommand(OneModel model)
        {
            var commandMany = new MySqlCommand("DELETE FROM ManyModel WHERE id = @id;");

            commandMany.Parameters.AddWithValue("@id", model.Id);

            return commandMany;
        }

        protected override MySqlCommand GenerateInsertCommand(OneModel model)
        {
            var commandOne = new MySqlCommand("INSERT INTO OneModel (One) VALUES (@o); " +
                  "SELECT id FROM OneModel WHERE row_count() > 0 AND id = last_insert_id();");

            commandOne.Parameters.AddWithValue("@o", model.One);

            return commandOne;
        }

        protected override MySqlCommand GenerateSelectByIdCommand(int id)
        {
            var commandOne = new MySqlCommand("SELECT Id, One FROM OneModel WHERE id = @id;");

            commandOne.Parameters.AddWithValue("@id", id);

            return commandOne;
        }

        protected override MySqlCommand GenerateUpdateCommand(OneModel model)
        {
            var commandOne = new MySqlCommand("UPDATE OneModel SET One = @o WHERE Id = @id;");

            commandOne.Parameters.AddWithValue("@o", model.One);

            commandOne.Parameters.AddWithValue("@id", model.Id);

            return commandOne;
        }

        public override void Create(OneModel model)
        {
            base.Create(model);

            Execute((MySqlConnection conn) =>
            {
                model.Many.ForEach((m) =>
                {
                    var commandMany = new MySqlCommand("INSERT INTO ManyModel (Many, OneModelId) VALUES (@m, @oid); " +
                   "SELECT id FROM ManyModel WHERE row_count() > 0 AND id = last_insert_id();", conn);

                    commandMany.Parameters.AddWithValue("@m", m.Many);
                    commandMany.Parameters.AddWithValue("@oid", model.Id);

                    m.Id = (int)commandMany.ExecuteScalar();
                });

                return model;
            });
        }

        public override void Delete(OneModel model)
        {
            Execute((MySqlConnection conn) =>
            {
                var commandOne = new MySqlCommand("DELETE FROM OneModel WHERE id = @id;", conn);
                commandOne.Parameters.AddWithValue("@id", model.Id);
                commandOne.ExecuteNonQuery();

                return null;
            });

            base.Delete(model);
        }

        public override void Update(OneModel model)
        {
            base.Update(model);

            Execute((MySqlConnection conn) =>
            {
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

        public override OneModel FindById(int id)
        {
            OneModel returnValue = base.FindById(id);

            Execute((MySqlConnection conn) =>
            {
                var commandOne = new MySqlCommand("SELECT Id, Many, OneModelId FROM ManyModel WHERE OneModelId = @id;", conn);
                commandOne.Parameters.AddWithValue("@id", id);
                var readerOne = commandOne.ExecuteReader();

                while (readerOne.Read())
                {
                    var returnMany = new ManyModel();
                    returnMany.Id = readerOne.GetInt32("Id");
                    returnMany.Many = readerOne.GetString("Many");
                    returnMany.OneModelId = readerOne.GetInt32("OneModelId");

                    returnValue.Many.Add(returnMany);
                }

                return returnValue;
            });

            return returnValue;
        }
    }
}