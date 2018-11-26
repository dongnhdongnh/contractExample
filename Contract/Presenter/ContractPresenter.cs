using Contract.Database;
using Contract.Model;
using Contract.utility;
using Npgsql;
using System.Data;

namespace Contract.Presenter
{
    public class ContractPresenter
    {
        public static ContractInfo GetContract(string ContractName)
        {
            return ContractDB.mInstance.GetContract(ContractName);
        }

        public static int SaveContract(NpgsqlConnection connection, IDbTransaction trans, ContractInfo mContractInfo)
        {
            if (mContractInfo.Id == null)
            {
                mContractInfo.Id = Utility.GetGuid();
                return InsertContract(connection, trans, mContractInfo);
            }
            else
            {
                return UpdateContract(connection, trans, mContractInfo);
            }
        }

        public static int InsertContract(NpgsqlConnection connection, IDbTransaction trans, ContractInfo mContractInfo)
        {
            return ContractDB.mInstance.InsertContract(connection, trans, mContractInfo);
        }

        public static int UpdateContract(NpgsqlConnection connection, IDbTransaction trans, ContractInfo mContractInfo)
        {
            return ContractDB.mInstance.UpdateContract(connection, trans, mContractInfo);
        }
    }
}
