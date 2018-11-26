using Contract.DAO;
using Contract.Model;
using Contract.utility;
using Npgsql;
using Sanita.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Contract.Database
{
    public class ContractDB : BaseDAO
    {
        private const string nameTable = "Contract";
        private static ContractDB _instance;

        public IList<ContractInfo> GetContracts()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * ");
            builder.Append(" FROM Contract ");
            DataTable dt = base.baseDAO.DoGetDataTable(builder.ToString());
            return this.MakeContracts(dt);
        }

        public ContractInfo GetContract()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * ");
            builder.Append(" FROM Contract ");
            DataRow row = base.baseDAO.DoGetDataRow(builder.ToString());
            return this.MakeContract(row);
        }


        public ContractInfo GetContract(string ContractName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * ");
            builder.Append(" FROM Contract ");
            builder.Append(" WHERE Name         = " + DatabaseUtility.Escape(ContractName));
            builder.Append(" AND   Active       = " + DatabaseUtility.Escape(DataTypeModel.ACTIVE));
            DataRow row = base.baseDAO.DoGetDataRow(builder.ToString());
            return this.MakeContract(row);
        }

        public int UpdateContract(NpgsqlConnection connection, IDbTransaction trans, ContractInfo mContractInfo)
        {
            try
            {
                lock (lockObject)
                {
                    if (mContractInfo == null)
                    {
                        return DataTypeModel.RESULT_NG;
                    }

                    StringBuilder builder = new StringBuilder();
                    builder.Append(" UPDATE Contract ");
                    builder.Append("  SET  ");
                    builder.Append("      Id        = " + DatabaseUtility.Escape(mContractInfo.Id) + ", ");
                    builder.Append("      Name      = " + DatabaseUtility.Escape(mContractInfo.Name) + ", ");
                    builder.Append("      Address   = " + DatabaseUtility.Escape(mContractInfo.Address) + ", ");
                    builder.Append("      Abi       = " + DatabaseUtility.Escape(mContractInfo.Abi) + ", ");
                    builder.Append("      ByteCode  = " + DatabaseUtility.Escape(mContractInfo.ByteCode) + ", ");
                    builder.Append("      Active    = " + DatabaseUtility.Escape(mContractInfo.Active) + " ");
                    builder.Append("  WHERE Id = " + DatabaseUtility.Escape(mContractInfo.Id));

                    return base.baseDAO.DoUpdate(connection, trans, builder.ToString());
                }
            }
            catch (Exception ex)
            {
                SanitaLog.Exception(ex);
                throw ex;
            }
        }


        public int InsertContract(NpgsqlConnection connection, IDbTransaction trans, ContractInfo mContractInfo)
        {
            try
            {
                lock (lockObject)
                {
                    if (mContractInfo == null)
                    {
                        return DataTypeModel.RESULT_NG;
                    }

                    StringBuilder builder = new StringBuilder();
                    builder.Append(" INSERT INTO Contract (");
                    builder.Append("            Id,");
                    builder.Append("            Name,");
                    builder.Append("            Address,");
                    builder.Append("            Abi,");
                    builder.Append("            ByteCode,");
                    builder.Append("            Active)");
                    builder.Append("  VALUES( ");
                    builder.Append("          " + DatabaseUtility.Escape(mContractInfo.Id) + ", ");
                    builder.Append("          " + DatabaseUtility.Escape(mContractInfo.Name) + ", ");
                    builder.Append("          " + DatabaseUtility.Escape(mContractInfo.Address) + ", ");
                    builder.Append("          " + DatabaseUtility.Escape(mContractInfo.Abi) + ", ");
                    builder.Append("          " + DatabaseUtility.Escape(mContractInfo.ByteCode) + ", ");
                    builder.Append("          " + DatabaseUtility.Escape(mContractInfo.Active) + ") ");

                    int num = base.baseDAO.DoInsert(connection, trans, builder.ToString());
                    if (num > 0)
                    {
                        return DataTypeModel.RESULT_OK;
                    }

                    return DataTypeModel.RESULT_NG;
                }
            }
            catch (Exception ex)
            {
                SanitaLog.Exception(ex);
                throw ex;
            }
        }

        private ContractInfo MakeContract(DataRow row)
        {
            ContractInfo contract = new ContractInfo();
            if (row != null)
            {
                contract.SetProperty(row);
            }
            return contract;
        }

        private IList<ContractInfo> MakeContracts(DataTable dt)
        {
            IList<ContractInfo> list = new List<ContractInfo>();
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(this.MakeContract(row));
                }
            }
            return list;
        }

        public static ContractDB mInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContractDB();
                }
                return _instance;
            }
        }

    }
}
