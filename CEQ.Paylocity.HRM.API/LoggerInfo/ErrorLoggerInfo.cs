using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEQ.Paylocity.HRM.API.LoggerInfo
{
    internal class ErrorLoggerInfo
    {
        public static string InsertLogEntity(string logTableName, LogInfo logInfo, string cnn, bool IsEnableLog = true)
        {
            try
            {
                if (logInfo != null)
                {
                    if (IsEnableLog)
                    {
                        LogInfo logObj = new LogInfo();
                        logObj.PartitionKey = logInfo.PartitionKey;
                        logObj.RowKey = Guid.NewGuid().ToString();
                        logObj.Method_Name = logInfo.Method_Name;
                        logObj.Method_Start = logInfo.Method_Start;
                        logObj.Method_End = logInfo.Method_End;
                        logObj.ReqInput = logInfo.ReqInput;
                        logObj.ApiUrl = logInfo.ApiUrl;
                        logObj.ResOutput = logInfo.ResOutput;
                        logObj.ErrorException = logInfo.ErrorException;
                        logObj.LogMessage = logInfo.LogMessage;
                        logObj.StatusCode = logInfo.StatusCode;

                        CloudStorageAccount storageAcc = CloudStorageAccount.Parse(cnn);
                        CloudTableClient tblclient = storageAcc.CreateCloudTableClient();
                        CloudTable table = tblclient.GetTableReference(logTableName);
                        var logOperationapi = TableOperation.Insert(logObj);
                        table.ExecuteAsync(logOperationapi);
                    }
                    return "Record Saved";
                }
                else
                {
                    return "Record not Saved";
                }
            }
            catch (Exception ex)
            {
                return "error " + Convert.ToString(ex);
            }
        }
    }
}
