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
        public string companyId { get; set; }
        public string companyName { get; set; }
        public string employeeAddressLine1 { get; set; }
        public string employeeAddressLine2 { get; set; }
        public string employeeBadgeClockNumber { get; set; }
        public string employeeCity { get; set; }
        public string employeeCostCenter1 { get; set; }
        public string employeeCostCenter2 { get; set; }
        public string employeeCostCenter3 { get; set; }
        public string employeeEEOClass { get; set; }
        public string employeeFirstName { get; set; }
        public string employeeGender { get; set; }
        public DateTime employeeHireDate { get; set; }
        public int employeeId { get; set; }
        public string employeeJobTitle { get; set; }
        public string employeeLastName { get; set; }
        public string employeeMaritalStatus { get; set; }
        public string employeeMiddleInitial { get; set; }
        public string employeePayFrequency { get; set; }
        public string employeePayType { get; set; }
        public string employeePosition { get; set; }
        public string employeeState { get; set; }
        public string employeeSupervisor { get; set; }
        public string employeeSupervisorId { get; set; }
        public string employeeType { get; set; }
        public string employeeWorkEMailAddress { get; set; }
        public string employeeWorkPhone { get; set; }
        public string employeeZip { get; set; }
        public string employeeTaxForm { get; set; }
    }
}
