using Npgsql;
using Sanita.Utility.Logger;
using System;
using System.Data;

namespace Sanita.Utility.Database.BaseDao
{
    public class NpgsqlBaseDao : IBaseDao
    {
        public bool DataBase_Ready = false;

        private object lockDBObject = new object();
        private string LocalConnectionString = "";
        private string Localserver = "localhost";
        private string Localdatabase = "smartcontract";
        private string Localuserid = "postgres";
        private string Localpassword = "Thu123456!";
        private string Localport = "5432";
        //private string PrivateKey = "";

        public void SetConnectionConfig(string host, string user, string password, string database, string port)
        {
            this.Localserver = host;
            this.Localdatabase = database;
            this.Localuserid = user;
            this.Localpassword = password;
            this.Localport = port;
        }

        public string GetConnectionString()
        {
            lock (this.lockDBObject)
            {
                //this.LocalConnectionString = $"server={this.Localserver};UserId={this.Localuserid}; Password={this.Localpassword}; Database={this.Localdatabase};port={this.Localport};SYNCNOTIFICATION=true";
                this.LocalConnectionString = $"server={this.Localserver};UserId={this.Localuserid}; Password={this.Localpassword}; Database={this.Localdatabase};port={this.Localport};";
                return this.LocalConnectionString;
            }
        }

        public int Update(string _sql)
        {
            int num3;
            lock (this.lockDBObject)
            {
                Exception exception;
                string str = _sql;
                try
                {
                    bool flag2;
                    int num = 0;
                    goto Label_0108;
                    Label_0020:
                    using (NpgsqlConnection connection = this.CreateConnection())
                    {
                        using (NpgsqlCommand command = new NpgsqlCommand())
                        {
                            try
                            {
                                command.Connection = connection;
                                command.CommandText = str;
                                command.CommandTimeout = 0x3e8;
                                connection.Open();
                                return command.ExecuteNonQuery();
                            }
                            catch (Exception exception1)
                            {
                                exception = exception1;
                                SanitaLog.Exception(exception);
                            }
                            num++;
                            if (num > 3)
                            {
                                SanitaLog.Error("NpgsqlBaseDao" + "Update('" + str + "') error !");
                                return -100;
                            }
                            SanitaLog.Error("NpgsqlBaseDao", "Try Update N = " + num.ToString());
                        }
                    }
                    Label_0108:
                    flag2 = true;
                    goto Label_0020;
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    SanitaLog.Error("NpgsqlBaseDao", "Update('" + str + "') error !");
                    SanitaLog.Exception(exception);
                    num3 = -100;
                }
            }
            return num3;
        }

        public NpgsqlConnection CreateConnection() =>
            new NpgsqlConnection(this.GetConnectionString());

        public int DoUpdate(IDbConnection connection, IDbTransaction trans, string sql)
        {
            if (connection != null)
            {
                return this.Update(connection, trans, sql);
            }
            return this.Update(sql);
        }

        public int Update(IDbConnection connection, IDbTransaction trans, string _sql)
        {
            int num2;
            string str = _sql;
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection as NpgsqlConnection;
                    command.Transaction = trans as NpgsqlTransaction;
                    command.CommandText = str;
                    command.CommandTimeout = 30;
                    num2 = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                SanitaLog.Error("NpgsqlBaseDao", "Update('" + str + "') error !");
                SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback: --START");
                SanitaLog.Exception(exception);
                try
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                        SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback OK");
                    }
                    if (connection != null)
                    {
                        connection.Close();
                        SanitaLog.Error("NpgsqlBaseDao", "=>Do close connection OK");
                    }
                }
                catch (Exception exception2)
                {
                    SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback NG");
                    SanitaLog.Exception(exception2);
                }
                SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback: --END");
                num2 = -100;
            }
            return num2;
        }

        public DataRow GetDataRow(string sql)
        {
            try
            {
                DataRow row = null;
                DataTable dataTable = this.GetDataTable(sql);
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        row = dataTable.Rows[0];
                    }
                    else
                    {
                        dataTable.Rows.Add(new object[] { 0 });
                        row = dataTable.Rows[0];
                    }
                }
                return row;
            }
            catch (Exception exception)
            {
                SanitaLog.Exception(exception);
                return null;
            }
        }

        public DataRow DoGetDataRow(IDbConnection connection, IDbTransaction trans, string sql)
        {
            if (connection != null)
            {
                return this.GetDataRow(connection, trans, sql);
            }
            return this.GetDataRow(sql);
        }

        public DataTable GetDataTable(string sql)
        {
            Exception exception;
            DataTable table2;
            try
            {
                bool flag;
                int num = 0;
                DataTable dataTable = new DataTable();
                goto Label_0118;
                Label_0010:
                using (NpgsqlConnection connection = this.CreateConnection())
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "SET enable_seqscan = off; " + sql;
                        command.CommandTimeout = 0x3e8;
                        try
                        {
                            using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                            {
                                adapter.SelectCommand = command;
                                connection.Open();
                                adapter.Fill(dataTable);
                                connection.Close();
                                return dataTable;
                            }
                        }
                        catch (Exception exception1)
                        {
                            exception = exception1;
                            SanitaLog.Error("NpgsqlBaseDao" + "GetDataTable 1 error !");
                            SanitaLog.Exception(exception1);
                        }
                        num++;
                        if (num > 5)
                        {
                            SanitaLog.Error("NpgsqlBaseDao" + "GetDataTable 2 error ! TryMax > 5");
                            return null;
                        }
                    }
                }
                Label_0118:
                flag = true;
                goto Label_0010;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                SanitaLog.Exception(exception2);
                table2 = null;
            }
            return table2;
        }

        public DataRow GetDataRow(IDbConnection connection, IDbTransaction trans, string sql)
        {
            try
            {
                DataRow row = null;
                DataTable table = this.GetDataTable(connection, trans, sql);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    row = table.Rows[0];
                }
                return row;
            }
            catch (Exception exception)
            {
                SanitaLog.Error("NpgsqlBaseDao" + "GetDataRow 1 error !");
                SanitaLog.Exception(exception);
                return null;
            }
        }

        public DataTable GetDataTable(IDbConnection connection, IDbTransaction trans, string sql)
        {
            Exception exception;
            DataTable table2;
            try
            {
                bool flag;
                int num = 0;
                DataTable dataTable = new DataTable();
                goto Label_00EF;
                Label_0010:
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection as NpgsqlConnection;
                    command.Transaction = trans as NpgsqlTransaction;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sql;
                    command.CommandTimeout = 0x3e8;
                    try
                    {
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                    catch (Exception exception1)
                    {
                        exception = exception1;
                        SanitaLog.Exception(exception1);
                    }
                    num++;
                    if (num > 5)
                    {
                        SanitaLog.Error("NpgsqlBaseDao" + "GetDataTable 2 error ! TryMax > 5");
                        return null;
                    }
                }
                Label_00EF:
                flag = true;
                goto Label_0010;
            }
            catch (Exception exception2)
            {
                exception = exception2;
                SanitaLog.Error("NpgsqlBaseDao" + "GetDataTable 3 error");
                SanitaLog.Exception(exception2);
                table2 = null;
            }
            return table2;
        }

        public int DoCreateDatabase(string sql)
        {
            throw new NotImplementedException();
        }

        public IDataReader DoGetDataReader(string sql)
        {
            throw new NotImplementedException();
        }

        public IDataReader DoGetDataReader(IDbConnection connection, IDbTransaction trans, string sql)
        {
            throw new NotImplementedException();
        }

        public DataRow DoGetDataRow(string sql) =>
            this.GetDataRow(sql);

        public DataSet DoGetDataSet(string sql) =>
             this.GetDataSet(sql);

        public DataSet GetDataSet(string sql)
        {
            DataSet set2;
            try
            {
                using (NpgsqlConnection connection = this.CreateConnection())
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = sql;
                        command.CommandTimeout = 0x3e8;
                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
                        {
                            adapter.SelectCommand = command;
                            DataSet dataSet = new DataSet();
                            connection.Open();
                            adapter.Fill(dataSet);
                            connection.Close();
                            set2 = dataSet;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                SanitaLog.Error("NpgsqlBaseDao", "GetDataSet error !");
                SanitaLog.Exception(exception);
                set2 = null;
            }
            return set2;
        }

        public DataTable DoGetDataTable(string sql) =>
            this.GetDataTable(sql);

        public DataTable DoGetDataTable(IDbConnection connection, IDbTransaction trans, string sql)
        {
            if (connection != null)
            {
                return this.GetDataTable(connection, trans, sql);
            }
            return this.GetDataTable(sql);
        }

        public int Insert(string _sql)
        {
            int num;
            string sql = _sql;
            try
            {
                using (NpgsqlConnection connection = this.CreateConnection())
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = sql;
                        command.CommandTimeout = 30;
                        connection.Open();
                        num = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exception)
            {
                SanitaLog.Error("NpgsqlBaseDao", "Insert('" + sql + "') error !");
                SanitaLog.Exception(exception);
                num = -100;
            }
            return num;
        }

        public int Insert(IDbConnection connection, IDbTransaction trans, string _sql)
        {
            int num;
            string sql = _sql;
            try
            {
                using (NpgsqlCommand command = new NpgsqlCommand())
                {
                    command.Connection = connection as NpgsqlConnection;
                    command.Transaction = trans as NpgsqlTransaction;
                    command.CommandText = sql;
                    command.CommandTimeout = 30;
                    num = command.ExecuteNonQuery();
                }
            }
            catch (Exception exception)
            {
                SanitaLog.Error("NpgsqlBaseDao", "Insert('" + sql + "') error !");
                SanitaLog.Exception(exception);
                SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback: --START");
                try
                {
                    if (trans != null)
                    {
                        trans.Rollback();
                        SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback OK");
                    }
                    if (connection != null)
                    {
                        connection.Close();
                        SanitaLog.Error("NpgsqlBaseDao", "=>Do close connection OK");
                    }
                }
                catch (Exception exception2)
                {
                    SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback NG");
                    SanitaLog.Exception(exception2);
                }
                SanitaLog.Error("NpgsqlBaseDao", "=>Do rollback: --END");
                num = -100;
            }
            return num;
        }

        public int DoInsert(string sql) =>
            this.Insert(sql);

        public int DoInsert(IDbConnection connection, IDbTransaction trans, string sql)
        {
            if (connection != null)
            {
                return this.Insert(connection, trans, sql);
            }
            return this.Insert(sql);
        }

        public void DoNotification(string chanel, string data)
        {
            throw new NotImplementedException();
        }

        public DataTable DoShowDatabase(string sql)
        {
            throw new NotImplementedException();
        }

        public int DoUpdate(string sql) =>
            this.Update(sql);

        public string GetConnectionString_Database() =>
           this.Localdatabase.ToLower();

        public string GetConnectionString_Host() =>
           this.Localserver.ToLower();

        public string GetDatabaseName()
        {
            lock (this.lockDBObject)
            {
                if (this.DataBase_Ready)
                {
                    if (this.Localdatabase != "")
                    {
                        return this.Localdatabase;
                    }
                    this.DataBase_Ready = false;
                }
                if (!this.DataBase_Ready)
                {
                    this.GetConnectionString();
                }
                return this.Localdatabase;
            }
        }

        public string GetEcriptKey()
        {
            throw new NotImplementedException();
        }

        public int UpdateSimple(string sql)
        {
            throw new NotImplementedException();
        }

        public IDbConnection GetConnection() =>
            this.CreateConnection();
    }
}
