namespace Contract.Database
{
    using MediboxDemo.Core.Database;
    using Sanita.Utility.Database.BaseDao;
    using System;

    public class BaseDB
    {
        private const string TAG = "BaseDB";
        protected object lockObject = new object();

        protected IBaseDao baseDAO =>
            ContractManagerDatabaseUtility.GetDatabaseDAO();
    }
}

