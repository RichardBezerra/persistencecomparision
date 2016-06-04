using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace PersistenceComparision.Core.Repo.ADO
{
    public abstract class RepoBaseADO<T> : Repo<T> where T : ModelBase
    {
        private string ConnString { get { return ConfigurationManager.ConnectionStrings["PersistenceComparision.Core.Repo.EF.EFContext"].ConnectionString; } }

        protected void Execute(Func<MySqlConnection, object> action)
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

        protected abstract MySqlCommand GenerateInsertCommand(T model);

        protected abstract MySqlCommand GenerateUpdateCommand(T model);

        protected abstract MySqlCommand GenerateDeleteCommand(T model);

        protected abstract MySqlCommand GenerateSelectByIdCommand(int id);

        protected abstract T Convert(MySqlDataReader reader);

        public virtual void Create(T model)
        {
            Execute((MySqlConnection conn) =>
            {   
                var command = GenerateInsertCommand(model);

                command.Connection = conn;

                object r = command.ExecuteScalar();

                model.Id = (int)r;

                return model.Id;
            });
        }
        
        public virtual void Delete(T model)
        {
            Execute((MySqlConnection conn) =>
            {
                var command = GenerateDeleteCommand(model);

                command.Connection = conn;
                
                return command.ExecuteScalar();
            });

        }

        public virtual T FindById(int id)
        {
            T returnValue = null;

            Execute((MySqlConnection conn) =>
            {
                var command = GenerateSelectByIdCommand(id);

                command.Connection = conn;

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    returnValue = Convert(reader);
                }

                return returnValue;
            });

            return returnValue;
        }

        public IEnumerable<T> Read()
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T model)
        {
            Execute((MySqlConnection conn) =>
            {
                var command = GenerateUpdateCommand(model);

                command.Connection = conn;

                return command.ExecuteScalar();
            });
        }
    }
}
