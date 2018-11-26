namespace MediboxDemo.Core.Database
{
    using Sanita.Utility.Database.BaseDao;
    using Sanita.Utility.Database.Utility;
    using System;
    using System.Collections.Generic;

    public class ContractManagerDatabaseUtility
    {
        private const string TAG = "MediboxDatabaseUtility";
        private static DatabaseUtility mDatabaseUtility_Main = new DatabaseUtility();

        public static IBaseDao GetDatabaseDAO()
        {
            IBaseDao databaseDAO = mDatabaseUtility_Main.GetDatabaseDAO();
            databaseDAO.SetConnectionConfig("localhost", "postgres", "Thu123456!", "smartcontract", "5432");
            return databaseDAO;
        }

        public static DatabaseUtility GetDatabaseUtility() => 
            mDatabaseUtility_Main;

        public static string GetDatabaseVersion() => 
            "10";

        //public static void InitDatabase()
        //{
        //    mDatabaseUtility_Main.GetDatabaseImplementUtility().InitDatabase(GetDatabaseDAO(), InitTable());
        //}

        //public static List<ClassTable> InitTable()
        //{
        //    List<ClassTable> list = new List<ClassTable>();
        //    ClassTable item = new ClassTable {
        //        Table = "tb_softupdate"
        //    };
        //    IList<ClassColumn> list2 = new List<ClassColumn>();
        //    ClassColumn column = new ClassColumn {
        //        ColumnName = "SoftUpdateID",
        //        ColumnDefine = " int(10) unsigned NOT NULL auto_increment ",
        //        isPRIMARY = true
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftUpdateVersion",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftUpdateSQL",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftUpdateData",
        //        ColumnDefine = " longblob ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftUpdateSize",
        //        ColumnDefine = " int(10) DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftUpdateUser",
        //        ColumnDefine = " int(10) DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftUpdateTime",
        //        ColumnDefine = " datetime ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftUpdateKey",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "Version",
        //        ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    item.listColumn = list2;
        //    list.Add(item);
        //    ClassTable table2 = new ClassTable {
        //        Table = "tb_account"
        //    };
        //    list2 = new List<ClassColumn>();
        //    column = new ClassColumn {
        //        ColumnName = "AccountID",
        //        ColumnDefine = " int(10) unsigned NOT NULL auto_increment ",
        //        isPRIMARY = true
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountCode",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountName",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountPassword",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "Name",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "Ngaysinh",
        //        ColumnDefine = " datetime ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountAddresss",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountPhone",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountEmail",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TokenCode",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "Version",
        //        ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    table2.listColumn = list2;
        //    list.Add(table2);
        //    ClassTable table3 = new ClassTable {
        //        Table = "tb_accountdoanhthu"
        //    };
        //    list2 = new List<ClassColumn>();
        //    column = new ClassColumn {
        //        ColumnName = "AccountDoanhThuID",
        //        ColumnDefine = " int(10) unsigned NOT NULL auto_increment ",
        //        isPRIMARY = true
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountID",
        //        ColumnDefine = " int(10) DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "CauHinhBenhVienID",
        //        ColumnDefine = " int(10) DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "NgayCapNhat",
        //        ColumnDefine = " datetime ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "NgayCapNhatLast",
        //        ColumnDefine = " datetime ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoLuotKham",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongDoanhThu",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongMienGiam",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongBenhNhan",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongDaThu",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongChietKhau",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongDoanhThu_KhamBenh",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongDoanhThu_XetNghiem",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongDoanhThu_ChanDoanHinhAnh",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongDoanhThu_ChuyenKhoa",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TongDoanhThu_Thuoc",
        //        ColumnDefine = " double DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "Version",
        //        ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    table3.listColumn = list2;
        //    list.Add(table3);
        //    ClassTable table4 = new ClassTable {
        //        Table = "tb_cauhinhbenhvien"
        //    };
        //    list2 = new List<ClassColumn>();
        //    column = new ClassColumn {
        //        ColumnName = "CauHinhBenhVienID",
        //        ColumnDefine = " int(10) unsigned NOT NULL auto_increment ",
        //        isPRIMARY = true
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "DonViQuanLy",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "GiamDocBenhVien",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "MaTinh",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "MaHuyen",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "MaXa",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "MaYTe",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "MaBenhVien",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TenBenhVien",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "DiaChiBenhVien",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "DM_TuyenBenhVienID",
        //        ColumnDefine = " int(10) DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "License",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "LogoData",
        //        ColumnDefine = " longblob ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "CauHinhBenhVienHarwareID",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "ExternalIP",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "DBHost",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "DBName",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "SoftVersion",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "ClientCode",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "AccountCode",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "TokenCode",
        //        ColumnDefine = " text CHARACTER SET utf8 COLLATE utf8_unicode_ci ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "LastUpdate",
        //        ColumnDefine = " datetime ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "IsQuanLyPK",
        //        ColumnDefine = " int(10) DEFAULT '0' ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    column = new ClassColumn {
        //        ColumnName = "Version",
        //        ColumnDefine = " timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP ",
        //        isPRIMARY = false
        //    };
        //    list2.Add(column);
        //    table4.listColumn = list2;
        //    list.Add(table4);
        //    return list;
        //}
    }
}

