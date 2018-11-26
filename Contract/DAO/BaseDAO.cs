using Npgsql;
using System;
using System.Data;
using Sanita.Utility.Logger;
using Sanita.Utility.Database.BaseDao;
using MediboxDemo.Core.Database;

namespace Contract.DAO
{
    public class BaseDAO
    {

        private const string TAG = "BaseDB";
        protected object lockObject = new object();

        protected IBaseDao baseDAO =>
            ContractManagerDatabaseUtility.GetDatabaseDAO();

        public NpgsqlConnection GetConnection()
        {
            try
            {
                string host = "localhost";
                string port = "5432";
                string user = "postgres";
                string password = "Thu123456!";
                string dataBaseName = "smartcontract";
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", host, port, user, password, dataBaseName);
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                return conn;
            }
            catch (Exception e)
            {
                // something went wrong, and you wanna know why
                SanitaLog.Exception(e);
                throw e;
            }
        }
    }
}
