using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace ExcelImport.DurableFunctions
{
    class StorageManager
    {
        public async Task<Stream> ReadBlob(string BlobName)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(configuration.GetConnectionString("StorageConnection"));

            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer excelBlobContainer = cloudBlobClient.GetContainerReference("excel");
            CloudBlockBlob cloudBlockBlob = excelBlobContainer.GetBlockBlobReference(BlobName);

            return await cloudBlockBlob.OpenReadAsync();      

        }
    }
}
//Set Disclaimer that OpenReadAsync might not be the right API to call in every situation. Please be careful if the Blob that we are reading from other sources.