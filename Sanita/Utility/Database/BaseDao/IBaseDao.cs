namespace Sanita.Utility.Database.BaseDao
{
    using Sanita.Utility.Database.Utility;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public interface IBaseDao
    {
        int DoCreateDatabase(string sql);
        IDataReader DoGetDataReader(string sql);
        IDataReader DoGetDataReader(IDbConnection connection, IDbTransaction trans, string sql);
        DataRow DoGetDataRow(string sql);
        DataRow DoGetDataRow(IDbConnection connection, IDbTransaction trans, string sql);
        DataSet DoGetDataSet(string sql);
        DataTable DoGetDataTable(string sql);
        DataTable DoGetDataTable(IDbConnection connection, IDbTransaction trans, string sql);
        int DoInsert(string sql);
        int DoInsert(IDbConnection connection, IDbTransaction trans, string sql);
        void DoNotification(string chanel, string data);
        DataTable DoShowDatabase(string sql);
        int DoUpdate(string sql);
        int DoUpdate(IDbConnection connection, IDbTransaction trans, string sql);
        IDbConnection GetConnection();
        string GetConnectionString();
        string GetConnectionString_Database();
        string GetConnectionString_Host();
        string GetDatabaseName();
        string GetEcriptKey();
        //void InitNotification(IList<string> mListChannel, OnDatabaseNotificationHandler mCallback);
        void SetConnectionConfig(string host, string user, string password, string database, string port);
        //void SetDatabaseImplementUtility(DatabaseImplementUtility utility);
        int UpdateSimple(string sql);
    }
}

