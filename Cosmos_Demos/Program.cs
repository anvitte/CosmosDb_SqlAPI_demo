using Cosmos_Demos.Helpers;
using Cosmos_Demos.Models;
using Cosmos_Demos.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cosmos_Demos
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProgram();
        }
        public static async void RunProgram()
        {

            Console.WriteLine("Hello World! from Cosmos World - Anvitte");
           var _clientService = new CosmosClientService();


            //Get Database If Exist or Create Database.
            Console.WriteLine("---------------Database creation Module start---------------------------");

            var databaseId = await _clientService.GetDbOrCreateIfDbNotExist(Common.DatabaseId);
            Console.WriteLine("Database Id {0}", databaseId);
            Console.WriteLine("---------------Database creation Module Finish");

            Console.WriteLine("---------------Container Creation Module Start--------------------------");
            var containerId = await _clientService.GetContainerOrCreateContainerIfNotExist(databaseId, Common.ContainerId);
            Console.WriteLine("Container Id {0}", containerId);
            Console.WriteLine("---------------Container Creation Module finish-------------------------");
            Console.WriteLine();

            Console.WriteLine("---------------Get Container Object Module Start------------------------");
            //Get Container Record
            var objContainer = _clientService.GetContainer(databaseId, containerId);
            Console.WriteLine("---------------Get Container Object Module finish-----------------------");
            Console.WriteLine();
            //Check Record if exist else Insert Record 
            Console.WriteLine("---------------Insert Module Start--------------------------------------");
            Console.WriteLine("Ist record entered");
            var result = await _clientService.InsertData(objContainer, new EmployeeModel()
            {
                EmployeeName = "Ankit Tyagi",
                Location = "Riyadh, Saudi Arabia",
                EmailId = "Ankittyagi366@gmail.com",
                City = "Riyadh",
                Address = "Abdul Aziz Dist.",
                Department = "MAS",
                Band = "B2",
                Id = "1",
                EmployeeId = "12312",
                Country = "Saudi Arabia",
                Role = "Senior Project Engineer"
            });

            Console.WriteLine(result);

            Console.WriteLine("Second record entered");
            var result1 = await _clientService.InsertData(objContainer, new EmployeeModel()
            {
                EmployeeName = "Manish Tyagi",
                Location = "Delhi, india",
                EmailId = "Manishtyagi12@gmail.com",
                City = "delhi",
                Address = "Shalimar Garden",
                Department = "HR",
                Band = "B1",
                Salary = 152000,
                EmployeeId = "12314",
                Country = "India",
                Role = "Senior Consultant"
            });
            // Read All Record using query

            Console.WriteLine("---------------Reading Module Start.............-----------------------");
            Console.WriteLine();
            var LstEmployeeModel = await _clientService.GetAllData(objContainer);
            foreach (var item in LstEmployeeModel)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }
            Console.WriteLine("---------------Reading Module Completed -------------------------------");
            Console.WriteLine();
            Console.WriteLine("---------------Updating Module start-----------------------------------");
            //Updating Employeed model with salary and role
            Console.WriteLine("Old Model before update");
            Console.WriteLine(JsonConvert.SerializeObject(LstEmployeeModel[0]));
            LstEmployeeModel[0].Salary = 200000;
            LstEmployeeModel[0].Role = "Technical Lead";
            Console.WriteLine("Updating ........");
            var updatedModel = await _clientService.UpdateData(LstEmployeeModel[0], objContainer, LstEmployeeModel[0].Id);
            Console.WriteLine("Updated ........");
            Console.WriteLine("New Model after update");
            Console.WriteLine(JsonConvert.SerializeObject(updatedModel));
            Console.WriteLine("---------------Updating Module finish---------------------------------");
            Console.Write("");
            //Deleting
            Console.WriteLine("---------------Deleting Module start----------------------------------");

            var deletedItem = _clientService.DeleteDataById(LstEmployeeModel[1].Department, objContainer, LstEmployeeModel[1].Id);
            Console.WriteLine("Deleted Item");
            Console.WriteLine(JsonConvert.SerializeObject(LstEmployeeModel[1]));
            Console.WriteLine("----------------Deleting Module Finish--------------------------------");

            Console.WriteLine("Demo Completed");
            Console.ReadKey();
        }
    }
}
