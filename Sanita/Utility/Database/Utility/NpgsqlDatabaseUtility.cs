//namespace Sanita.Utility.Database.Utility
//{
//    using Sanita.Utility.ExtendedThread;
//    using Sanita.Utility.Logger;
//    using System;
//    using System.Collections.Generic;
//    using System.Data;
//    using System.IO;
//    using System.Linq;
//    using System.Text;

//    public class NpgsqlDatabaseUtility : DatabaseImplementUtility
//    {
//        private const string TAG = "NpgsqlDatabaseUtility";

//        public override int AlterTableTable(string DatabaseName, ClassTable Fixtable, ClassTable NewTable)
//        {
//            StringBuilder builder = new StringBuilder();
//            bool flag = false;
//            bool flag2 = false;
//            string str = "";
//            try
//            {
//                builder.Append(" ALTER TABLE " + Fixtable.Table + "     ");
//                Func<ClassColumn, bool> predicate = null;
//                for (int i = 0; i < Fixtable.listColumn.Count; i++)
//                {
//                    if (Fixtable.listColumn[i].isPRIMARY)
//                    {
//                        if (string.IsNullOrEmpty(str))
//                        {
//                            str = "'" + Fixtable.listColumn[i].ColumnName + "'";
//                        }
//                        else
//                        {
//                            str = str + " , '" + Fixtable.listColumn[i].ColumnName + "'";
//                        }
//                    }
//                    bool flag3 = false;
//                    if (predicate == null)
//                    {
//                        predicate = p => p.ColumnName.ToLower() == Fixtable.listColumn[i].ColumnName.ToLower();
//                    }
//                    if (NewTable.listColumn.FirstOrDefault<ClassColumn>(predicate) == null)
//                    {
//                        builder.Append(" ADD COLUMN " + Fixtable.listColumn[i].ColumnName + " " + Fixtable.listColumn[i].ColumnDefine + "  ");
//                        flag = true;
//                        flag3 = true;
//                        SanitaLogEx.e("NpgsqlDatabaseUtility", "      >>Add column '" + Fixtable.listColumn[i].ColumnName + "'");
//                    }
//                    if (i < (Fixtable.listColumn.Count - 1))
//                    {
//                        if (flag3)
//                        {
//                            builder.Append(" , ");
//                        }
//                    }
//                    else
//                    {
//                        builder.Append(" ; ");
//                    }
//                }
//                if (!flag)
//                {
//                    builder = new StringBuilder();
//                }
//                for (int j = 0; j < Fixtable.listColumn.Count; j++)
//                {
//                    if (Fixtable.listColumn[j].isIndex)
//                    {
//                        string index_name = $"{Fixtable.Table.ToLower()}_{Fixtable.listColumn[j].ColumnName.ToLower().Replace(Fixtable.Table.ToLower().Replace("tb_", ""), "")}_idx";
//                        if (NewTable.listIndex.FirstOrDefault<ClassColumn>(p => p.ColumnName.Equals(index_name, StringComparison.CurrentCultureIgnoreCase)) == null)
//                        {
//                            builder.Append(string.Format("CREATE INDEX {2} ON {0} USING btree ({1});", Fixtable.Table, Fixtable.listColumn[j].ColumnName, index_name));
//                            flag2 = true;
//                            SanitaLogEx.e("NpgsqlDatabaseUtility", "      >>Add index '" + Fixtable.listColumn[j].ColumnName + "'");
//                        }
//                    }
//                }
//                builder.Replace(",  ; ", "; ");
//            }
//            catch (Exception exception)
//            {
//                SanitaLogEx.e("NpgsqlDatabaseUtility", exception);
//            }
//            try
//            {
//                if (flag || flag2)
//                {
//                    return base.myBaseDao.DoUpdate(builder.ToString());
//                }
//                return 0;
//            }
//            catch
//            {
//                return -100;
//            }
//        }

//        public override int CreateDatabase(string DatabaseName)
//        {
//            if (string.IsNullOrEmpty(DatabaseName))
//            {
//                return -100;
//            }
//            StringBuilder builder = new StringBuilder();
//            builder.Append(" CREATE DATABASE " + DatabaseName);
//            try
//            {
//                return base.myBaseDao.DoCreateDatabase(builder.ToString());
//            }
//            catch
//            {
//                return -100;
//            }
//        }

//        public override int CreateTable(string DatabaseName, ClassTable table)
//        {
//            StringBuilder builder = new StringBuilder();
//            try
//            {
//                int num;
//                builder.Append(" CREATE TABLE " + table.Table + " (  ");
//                for (num = 0; num < table.listColumn.Count; num++)
//                {
//                    builder.Append(" " + table.listColumn[num].ColumnName + " " + table.listColumn[num].ColumnDefine);
//                    if (table.listColumn[num].isPRIMARY)
//                    {
//                        builder.Append(" PRIMARY KEY ");
//                    }
//                    if (num < (table.listColumn.Count - 1))
//                    {
//                        builder.Append(" , ");
//                    }
//                }
//                builder.Append(" ) WITH OIDS ;  ");
//                for (num = 0; num < table.listColumn.Count; num++)
//                {
//                    if (table.listColumn[num].isIndex)
//                    {
//                        builder.Append(string.Format("CREATE INDEX {0}_{1}_idx ON {0} USING btree ({2});", table.Table.ToLower(), table.listColumn[num].ColumnName.ToLower().Replace(table.Table.ToLower().Replace("tb_", ""), ""), table.listColumn[num].ColumnName));
//                    }
//                }
//            }
//            catch (Exception exception)
//            {
//                SanitaLogEx.e("NpgsqlDatabaseUtility", exception);
//            }
//            try
//            {
//                return base.myBaseDao.DoUpdate(builder.ToString());
//            }
//            catch
//            {
//                return -100;
//            }
//        }

//        public override void DoNotification(string chanel, string data)
//        {
//            base.myBaseDao.DoNotification(chanel, data);
//        }

//        public override IList<ClassColumn> GetListColumn(string DatabaseName, string TableName)
//        {
//            StringBuilder builder = new StringBuilder();
//            builder.Append(" select * from information_schema.columns where table_schema = 'public' and table_name ='" + TableName + "'  ");
//            DataTable table = base.myBaseDao.DoGetDataTable(builder.ToString());
//            IList<ClassColumn> list = new List<ClassColumn>();
//            if (table != null)
//            {
//                foreach (DataRow row in table.Rows)
//                {
//                    ClassColumn item = new ClassColumn();
//                    if (row != null)
//                    {
//                        if (row["column_name"] != DBNull.Value)
//                        {
//                            item.ColumnName = row["column_name"].ToString();
//                        }
//                        if (row["data_type"] != DBNull.Value)
//                        {
//                            item.ColumnDefine = item.ColumnDefine + " " + row["data_type"].ToString();
//                        }
//                        if ((row["is_nullable"] != DBNull.Value) && (row["is_nullable"].ToString() == "NO"))
//                        {
//                            item.ColumnDefine = item.ColumnDefine + " NOT NULL";
//                            if ((row["ordinal_position"] != DBNull.Value) && (((int) row["ordinal_position"]) == 1))
//                            {
//                                item.isPRIMARY = true;
//                            }
//                        }
//                    }
//                    list.Add(item);
//                }
//            }
//            return list;
//        }

//        public override IList<ClassDatabase> GetListDatabase()
//        {
//            StringBuilder builder = new StringBuilder();
//            builder.Append(" SELECT datname FROM pg_database; ");
//            DataTable table = base.myBaseDao.DoShowDatabase(builder.ToString());
//            IList<ClassDatabase> list = new List<ClassDatabase>();
//            if (table != null)
//            {
//                foreach (DataRow row in table.Rows)
//                {
//                    ClassDatabase item = new ClassDatabase();
//                    if ((row != null) && (row["datname"] != DBNull.Value))
//                    {
//                        item.Database = row["datname"].ToString();
//                    }
//                    list.Add(item);
//                }
//            }
//            return list;
//        }

//        public override IList<ClassColumn> GetListIndex(string DatabaseName, string TableName)
//        {
//            StringBuilder builder = new StringBuilder();
//            builder.Append($"SELECT  relname FROM pg_class WHERE oid IN (SELECT indexrelid FROM pg_index, pg_class WHERE pg_class.relname='{TableName}' AND pg_class.oid=pg_index.indrelid AND indisunique != 't' AND indisprimary != 't')");
//            DataTable table = base.myBaseDao.DoGetDataTable(builder.ToString());
//            IList<ClassColumn> list = new List<ClassColumn>();
//            if (table != null)
//            {
//                foreach (DataRow row in table.Rows)
//                {
//                    ClassColumn item = new ClassColumn();
//                    if ((row != null) && (row["relname"] != DBNull.Value))
//                    {
//                        item.ColumnName = row["relname"].ToString();
//                    }
//                    list.Add(item);
//                }
//            }
//            return list;
//        }

//        public override IList<ClassTable> GetListTable(string DatabaseName)
//        {
//            StringBuilder builder = new StringBuilder();
//            builder.Append(" select * from information_schema.tables where table_schema = 'public'  ");
//            DataTable table = base.myBaseDao.DoGetDataTable(builder.ToString());
//            IList<ClassTable> list = new List<ClassTable>();
//            if (table != null)
//            {
//                foreach (DataRow row in table.Rows)
//                {
//                    ClassTable item = new ClassTable();
//                    if ((row != null) && (row["table_name"] != DBNull.Value))
//                    {
//                        item.Table = row["table_name"].ToString();
//                        item.listColumn = this.GetListColumn(DatabaseName, item.Table);
//                        item.listIndex = this.GetListIndex(DatabaseName, item.Table);
//                    }
//                    list.Add(item);
//                }
//            }
//            return list;
//        }

//        public override void GetSchema(string DatabaseName)
//        {
//            IList<ClassTable> listTable = this.GetListTable(DatabaseName);
//            StreamWriter writer = new StreamWriter("C://test.txt", false, Encoding.UTF8);
//            for (int i = 0; i < listTable.Count; i++)
//            {
//                writer.WriteLine("#region " + listTable[i].Table);
//                writer.WriteLine("ClassTable " + listTable[i].Table + " = new ClassTable();");
//                writer.WriteLine(listTable[i].Table + ".Table = \"" + listTable[i].Table + "\";");
//                writer.WriteLine("{");
//                writer.WriteLine("    IList<ClassColumn> listColumn = new List<ClassColumn>();");
//                for (int j = 0; j < listTable[i].listColumn.Count; j++)
//                {
//                    writer.WriteLine("    {");
//                    writer.WriteLine("        ClassColumn Column = new ClassColumn();");
//                    writer.WriteLine("        Column.ColumnName = \"" + listTable[i].listColumn[j].ColumnName + "\";");
//                    writer.WriteLine("        Column.ColumnDefine = \"" + listTable[i].listColumn[j].ColumnDefine + "\";");
//                    if (listTable[i].listColumn[j].isPRIMARY)
//                    {
//                        writer.WriteLine("        Column.isPRIMARY = true;");
//                    }
//                    else
//                    {
//                        writer.WriteLine("        Column.isPRIMARY = false;");
//                    }
//                    writer.WriteLine("        listColumn.Add(Column);");
//                    writer.WriteLine("    }");
//                }
//                writer.WriteLine("    " + listTable[i].Table + ".listColumn = listColumn;");
//                writer.WriteLine("}");
//                writer.WriteLine("listFixTable.Add(" + listTable[i].Table + ");");
//                writer.WriteLine("#endregion");
//            }
//            writer.Close();
//        }

//        public override void InitNotification(IList<string> mListChannel, OnDatabaseNotificationHandler mCallback)
//        {
//            base.myBaseDao.InitNotification(mListChannel, mCallback);
//        }

//        public override bool isDatabaseOK()
//        {
//            string DatabaseName = base.myBaseDao.GetDatabaseName();
//            if (!this.GetListDatabase().Any<ClassDatabase>(p => p.Database.Equals(DatabaseName, StringComparison.CurrentCultureIgnoreCase)))
//            {
//                return false;
//            }
//            return true;
//        }

//        public override void SynchDatabase(ExBackgroundWorker worker)
//        {
//            base.synch_worker = worker;
//            string DatabaseName = base.myBaseDao.GetDatabaseName();
//            SanitaLogEx.d("NpgsqlDatabaseUtility", "[SynchDatabase] Database = [" + DatabaseName + "]");
//            if (!this.GetListDatabase().Any<ClassDatabase>(p => p.Database.Equals(DatabaseName, StringComparison.CurrentCultureIgnoreCase)))
//            {
//                SanitaLogEx.e("NpgsqlDatabaseUtility", "[SynchDatabase] Database chưa tồn tại");
//                if (this.CreateDatabase(DatabaseName) <= -100)
//                {
//                    SanitaLogEx.e("NpgsqlDatabaseUtility", "[SynchDatabase] Tạo database mới bị lỗi");
//                    if (base.synch_worker != null)
//                    {
//                        base.synch_worker.ReportProgress(-1, 0);
//                    }
//                    return;
//                }
//                SanitaLogEx.e("NpgsqlDatabaseUtility", "[SynchDatabase] Tạo database mới OK");
//            }
//            IList<ClassTable> listTable = this.GetListTable(DatabaseName);
//            if (base.synch_worker != null)
//            {
//                base.synch_worker.ReportProgress(0, base.listFixTable.Count);
//            }
//            Func<ClassTable, bool> predicate = null;
//            for (int i = 0; i < base.listFixTable.Count; i++)
//            {
//                base.synch_worker.ReportProgress(i + 1, "N\x00e2ng cấp table '" + base.listFixTable[i].Table + "...");
//                for (int j = 0; j < base.listFixTable[i].listColumn.Count; j++)
//                {
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned NOT NULL auto_increment", "serial");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned", "INTEGER");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10) unsigned NOT NULL", "INTEGER");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("int(10)", "INTEGER");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("int(11)", "INTEGER");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("text CHARACTER SET utf8 COLLATE utf8_unicode_ci", "text");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("longblob", "bytea");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("double", "double precision");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("Datetime DEFAULT '1-1-1'", "TIMESTAMP");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("datetime DEFAULT '1-1-1'", "TIMESTAMP");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("Datetime", "TIMESTAMP");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("datetime", "TIMESTAMP");
//                    base.listFixTable[i].listColumn[j].ColumnDefine = base.listFixTable[i].listColumn[j].ColumnDefine.Replace("timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP", "TIMESTAMP");
//                }
//                if (predicate == null)
//                {
//                    predicate = p => p.Table.Equals(this.listFixTable[i].Table, StringComparison.CurrentCultureIgnoreCase);
//                }
//                ClassTable newTable = listTable.FirstOrDefault<ClassTable>(predicate);
//                if (newTable == null)
//                {
//                    SanitaLogEx.d("NpgsqlDatabaseUtility", "[SynchDatabase] Create table '" + base.listFixTable[i].Table + "'");
//                    if (this.CreateTable(DatabaseName, base.listFixTable[i]) <= -100)
//                    {
//                        SanitaLogEx.e("NpgsqlDatabaseUtility", "[SynchDatabase] Create table lỗi");
//                        if (base.synch_worker != null)
//                        {
//                            base.synch_worker.ReportProgress(-1, "Tạo table '" + base.listFixTable[i].Table + "' lỗi !");
//                        }
//                        return;
//                    }
//                }
//                else
//                {
//                    SanitaLogEx.e("NpgsqlDatabaseUtility", "[SynchDatabase] Alter table '" + base.listFixTable[i].Table + "'");
//                    if (this.AlterTableTable(DatabaseName, base.listFixTable[i], newTable) <= -100)
//                    {
//                        SanitaLogEx.e("NpgsqlDatabaseUtility", "[SynchDatabase] Alter table lỗi");
//                        if (base.synch_worker != null)
//                        {
//                            base.synch_worker.ReportProgress(-1, "Update table '" + base.listFixTable[i].Table + "' lỗi !");
//                        }
//                        return;
//                    }
//                }
//                if (base.synch_worker != null)
//                {
//                    base.synch_worker.ReportProgress(i + 1, "N\x00e2ng cấp table '" + base.listFixTable[i].Table + "' OK !");
//                }
//            }
//            string str = "tb_dm_icd10";
//            string str2 = "1";
//            base.myBaseDao.DoUpdate("CREATE OR REPLACE FUNCTION " + str + "_updated_function() RETURNS TRIGGER LANGUAGE 'plpgsql' AS $$ BEGIN update tb_dm_tablecache set dm_tablecachelastupdate = now() where dm_tablecacheid = " + str2 + "; return null; END $$;");
//            base.myBaseDao.DoUpdate("DROP TRIGGER IF EXISTS " + str + "_updated ON " + str + ";");
//            base.myBaseDao.DoUpdate("CREATE TRIGGER " + str + "_updated AFTER INSERT OR UPDATE OR DELETE ON " + str + " FOR EACH ROW EXECUTE PROCEDURE " + str + "_updated_function();");
//            str = "tb_service";
//            str2 = "2";
//            base.myBaseDao.DoUpdate("CREATE OR REPLACE FUNCTION " + str + "_updated_function() RETURNS TRIGGER LANGUAGE 'plpgsql' AS $$ BEGIN update tb_dm_tablecache set dm_tablecachelastupdate = now() where dm_tablecacheid = " + str2 + "; return null; END $$;");
//            base.myBaseDao.DoUpdate("DROP TRIGGER IF EXISTS " + str + "_updated ON " + str + ";");
//            base.myBaseDao.DoUpdate("CREATE TRIGGER " + str + "_updated AFTER INSERT OR UPDATE OR DELETE ON " + str + " FOR EACH ROW EXECUTE PROCEDURE " + str + "_updated_function();");
//        }
//    }
//}

