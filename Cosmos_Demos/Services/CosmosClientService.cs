using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Cosmos_Demos.Helpers;
using Cosmos_Demos.Models;
using Microsoft.Azure.Cosmos.Client;


namespace Cosmos_Demos.Services
{
    public class CosmosClientService
    {
        public CosmosClient _cosmosClient;
        public CosmosClientService()
        {
            _cosmosClient = new CosmosClient(Common.EnpointUrl, Common.AuthorizationKey);
        }
        
        public async Task<string> GetDbOrCreateIfDbNotExist(string databaseId)
        {

            try
            {
                var database = _cosmosClient.GetDatabase(databaseId);
                if (database!=null)
                {
                    var response = await this._cosmosClient.CreateDatabaseIfNotExistsAsync(database.Id,null,null);
                   
                    return response.Database.Id;
                }
                else
                {
                    return database.Id;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task<string> GetContainerOrCreateContainerIfNotExist(string databaseId,string containerId)
        {
            try
            {
                var container = GetContainer(databaseId,containerId);
                if (container != null)
                {
                    ContainerProperties containerProperies = new ContainerProperties(Common.ContainerId, Common.employeesPartitionKey);
                    var response = await _cosmosClient.GetDatabase(databaseId).CreateContainerIfNotExistsAsync(containerProperies);
                    return response.Container.Id;
                }
                else
                    return container.Id;
               
            }
            catch (Exception)
            {

                throw;
            }

        }

        public Container GetContainer(string databaseid,string containerId)
        {
            return _cosmosClient.GetContainer(databaseid, containerId);
        }

        public async Task<string> InsertData(Container objContainer,EmployeeModel employeeModel)
        {

            try
            {
                //CheckIfExist
                ItemResponse<EmployeeModel> objModel = await objContainer.ReadItemAsync<EmployeeModel>(employeeModel.Id, new PartitionKey(employeeModel.Department));
                if (objModel != null)
                {
                    return "Record Already Exists";
                }

                var itemResponse = await objContainer.CreateItemAsync(employeeModel, new PartitionKey(employeeModel.Department));
                if (itemResponse.StatusCode == HttpStatusCode.OK)
                {
                    return "Inserted Sucessfully";
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<List<EmployeeModel>> GetAllData(Container objContainer)
        {
            try
            {
                List<EmployeeModel> lstModel = new List<EmployeeModel>();
                QueryDefinition queryDefination = new QueryDefinition(Common.GetAllEmployeeSqlQuery);
                var query= objContainer.GetItemQueryIterator<EmployeeModel>(queryDefination);
              
                while (query.HasMoreResults)
                {
                    var response = await query.ReadNextAsync();
                    lstModel.AddRange(response.ToList());
                }
                return lstModel;
            }
            catch (Exception)
            {


                throw;
            }
        }

        public async Task<EmployeeModel> UpdateData(EmployeeModel objModel,Container objContainer,string id)
        {
           return await  objContainer.UpsertItemAsync(objModel, new PartitionKey(objModel.Department));
        }
        public async Task<EmployeeModel> DeleteDataById(string  partitionKey,Container objContainer,string id)
        {
           return await objContainer.DeleteItemAsync<EmployeeModel>(id, new PartitionKey(partitionKey));
        }

    }
}
