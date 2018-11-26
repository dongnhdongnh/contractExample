namespace Sanita.Utility.Database.Utility
{
    using Sanita.Utility;
    using Sanita.Utility.Database.BaseDao;
    using Sanita.Utility.Logger;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class DatabaseUtility
    {
        private const string TAG = "DatabaseUtility";
        private IBaseDao _baseDAO = null;
        //private DatabaseImplementUtility _DatabaseUtility = null;

        //public bool CheckConnection(string host, string user, string password, string database, string port)
        //{
        //    try
        //    {
        //        this.GetDatabaseDAO().SetConnectionConfig(host, user, password, database, port);
        //        if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
        //        {
        //            StringBuilder builder = new StringBuilder();
        //            builder.Append(" SELECT NOW() AS TIME_NOW ");
        //            return (this.GetDatabaseDAO().DoGetDataRow(builder.ToString()) != null);
        //        }
        //        return ((GetDatabaseType() == DATABASE_TYPE.SQLITE) && File.Exists(@"Database\" + database + ".db"));
        //    }
        //    catch (Exception exception)
        //    {
        //        SanitaLog.e("DatabaseUtility", "Check connection error !", exception);
        //        return false;
        //    }
        //}

        //public static string Escape(bool s)
        //{
        //    if (s)
        //    {
        //        return "'1'";
        //    }
        //    return "'0'";
        //}

        //public static string Escape(IList<int> s) => 
        //    ("(" + string.Join(",", (from p in s select p.ToString()).ToArray<string>()) + ")");

        //public static string Escape(byte[] s)
        //{
        //    if ((GetDatabaseType() == DATABASE_TYPE.POSTGRESQL) || (GetDatabaseType() == DATABASE_TYPE.SQLITE))
        //    {
        //        return SanitaUtility.ConvertBinary2HexString_POSTGRES(s);
        //    }
        //    return SanitaUtility.ConvertBinary2HexString_MYSQL(s);
        //}

        //public static string Escape(DateTime s) => 
        //    $"'{s:yyyy-MM-dd HH:mm:ss}'";

        //public static string Escape(double s) => 
        //    ("'" + s.ToString(CultureInfo.InvariantCulture) + "'");

        //public static string Escape(int s) => 
        //    ("'" + s.ToString() + "'");

        //public static string Escape(long s) => 
        //    ("'" + s.ToString() + "'");

        //public static string Escape(string s)
        //{
        //    if (string.IsNullOrEmpty(s))
        //    {
        //        if ((GetDatabaseType() == DATABASE_TYPE.POSTGRESQL) || (GetDatabaseType() == DATABASE_TYPE.SQLITE))
        //        {
        //            return "''";
        //        }
        //        return "NULL";
        //    }
        //    return ("'" + s.Replace("'", "''") + "'");
        //}

        //public static string Escape(ulong s)
        //{
        //    if (s == 0L)
        //    {
        //        return "NULL";
        //    }
        //    DateTime time = DateTime.FromBinary((long) s);
        //    return $"'{time:yyyy-MM-dd HH:mm:ss}'";
        //}

        //public DateTime GetCurrentTime()
        //{
        //    StringBuilder builder = new StringBuilder();
        //    if (GetDatabaseType() == DATABASE_TYPE.POSTGRESQL)
        //    {
        //        builder.Append(" SELECT NOW()::TIMESTAMP WITHOUT TIME ZONE AS TIME_NOW ");
        //    }
        //    else if (GetDatabaseType() == DATABASE_TYPE.MYSQL)
        //    {
        //        builder.Append(" SELECT NOW() AS TIME_NOW ");
        //    }
        //    else
        //    {
        //        return DateTime.Now;
        //    }
        //    DataRow row = this.GetDatabaseDAO().DoGetDataRow(builder.ToString());
        //    DateTime result = new DateTime();
        //    if (row != null)
        //    {
        //        if (row["TIME_NOW"] != DBNull.Value)
        //        {
        //            DateTime.TryParse(row["TIME_NOW"].ToString(), out result);
        //        }
        //        else
        //        {
        //            SanitaLogEx.e("X100", "GetCurrentTime.row.TIME = null");
        //        }
        //    }
        //    else
        //    {
        //        SanitaLogEx.e("X100", "GetCurrentTime.row = null");
        //    }
        //    if (result.Year > 0x7d1)
        //    {
        //        UtilityDate.SetLocalTime(result);
        //    }
        //    return result;
        //}

        public IBaseDao GetDatabaseDAO()
        {
            if (this._baseDAO == null)
            {
                this._baseDAO = new NpgsqlBaseDao();
            }
            return this._baseDAO;
        }

        //public DatabaseImplementUtility GetDatabaseImplementUtility()
        //{
        //    if (this._DatabaseUtility == null)
        //    {
        //        this._DatabaseUtility = new NpgsqlDatabaseUtility();
        //    }
        //    return this._DatabaseUtility;
        //}

        //public static DATABASE_TYPE GetDatabaseType() => 
        //    SystemInfo.DatabaseType;

        //public void SetConnectionConfig(string host, string user, string password, string database, string port)
        //{
        //    this._baseDAO = null;
        //    this.GetDatabaseDAO().SetConnectionConfig(host, user, password, database, port);
        //}

        //public enum DATABASE_TYPE
        //{
        //    MYSQL,
        //    POSTGRESQL,
        //    MSSQL,
        //    SQLITE
        //}
    }
}

