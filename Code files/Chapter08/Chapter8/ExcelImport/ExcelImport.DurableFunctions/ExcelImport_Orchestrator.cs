using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace ExcelImport.DurableFunctions
{
    public static class ExcelImport_Orchestrator
    {
        [FunctionName("ExcelImport_Orchestrator")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] DurableOrchestrationContext context)
        {
            var outputs = new List<string>();
            string ExcelFileName = context.GetInput<string>();
            List<Employee> employees = await context.CallActivityAsync<List<Employee>>("ReadExcel_AT", ExcelFileName);

            await context.CallActivityAsync<string>("ScaleRU_AT", 500);

            await context.CallActivityAsync<string>("ImportData_AT", employees);

            return outputs;
        }
        [FunctionName("ReadExcel_AT")]
        public static async Task<List<Employee>> ReadExcel_AT(
            [ActivityTrigger] string name,
            ILogger log)
        {
            log.LogInformation("Orchestration started");

            StorageManager storageManager = new StorageManager();
            Stream stream = null;

            log.LogInformation("Reading the Blob Started");
            stream = await storageManager.ReadBlob(name);
            log.LogInformation("Reading the Blob has Completed");

            EPPLusExcelManager ePPLusExcelManager = new EPPLusExcelManager();

            log.LogInformation("Reading the Excel Data Started");
            List<Employee> employees = ePPLusExcelManager.ReadExcelData(stream);
            log.LogInformation("Reading the Blob has Completed");
           
            return employees;
        }

        [FunctionName("ScaleRU_AT")]
        public static async Task<string> ScaleRU_AT(
           [ActivityTrigger] int RequestUnits,
           [CosmosDB(ConnectionStringSetting = "CosmosDBConnectionString")]DocumentClient client
           )
        {
            DocumentCollection EmployeeCollection = await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri("cookbookdb", "EmployeeCollection"));
            Offer offer = client.CreateOfferQuery().Where(o => o.ResourceLink == EmployeeCollection.SelfLink).AsEnumerable().Single();
            Offer replaced = await client.ReplaceOfferAsync(new OfferV2(offer, RequestUnits));
            return $"The RUs are scaled to 500 RUs!";
        }

        [FunctionName("ImportData_AT")]
        public static async Task<string> ImportData_AT(
            [ActivityTrigger] List<Employee> employees,
             [CosmosDB(ConnectionStringSetting = "CosmosDBConnectionString")]DocumentClient client,
            ILogger log)
        {
            foreach (Employee employee in employees)
            {
                await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("cookbookdb", "EmployeeCollection"), employee);
                log.LogInformation($"Successfully inserted {employee.Name}.");
            }
            return $"Data has been imported to Cosmos DB Collection Successfully!";
        }
        
        [FunctionName("ExcelImport_Orchestrator_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("ExcelImport_Orchestrator", null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}