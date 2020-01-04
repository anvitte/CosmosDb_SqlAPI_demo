using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos_Demo.Helpers
{
    public static class Common
    {
        //private const string EndpointUrl = "https://<your-account>.documents.azure.com:443/";
        //private const string AuthorizationKey = "<your-account-key>";
        //private const string DatabaseId = "FamilyDatabase";
        //private const string ContainerId = "FamilyContainer";

        public const string EnpointUrl = "https://mycosmosapi.documents.azure.com:443/";
        public const string AuthorizationKey = "HfIiLjLsgMrtJLUDcXYkbWEiI3852uFT8YX142oCOZagoarB4koYhAFrRuCmkGZwk1k8kifFlnpD283Lxo7vMg==";
        public const string DatabaseId = "Employee_DB";
        public const string ContainerId = "Employees";
        public const string employeesPartitionKey = "/department";

        //Query

        public const string GetAllEmployeeSqlQuery = "Select * from employees";

        public const string GetEmployeeByIdSqlQuery = "Select * from {0} where EmployeeId={1}";

    }
}
