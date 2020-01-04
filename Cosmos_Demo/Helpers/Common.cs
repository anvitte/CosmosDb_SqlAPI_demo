using System;
using System.Collections.Generic;
using System.Text;

namespace Cosmos_Demo.Helpers
{
    public static class Common
    {
        public const string EndpointUrl = "https://<your-account>.documents.azure.com:443/";
        public const string AuthorizationKey = "<your-account-key>";
        public const string DatabaseId = "FamilyDatabase";
        public const string ContainerId = "FamilyContainer";


        public const string employeesPartitionKey = "/department";

        //Query

        public const string GetAllEmployeeSqlQuery = "Select * from employees";

        public const string GetEmployeeByIdSqlQuery = "Select * from {0} where EmployeeId={1}";

    }
}
