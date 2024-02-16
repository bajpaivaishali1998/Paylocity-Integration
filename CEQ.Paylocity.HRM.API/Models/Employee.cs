using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEQ.Paylocity.HRM.API.Models
{
    //public class Employee
    //{
    //    public string employeeId { get; set; }
    //    public string address1 { get; set; }
    //    public string address2 { get; set; }
    //    public string autoPayType { get; set; }
    //    public int baseRate { get; set; }
    //    public string city { get; set; }
    //    public string costCenter1 { get; set; }
    //    public string costCenter2 { get; set; }
    //    public string costCenter3 { get; set; }
    //    public int defaultHours { get; set; }
    //    public string employeeStatus { get; set; }
    //    public string employmentType { get; set; }
    //    public string federalFilingStatus { get; set; }
    //    public string firstName { get; set; }
    //    public string sex { get; set; }
    //    public string hireDate { get; set; }
    //    public string homePhone { get; set; }
    //    public string lastName { get; set; }
    //    public string maritalStatus { get; set; }
    //    public string personalMobilePhone { get; set; }
    //    public string payFrequency { get; set; }
    //    public string personalEmailAddress { get; set; }
    //    public string payType { get; set; }
    //    public string ratePer { get; set; }
    //    public int salary { get; set; }
    //    public string state { get; set; }
    //    public string ssn { get; set; }
    //    public string stateFilingStatus { get; set; }
    //    public string suiState { get; set; }
    //    public string taxForm { get; set; }
    //    public string taxState { get; set; }
    //    public string userName { get; set; }
    //    public string workEmailAddress { get; set; }
    //    public string zip { get; set; }
    //}
    public class Employee
    {
        public string CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string BadgeClockNumber { get; set; }
        public string City { get; set; }
        public string CostCenter1 { get; set; }
        public string CostCenter2 { get; set; }
        public string CostCenter3 { get; set; }
        public string EEOClass { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public int EmployeeId { get; set; }
        public string JobTitle { get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public string MiddleInitial { get; set; }
        public string PayFrequency { get; set; }
        public string PayType { get; set; }
        public string Position { get; set; }
        public string State { get; set; }
        public string Supervisor { get; set; }
        public string SupervisorId { get; set; }
        public string EmployeeType { get; set; }
        public string WorkEMailAddress { get; set; }
        public string WorkPhone { get; set; }
        public string Zip { get; set; }
        public string TaxForm { get; set; }
    }
    
    public class AadGroupMember
    {
        public string? ObjectId { get; set; }
        public string? Name { get; set; }
        public string? UserPrincipalName { get; set; }
        public string? Email { get; set; } //UserPrincipalName
        public string? OtherEmail { get; set; } //UserPrincipalName
    }

}
