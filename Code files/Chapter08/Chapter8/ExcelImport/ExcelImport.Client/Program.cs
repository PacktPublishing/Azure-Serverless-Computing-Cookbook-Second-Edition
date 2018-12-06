using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExcelImport.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                UploadBlob().Wait();
                Console.WriteLine("Successfully Uploaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error has occured with the message" + ex.Message);
            }
        }
        private static async Task UploadBlob()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("StorageConnection"));

            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer excelBlobContainer = cloudBlobClient.GetContainerReference("excelimports");
            await excelBlobContainer.CreateIfNotExistsAsync();

            CloudBlockBlob cloudBlockBlob = excelBlobContainer.GetBlockBlobReference("EmployeeInformation" + Guid.NewGuid().ToString());

            await cloudBlockBlob.UploadFromFileAsync(@"C:\Users\vmadmin\source\repos\POC\ImportExcelPOC\ImportExcelPOC\EmployeeInformation.xlsx");

        }
    }
}
