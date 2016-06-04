using MySql.Data.MySqlClient;

namespace PersistenceComparision.Core.Repo.ADO
{
    public class RepoLargeADO : RepoBaseADO<LargeModel>
    {
        protected override LargeModel Convert(MySqlDataReader reader)
        {
            var returnValue = new LargeModel();

            returnValue.Id = reader.GetInt32("Id");
            returnValue.Large = reader.GetString("Large");
            returnValue.LargeDescription2 = reader.GetString("LargeDescription2");
            returnValue.LargeDescription3 = reader.GetString("LargeDescription3");
            returnValue.LargeDescription4 = reader.GetString("LargeDescription4");
            returnValue.LargeDescription5 = reader.GetString("LargeDescription5");
            returnValue.LargeDescription6 = reader.GetString("LargeDescription6");
            returnValue.LargeDescription7 = reader.GetString("LargeDescription7");
            returnValue.LargeDescription8 = reader.GetString("LargeDescription8");
            returnValue.LargeDescription9 = reader.GetString("LargeDescription9");
            returnValue.LargeDescription10 = reader.GetString("LargeDescription10");
            returnValue.LargeDescription11 = reader.GetString("LargeDescription11");
            returnValue.LargeDescription12 = reader.GetString("LargeDescription12");
            returnValue.LargeDescription13 = reader.GetString("LargeDescription13");
            returnValue.LargeDescription14 = reader.GetString("LargeDescription14");
            returnValue.LargeDescription15 = reader.GetString("LargeDescription15");
            returnValue.LargeDescription16 = reader.GetString("LargeDescription16");
            returnValue.LargeDescription17 = reader.GetString("LargeDescription17");
            returnValue.LargeDescription18 = reader.GetString("LargeDescription18");
            returnValue.LargeDescription19 = reader.GetString("LargeDescription19");
            returnValue.LargeDescription20 = reader.GetString("LargeDescription20");

            return returnValue;
        }

        protected override MySqlCommand GenerateDeleteCommand(LargeModel model)
        {
            var command = new MySqlCommand("DELETE FROM LargeModel WHERE id = @id;");

            command.Parameters.AddWithValue("@id", model.Id);

            return command;
        }

        protected override MySqlCommand GenerateInsertCommand(LargeModel model)
        {
            string fields = "Large,";
            fields += "LargeDescription2,";
            fields += "LargeDescription3,";
            fields += "LargeDescription4,";
            fields += "LargeDescription5,";
            fields += "LargeDescription6,";
            fields += "LargeDescription7,";
            fields += "LargeDescription8,";
            fields += "LargeDescription9,";
            fields += "LargeDescription10,";
            fields += "LargeDescription11,";
            fields += "LargeDescription12,";
            fields += "LargeDescription13,";
            fields += "LargeDescription14,";
            fields += "LargeDescription15,";
            fields += "LargeDescription16,";
            fields += "LargeDescription17,";
            fields += "LargeDescription18,";
            fields += "LargeDescription19,";
            fields += "LargeDescription20";

            var command = new MySqlCommand("INSERT INTO LargeModel (" + fields + ") VALUES (@d,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20); " +
                  "SELECT id FROM LargeModel WHERE row_count() > 0 AND id = last_insert_id();");

            command.Parameters.AddWithValue("@d", model.Large);
            command.Parameters.AddWithValue("@d2", model.LargeDescription2);
            command.Parameters.AddWithValue("@d3", model.LargeDescription3);
            command.Parameters.AddWithValue("@d4", model.LargeDescription4);
            command.Parameters.AddWithValue("@d5", model.LargeDescription5);
            command.Parameters.AddWithValue("@d6", model.LargeDescription6);
            command.Parameters.AddWithValue("@d7", model.LargeDescription7);
            command.Parameters.AddWithValue("@d8", model.LargeDescription8);
            command.Parameters.AddWithValue("@d9", model.LargeDescription9);
            command.Parameters.AddWithValue("@d10", model.LargeDescription10);
            command.Parameters.AddWithValue("@d11", model.LargeDescription11);
            command.Parameters.AddWithValue("@d12", model.LargeDescription12);
            command.Parameters.AddWithValue("@d13", model.LargeDescription13);
            command.Parameters.AddWithValue("@d14", model.LargeDescription14);
            command.Parameters.AddWithValue("@d15", model.LargeDescription15);
            command.Parameters.AddWithValue("@d16", model.LargeDescription16);
            command.Parameters.AddWithValue("@d17", model.LargeDescription17);
            command.Parameters.AddWithValue("@d18", model.LargeDescription18);
            command.Parameters.AddWithValue("@d19", model.LargeDescription19);
            command.Parameters.AddWithValue("@d20", model.LargeDescription20);

            return command;
        }

        protected override MySqlCommand GenerateSelectByIdCommand(int id)
        {
            string fields = "Large,";
            fields += "LargeDescription2,";
            fields += "LargeDescription3,";
            fields += "LargeDescription4,";
            fields += "LargeDescription5,";
            fields += "LargeDescription6,";
            fields += "LargeDescription7,";
            fields += "LargeDescription8,";
            fields += "LargeDescription9,";
            fields += "LargeDescription10,";
            fields += "LargeDescription11,";
            fields += "LargeDescription12,";
            fields += "LargeDescription13,";
            fields += "LargeDescription14,";
            fields += "LargeDescription15,";
            fields += "LargeDescription16,";
            fields += "LargeDescription17,";
            fields += "LargeDescription18,";
            fields += "LargeDescription19,";
            fields += "LargeDescription20";

            var command = new MySqlCommand("SELECT Id, " + fields + "  FROM LargeModel WHERE id = @id;");

            command.Parameters.AddWithValue("@id", id);

            return command;
        }

        protected override MySqlCommand GenerateUpdateCommand(LargeModel model)
        {
            string fields = "Large=@d,";
            fields += "LargeDescription2=@d2,";
            fields += "LargeDescription3=@d3,";
            fields += "LargeDescription4=@d4,";
            fields += "LargeDescription5=@d5,";
            fields += "LargeDescription6=@d6,";
            fields += "LargeDescription7=@d7,";
            fields += "LargeDescription8=@d8,";
            fields += "LargeDescription9=@d9,";
            fields += "LargeDescription10=@d10,";
            fields += "LargeDescription11=@d11,";
            fields += "LargeDescription12=@d12,";
            fields += "LargeDescription13=@d13,";
            fields += "LargeDescription14=@d14,";
            fields += "LargeDescription15=@d15,";
            fields += "LargeDescription16=@d16,";
            fields += "LargeDescription17=@d17,";
            fields += "LargeDescription18=@d18,";
            fields += "LargeDescription19=@d19,";
            fields += "LargeDescription20=@d20";

            var command = new MySqlCommand("UPDATE LargeModel SET " + fields + " WHERE id = @id;");

            command.Parameters.AddWithValue("@id", model.Id);

            command.Parameters.AddWithValue("@d", model.Large);
            command.Parameters.AddWithValue("@d2", model.LargeDescription2);
            command.Parameters.AddWithValue("@d3", model.LargeDescription3);
            command.Parameters.AddWithValue("@d4", model.LargeDescription4);
            command.Parameters.AddWithValue("@d5", model.LargeDescription5);
            command.Parameters.AddWithValue("@d6", model.LargeDescription6);
            command.Parameters.AddWithValue("@d7", model.LargeDescription7);
            command.Parameters.AddWithValue("@d8", model.LargeDescription8);
            command.Parameters.AddWithValue("@d9", model.LargeDescription9);
            command.Parameters.AddWithValue("@d10", model.LargeDescription10);
            command.Parameters.AddWithValue("@d11", model.LargeDescription11);
            command.Parameters.AddWithValue("@d12", model.LargeDescription12);
            command.Parameters.AddWithValue("@d13", model.LargeDescription13);
            command.Parameters.AddWithValue("@d14", model.LargeDescription14);
            command.Parameters.AddWithValue("@d15", model.LargeDescription15);
            command.Parameters.AddWithValue("@d16", model.LargeDescription16);
            command.Parameters.AddWithValue("@d17", model.LargeDescription17);
            command.Parameters.AddWithValue("@d18", model.LargeDescription18);
            command.Parameters.AddWithValue("@d19", model.LargeDescription19);
            command.Parameters.AddWithValue("@d20", model.LargeDescription20);

            return command;
        }
    }
}
