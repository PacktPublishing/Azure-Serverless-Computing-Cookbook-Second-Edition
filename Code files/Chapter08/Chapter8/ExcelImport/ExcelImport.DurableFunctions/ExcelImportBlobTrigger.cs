using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ExcelImport.DurableFunctions
{
    public static class ExcelImportBlobTrigger
    {
        [FunctionName("ExcelImportBlobTrigger")]
        public static async void Run(
                [BlobTrigger("excelimports/{name}", Connection = "StorageConnection")]Stream myBlob, 
                string name,
                [OrchestrationClient]DurableOrchestrationClient starter,
                ILogger log)
        {
            string instanceId = await starter.StartNewAsync("ExcelImport_Orchestrator", name);

            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");

        }
    }
}
