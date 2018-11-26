using Contract.DAO;
using Contract.Model;
using Contract.Presenter;
using Contract.utility;
using Microsoft.AspNetCore.Mvc;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Sanita.Utility.Logger;

namespace Contract.Controllers
{
    public class ResultApi
    {
        public string status
        {
            get;
            set;
        }

        public string message
        {
            get;
            set;
        }

        public string data
        {
            get;
            set;
        }
    }

    public class TheContract
    {
        public string Abi
        {
            get;
            set;
        }

        public string Bytecode
        {
            get;
            set;
        }
    }

    public class Web3Api
    {
        //Singleton
        private static Web3 _web3;
        public static Web3 mWeb3
        {
            get
            {
                if (_web3 == null)
                {
                    SanitaLog.Log("", "INITIALIZE WEB3");
                }
                _web3 = _web3 ?? new Nethereum.Web3.Web3(Link.WEB3);
                return _web3;
            }
        }

        public static async Task<bool> UnlockAccount(string accountPublicKey, string accountPassword, int accountUnlockTime)
        {
            bool unlockResult = await mWeb3.Personal.UnlockAccount.SendRequestAsync(accountPublicKey, accountPassword, accountUnlockTime);

            return unlockResult;
        }

        public static async Task<TheContract> GetTheContract(string contractName)
        {
            string actionUrl = Utility.CombineUri(Link.HOST, "smart-contract/get-the-contract");
            string pathFile = Utility.GetPathFileContract(contractName);

            if (!System.IO.File.Exists(pathFile))
            {
                SanitaLog.Error("Not find " + pathFile);
                return null;
            }
            HttpContent fileStreamContent = new ByteArrayContent(System.IO.File.ReadAllBytes(pathFile));

            using (HttpClient client = new HttpClient())
            using (MultipartFormDataContent formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent, "file", contractName);

                var response = await client.PostAsync(actionUrl, formData);

                response.EnsureSuccessStatusCode();

                var contentString = await response.Content.ReadAsStringAsync();
                var contents = JObject.Parse(contentString);
                ResultApi result = JsonConvert.DeserializeObject<ResultApi>(contentString);

                client.Dispose();

                if (result.status == "success")
                {
                    TheContract mTheContract = JsonConvert.DeserializeObject<TheContract>(result.data);
                    SanitaLog.Log("Abi", mTheContract.Abi);
                    SanitaLog.Log("ByteCode", mTheContract.Bytecode);
                    return mTheContract;
                }
                else
                {
                    return null;
                }
            }
        }

        //Deploy the contract
        public static async Task<string> WaitDeploy(string abi, string byteCode, string accountPublicKey, HexBigInteger gas, params object[] values)
        {
            string transactionHash = await Web3Api.mWeb3.Eth.DeployContract.SendRequestAsync(abi, byteCode, accountPublicKey, gas, values);
            return transactionHash;
        }

        //Mine the transaction of deployment and Get a receipt for that transaction
        public static async Task<TransactionReceipt> WaitMiner(string transactionHash)
        {
            TransactionReceipt receipt = null;

            // If receipt is null it means the Contract creation transaction is not minded yet.
            while (receipt == null)
            {
                receipt = await mWeb3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
                SanitaLog.Log("Minner", "It will take some seconds for the transaction to be approved by the network minders.");
                Thread.Sleep(4000);
            }

            SanitaLog.LogObject("Contract", receipt);
            SanitaLog.Log("Contract address", receipt.ContractAddress);

            //string contractAddress = receipt.ContractAddress;
            //var EthGetTransactionReceipt = Web3Api.mWeb3.Eth.Transactions.GetTransactionReceipt;
            //var EthCall = Web3Api.mWeb3.Eth.Transactions.Call;
            //var EthEstimateGas = Web3Api.mWeb3.Eth.Transactions.EstimateGas;
            //var EthGetTransactionByBlockHashAndIndex = Web3Api.mWeb3.Eth.Transactions.GetTransactionByBlockHashAndIndex;
            //var NetPeerCount = Web3Api.mWeb3.Net.PeerCount;
            //var EthGetBalance = Web3Api.mWeb3.Eth.GetBalance;
            //var EthMining = Web3Api.mWeb3.Eth.Mining.IsMining;
            //var EthAccounts = Web3Api.mWeb3.Eth.Accounts;

            return receipt;
        }

        #region CALL Multiply

        // Retrieve the total number of transactions of your sender address

        public static Nethereum.Contracts.Contract GetContract(string abi, string contractAddress)
        {
            Nethereum.Contracts.Contract contract = mWeb3.Eth.GetContract(abi, contractAddress);
            SanitaLog.Log("abi", abi);
            SanitaLog.Log("contract address", contractAddress);
            SanitaLog.LogObject("contract", contract);
            return contract;
        }

        public static async Task<HexBigInteger> GetTotalTransactions(string senderAddress)
        {
            HexBigInteger transactionCount = await Web3Api.mWeb3.Eth.Transactions.GetTransactionCount.SendRequestAsync(senderAddress);
            return transactionCount;
        }

        public static Function getFunction(Nethereum.Contracts.Contract contract, string functionName)
        {
            Function funct = contract.GetFunction(functionName);
            return funct;
        }

        #endregion CALL Multiply
    }

    [Route("api/[controller]")]
    public class TestController : Controller
    {
        // GET api/test
        [HttpGet]
        public async Task<string> GET()
        {
            try
            {
                SanitaLog.Method("GET", "TestController");

                // this will leave the account unlucked for 2 minutes    
                string contractName = "King.sol";
                int accountUnlockTime = 120;
                string accountPublicKey = "0xd6b37e4590c65787437f57e1ad7bb6b9a6f7ba8f";
                string accountPassword = "khuyenthuvn@gmail.com";
                HexBigInteger gas = new HexBigInteger(3000000);
                //HexBigInteger balance = new HexBigInteger(120);
                var multiplier = 7;


                // Unlock the caller's account with the given password
                bool unlockResult = await Web3Api.UnlockAccount(accountPublicKey, accountPassword, accountUnlockTime);

                //Get abi of contract
                TheContract mTheContract = await Web3Api.GetTheContract(contractName);

                if (mTheContract != null)
                {
                    string abi = mTheContract.Abi;
                    string byteCode = "0x" + mTheContract.Bytecode;

                    BaseDAO baseDAO = new BaseDAO();

                    //Get connection
                    using (NpgsqlConnection connection = baseDAO.GetConnection())
                    {
                        try
                        {
                            //Open connection
                            connection.Open();

                            HexBigInteger estimateGas = await Web3Api.mWeb3.Eth.DeployContract.EstimateGasAsync(abi, byteCode, accountPublicKey);
                            SanitaLog.Log("Estimate gas", estimateGas);

                            //TODO: Add code to save address of transactionHash => not deploy multiple times
                            //Find contract is deployed

                            ContractInfo mContractInfo = ContractPresenter.GetContract(contractName);

                            //If contract is not exist or not update
                            if (mContractInfo == null || !String.Equals(mContractInfo.ByteCode, byteCode))
                            {
                                string transactionHash = await Web3Api.WaitDeploy(abi, byteCode, accountPublicKey, gas, multiplier);

                                //Minner
                                TransactionReceipt receipt = await Web3Api.WaitMiner(transactionHash);

                                //Insert or update contract
                                mContractInfo = new ContractInfo();
                                mContractInfo.Name = contractName;
                                mContractInfo.Address = receipt.ContractAddress;
                                mContractInfo.Abi = abi;
                                mContractInfo.ByteCode = byteCode;
                                mContractInfo.Active = DataTypeModel.ACTIVE;

                                int _result = ContractPresenter.SaveContract(connection, null, mContractInfo);

                                if (_result == DataTypeModel.RESULT_NG)
                                {
                                    SanitaLog.Error("No save contract to databse");
                                    return "Error occur. Please try again.";
                                }
                            }

                            //Close connection
                            connection.Close();

                            //Get contract
                            Nethereum.Contracts.Contract contract = Web3Api.GetContract(abi, mContractInfo.Address);

                            #region Error
                            //Get event
                            //Event multiplyEvent = contract.GetEvent("Multiplied");

                            //var filterAll = await multiplyEvent.CreateFilterAsync();
                            //var filter7 = await multiplyEvent.CreateFilterAsync(7);

                            //var gasFunc = await multiplyFunction.EstimateGasAsync(accountPublicKey, null, null, newAddress, amountToSend);

                            //Call event
                            //transactionHash = await multiplyFunction.SendTransactionAsync(accountPublicKey, 7);
                            //SanitaLog.Log("transaction hash", transactionHash);

                            //Call event
                            //transactionHash = await multiplyFunction.SendTransactionAsync(accountPublicKey, 8);
                            //SanitaLog.Log("transaction hash", transactionHash);

                            //Minner
                            //receipt = await Web3Api.WaitMiner(transactionHash);
                            #endregion Error

                            Function funct = null;

                            funct = Web3Api.getFunction(contract, "getOwner");
                            var result = await funct.CallAsync<string>();
                            SanitaLog.Log("Result of function getOwner", result);

                            funct = Web3Api.getFunction(contract, "showListShareHolders");
                            var mListShareHolders = await funct.CallAsync<List<string>>();
                            SanitaLog.Log("Result of function showListShareHolders", Utility.ToStringList(mListShareHolders));

                            funct = Web3Api.getFunction(contract, "addListShareHolders");
                            await funct.CallAsync<object>("0x5e98ff12d889945488ddd53ffb71f6580cae1571");

                            funct = Web3Api.getFunction(contract, "addListShareHolders");
                            await funct.CallAsync<object>("0x9bb7f17d1d53774e0fdfa4b76ba68c48e8a4daba");

                            funct = Web3Api.getFunction(contract, "addListShareHolders");
                            await funct.CallAsync<object>("0xee2d522d8f10769a5f150ed55f40f305c8d0595f");

                            funct = Web3Api.getFunction(contract, "showListShareHolders");
                            mListShareHolders = await funct.CallAsync<List<string>>();
                            SanitaLog.Log("Result of function showListShareHolders", Utility.ToStringList(mListShareHolders));

                            SanitaLog.Success("Success deploy contract");
                            return "success";
                        }
                        catch (Exception exception1)
                        {
                            //Close connection
                            connection.Close();

                            SanitaLog.Exception(exception1);
                            return "Error occur. Please try again.";
                        }
                    }
                }
                else
                {
                    SanitaLog.Error("Fail deploy contract");
                    return "fail";
                }
            }
            catch (Exception exception2)
            {

                SanitaLog.Exception(exception2);
                return "Error occur. Please try again.";
            }
        }
    }
}
