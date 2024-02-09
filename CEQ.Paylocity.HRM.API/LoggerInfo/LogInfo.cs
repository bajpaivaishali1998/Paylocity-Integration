using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEQ.Paylocity.HRM.API.LoggerInfo
{
    public class LogInfo : TableEntity
    {
        public string Method_Name { get; set; }
        public string Method_Start { get; set; }
        public string Method_End { get; set; }
        public string ReqInput { get; set; }
        public string ApiUrl { get; set; }
        public string ResOutput { get; set; }
        public string ErrorException { get; set; }
        public string LogMessage { get; set; }
        public string StatusCode { get; set; }
        public LogInfo()
        {
            Method_Name = "NA"; Method_Start = "NA"; ReqInput = "NA"; ApiUrl = "NA"; ResOutput = "NA"; Method_End = "NA"; ErrorException = "NA"; LogMessage = "NA"; StatusCode = "NA";
        }
    }

}
