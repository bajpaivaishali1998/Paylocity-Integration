using Azure.Identity;
using CEQ.Paylocity.HRM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Newtonsoft.Json;

namespace CEQ.Paylocity.HRM.API
{
    public class PaylocityEmployeeAdd
    {
        public readonly ILogger<PaylocityEmployeeAdd> _logger;

        public PaylocityEmployeeAdd(ILogger<PaylocityEmployeeAdd> logger)
        {
            _logger = logger;
        }

        [Function("PaylocityEmployeeAdd")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var clientId = Environment.GetEnvironmentVariable("clientId");
            var tenantId = Environment.GetEnvironmentVariable("tenantId");
            var clientSecret = Environment.GetEnvironmentVariable("clientSecret");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string resString = string.Empty;
            string[] scopes = { "https://graph.microsoft.com/.default" };
            ClientSecretCredential authProvider = new ClientSecretCredential(tenantId, clientId, clientSecret);
            GraphServiceClient graphClient = new GraphServiceClient(authProvider, scopes);


            Payload deserializedPayload = JsonConvert.DeserializeObject<Payload>(requestBody);

            Employee objUserInfo = new Employee();
            objUserInfo.FirstName = deserializedPayload.employeeFirstName;
            objUserInfo.LastName = deserializedPayload.employeeLastName;
            objUserInfo.CompanyName = deserializedPayload.companyName;
            objUserInfo.CompanyId = deserializedPayload.companyId;
            objUserInfo.AddressLine1 = deserializedPayload.employeeAddressLine1;
            objUserInfo.AddressLine2 = deserializedPayload.employeeAddressLine2;
            objUserInfo.Gender = deserializedPayload.employeeGender;
            objUserInfo.JobTitle = deserializedPayload.employeeJobTitle;
            objUserInfo.Position = deserializedPayload.employeePosition;
            objUserInfo.WorkEMailAddress= deserializedPayload.employeeWorkEMailAddress;
            objUserInfo.PayFrequency = deserializedPayload.employeePayFrequency;
            objUserInfo.WorkPhone = deserializedPayload.employeeWorkPhone;
            objUserInfo.PayType = deserializedPayload.employeePayType;
            objUserInfo.City = deserializedPayload.employeeCity;
            objUserInfo.CostCenter1 = deserializedPayload.employeeCostCenter1;
            objUserInfo.CostCenter2 = deserializedPayload.employeeCostCenter2;
            objUserInfo.CostCenter3 = deserializedPayload.employeeCostCenter3;
            objUserInfo.HireDate = deserializedPayload.employeeHireDate;
            objUserInfo.BadgeClockNumber = deserializedPayload.employeeBadgeClockNumber;
            objUserInfo.EEOClass = deserializedPayload.employeeEEOClass;
            objUserInfo.MaritalStatus = deserializedPayload.employeeMaritalStatus;
            objUserInfo.MiddleInitial= deserializedPayload.employeeMiddleInitial;
            objUserInfo.State= deserializedPayload.employeeState;
            objUserInfo.Supervisor= deserializedPayload.employeeSupervisor;
            objUserInfo.SupervisorId= deserializedPayload.employeeSupervisorId;
            objUserInfo.TaxForm= deserializedPayload.employeeTaxForm;
            objUserInfo.Zip= deserializedPayload.employeeZip;

            //------------------------------------GET USER FROM AZURE AD------------------------------------------------------------------//

            try
            {
                    var allUsers = await graphClient.Users.GetAsync((requestConfiguration) =>
                    {
                        requestConfiguration.QueryParameters.Top = int.MaxValue;
                        requestConfiguration.QueryParameters.Select = new string[] { "displayName", "userPrincipalName", "otherMails", "mail", "id" };                        
                    });

                    List<AadGroupMember> getAllUserList = new List<AadGroupMember>();
                    foreach (var user in allUsers.Value)
                    {
                        if (user.UserPrincipalName.ToLower() == objUserInfo.WorkEMailAddress.ToLower())
                        {
                            getAllUserList.Add(new AadGroupMember
                            {
                                ObjectId = user.Id,
                                UserPrincipalName = user.UserPrincipalName,
                                Name = user.DisplayName,
                                Email = user.Mail,
                            });
                        }
                        else
                        {
                            foreach (var otherMail in user.OtherMails)
                            {
                                if (otherMail.ToLower().ToString() == objUserInfo.WorkEMailAddress.ToLower().ToString())
                                {
                                    getAllUserList.Add(new AadGroupMember
                                    {
                                        OtherEmail = otherMail
                                    });
                                }
                            }
                        }
                    }

                //---------------------------------------------------ADD NEW USER IN AZURE AD----------------------------------------------------------//

                if (getAllUserList.Count == 0)
                {
                    //Getting users from Azure AD based on Display Name and creating new company email                      

                    List<AadGroupMember> userList = new List<AadGroupMember>();

                    foreach (var user in allUsers.Value)
                    {
                        if (user.DisplayName.ToLower() == objUserInfo.FirstName.ToLower())
                            userList.Add(new AadGroupMember
                            {
                                ObjectId = user.Id,
                                UserPrincipalName = user.UserPrincipalName,
                                Name = user.DisplayName,
                                Email = user.Mail,
                            });
                    }

                    int usercount = userList.Count;
                    string createusermail = "";




                }



            }
            catch (Exception ex)
            {

                throw ex;
            }



            return new OkObjectResult("Welcome to Azure Functions!");
        }

    }
}
