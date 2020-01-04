using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Cosmos_Demo.Helpers;
using Cosmos_Demo.Models;
using Microsoft.Azure.Cosmos;

namespace Cosmos_Demo.Services
{
    public class CosmosClientService
    {
        public CosmosClient _cosmosClient;
        public CosmosClientService()
        {
            _cosmosClient = new CosmosClient(Common.EndpointUrl, Common.AuthorizationKey);
        }

        public async Task<string> GetDbOrCreateIfDbNotExist(string databaseId)
        {

            try
            {
                var database = _cosmosClient.GetDatabase(databaseId);
                if (database != null)
                {
                    var response =  _cosmosClient.CreateDatabaseIfNotExistsAsync(database.Id, null, null).Result;
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
        public async Task<string> GetContainerOrCreateContainerIfNotExist(string databaseId, string containerId)
        {
            try
            {
                var container = GetContainer(databaseId, containerId);
                if (container != null)
                {
                    ContainerProperties containerProperies = new ContainerProperties(Common.ContainerId, Common.employeesPartitionKey);
                    var response =  _cosmosClient.GetDatabase(databaseId).CreateContainerIfNotExistsAsync(containerProperies).Result;
                    return response.Container.Id;
                }
                else
                    return container.Id;

            }
            catch (CosmosException ex )
            {
                return ex.ToString();
                throw;
            }
            catch(Exception ex)
            {
                return ex.ToString();
            }

        }

        public Container GetContainer(string databaseid, string containerId)
        {
            return _cosmosClient.GetContainer(databaseid, containerId);
        }

        public async Task<string> InsertData(Container objContainer, EmployeeModel employeeModel)
        {

            try
            {
                //CheckIfExist
                ItemResponse<EmployeeModel> objModel = objContainer.ReadItemAsync<EmployeeModel>(employeeModel.Id, new PartitionKey(employeeModel.Department)).Result;
                if (objModel != null)
                {
                    objModel = null;
                    return "Record Already Exists";
                }

                ItemResponse<EmployeeModel> itemResponse =  objContainer.CreateItemAsync(employeeModel, new PartitionKey(employeeModel.Department)).Result;
                if (itemResponse != null)
                {
                    return "Inserted Sucessfully";
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<EmployeeModel>> GetAllDataAsync(Container objContainer)
        {
            try
            {
                List<EmployeeModel> lstModel = new List<EmployeeModel>();
                QueryDefinition queryDefination = new QueryDefinition(Common.GetAllEmployeeSqlQuery);

                var query = objContainer.GetItemQueryIterator<EmployeeModel>(queryDefination);
             
                while (query.HasMoreResults)
                {
                    var response =  query.ReadNextAsync().Result;
                    foreach (var item in response)
                    {
                        
                        lstModel.Add(item);
                    }

                }


                return lstModel;
            }
            catch (Exception)
            {


                throw;
            }
        }

        public async Task<EmployeeModel> UpdateData(EmployeeModel objModel, Container objContainer, string id)
        {
            return  objContainer.UpsertItemAsync(objModel, new PartitionKey(objModel.Department)).Result;
        }
        public async Task<EmployeeModel> DeleteDataById(string partitionKey, Container objContainer, string id)
        {
            return  objContainer.DeleteItemAsync<EmployeeModel>(id, new PartitionKey(partitionKey)).Result;
        }

    }
}
