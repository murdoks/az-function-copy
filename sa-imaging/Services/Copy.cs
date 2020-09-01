using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace sa_imaging.Services
{
    public class Copy : ICopy
    {
        private readonly IConfiguration config;

        public Copy(IConfiguration config)
        {
            this.config = config;
        }
        public async Task<CloudBlockBlob> CopyBlob(CloudBlockBlob nitBlob, ILogger log)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(config.GetValue<string>("sa-connect"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("containerbackup");

            // si container existe
            if (!await container.ExistsAsync())
                await container.CreateIfNotExistsAsync();

            
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(nitBlob.Name);
            log.LogInformation($"Blob destination {blockBlob.Name}");

            log.LogInformation("starting copy ... ");

                        await blockBlob.StartCopyAsync(nitBlob);


            log.LogInformation("Operation completed");

            await nitBlob.DeleteAsync();
            return blockBlob;
        }
    }
}
