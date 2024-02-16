using Azure.Identity;
using CEQ.Paylocity.HRM.API.LoggerInfo;
using CEQ.Paylocity.HRM.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
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


            CEQ.Paylocity.HRM.API.Models.Payload deserializedPayload = JsonConvert.DeserializeObject<CEQ.Paylocity.HRM.API.Models.Payload>(requestBody);

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
            objUserInfo.WorkEMailAddress = deserializedPayload.employeeWorkEMailAddress;
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
            objUserInfo.MiddleInitial = deserializedPayload.employeeMiddleInitial;
            objUserInfo.State = deserializedPayload.employeeState;
            objUserInfo.Supervisor = deserializedPayload.employeeSupervisor;
            objUserInfo.SupervisorId = deserializedPayload.employeeSupervisorId;
            objUserInfo.TaxForm = deserializedPayload.employeeTaxForm;
            objUserInfo.Zip = deserializedPayload.employeeZip;

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
                    //--------------------Getting users from Azure AD based on Display Name and creating new company email------------------------------------//
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
                    var FirstName = objUserInfo.FirstName.Split(" ").FirstOrDefault();
                    if (usercount == 0)
                    {
                        if (!string.IsNullOrEmpty((FirstName)) && !string.IsNullOrWhiteSpace((objUserInfo.LastName)))
                        {
                            var lastName = objUserInfo.LastName.Split(" ").LastOrDefault();
                            createusermail = (FirstName).ToLower().Trim() + "." + (lastName).ToLower().Trim() + "@cloudeq.com";
                        }
                        else
                        {
                            createusermail = (FirstName).ToLower().Trim() + "@cloudeq.com";
                        }
                    }
                    else if (usercount > 0)
                    {
                        if (!string.IsNullOrEmpty((FirstName)) && !string.IsNullOrWhiteSpace((objUserInfo.LastName)))
                        {
                            var lastName = objUserInfo.LastName.Split(" ").LastOrDefault();
                            createusermail = (FirstName).ToLower().Trim() + "." + (lastName).ToLower().Trim() + (usercount) + "@cloudeq.com";
                        }
                        else
                        {
                            createusermail = (FirstName).ToLower().Trim() + (usercount) + "@cloudeq.com";
                        }
                    }
                    List<string> otherEmails = new List<string>();
                    otherEmails.Add(objUserInfo.WorkEMailAddress);

                    User userData = new User()
                    {
                        AccountEnabled = true,
                        //GivenName = deserializedPayload.Payload.FirstName,
                        //Surname = surName,
                        //DisplayName = myDeserializedClass.Payload.Name.Trim(),
                        //UserPrincipalName = createusermail,  //createusermail is comes from Employee Update event(creatig new user)
                        //JobTitle = myDeserializedClass.Payload.Designation,
                        //Department = myDeserializedClass.Payload.Department,
                        //EmployeeId = myDeserializedClass.Payload.EmployeeCode,
                        //EmployeeHireDate = myDeserializedClass.Payload.DateOfJoining,
                        //MobilePhone = myDeserializedClass.Payload.Mobile,
                        //Mail = myDeserializedClass.Payload.Email,
                        //MailNickname = objUserInfo.MailNickname,
                        //UsageLocation = objUserInfo.UsageLocation, //object to string reference
                        //CompanyName = objUserInfo.CompanyName,
                        //StreetAddress = objUserInfo.PresentAddress, //streat address comes from PresentAddress
                        //OtherMails = otherEmails,
                        PasswordProfile = new PasswordProfile()
                        {
                            Password = "Password1!",
                            ForceChangePasswordNextSignIn = true
                        }
                    };

                    try
                    {
                        var createdUser = await graphClient.Users.PostAsync(userData);

                        //log.LogInformation("User Added Successfully!: " + createdUser);

                        AddLicense(createdUser.Id, graphClient); //Assigning license ad the creation time

                        // Add User in Groups
                        await CEQIPolicyMFAGrant(createdUser.Id, graphClient);


                        return new OkObjectResult(createdUser);
                    }
                    catch (Exception ex)
                    {
                    }



                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return new OkObjectResult("Welcome to Azure Functions!");
        }

        //----------------------------------------------AddLicense--------------------------------------------------------
        private static void AddLicense(string userId, GraphServiceClient graphClient)
        {
            var requestBody = new Microsoft.Graph.Users.Item.AssignLicense.AssignLicensePostRequestBody
            {
                AddLicenses = new List<AssignedLicense>
                {
                        new AssignedLicense
                        {
                            SkuId = Guid.Parse("6fd2c87f-b296-42f0-b197-1e91e994b900"),
                        },
                },
                RemoveLicenses = new List<Guid?>
                {
                    // Guid.Parse("47730b0c-916d-44a1-a6ff-c3c9b8044ec5"),
                },
            };
            var result = graphClient.Users[userId].AssignLicense.PostAsync(requestBody);
        }

        //------------------------------------Adding user in CEQIPolicyMFAGrant group--------------------------------------------------
        public static async Task CEQIPolicyMFAGrant(string userId, GraphServiceClient graphClient)
        {
            Console.WriteLine("User Adding start in CEQIPolicyMFAGrant Group....");
            string cnn = Environment.GetEnvironmentVariable("AzureWebJobsStorage", EnvironmentVariableTarget.Process);

            var CEQIPolicyMFAGrant = Environment.GetEnvironmentVariable("CEQIPolicyMFAGrant", EnvironmentVariableTarget.Process);

            var members = await graphClient.Groups[CEQIPolicyMFAGrant].Members.GetAsync((requestConfiguration) =>
            {
                requestConfiguration.QueryParameters.Top = 999;
            });
            int count = 0;
            foreach (var userlst in members.Value)
            {
                if (userlst.Id == userId)
                {
                    count++;
                }
            }

            if (count == 0)
            {
                try
                {
                    var requestBody = new Microsoft.Graph.Models.ReferenceCreate
                    {
                        OdataId = "https://graph.microsoft.com/v1.0/directoryObjects/" + userId,
                    };
                    await graphClient.Groups[CEQIPolicyMFAGrant].Members.Ref.PostAsync(requestBody);

                    Console.WriteLine("User successfullu Added in CEQIPolicyMFAGrant Group!!!");
                }
                catch (Exception ex)
                {
                }
            }
        }

    }
}
