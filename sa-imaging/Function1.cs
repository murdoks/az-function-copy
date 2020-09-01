using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using sa_imaging.Services;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Threading.Tasks;

namespace sa_imaging
{
    public class Function1
    {
        private readonly ICopy copy;

        public Function1(ICopy copy)
        {
            this.copy = copy;
        }

        [FunctionName("sa-copy")]
        public async Task Run([BlobTrigger("container-blob/{name}", Connection = "sa-connect")]CloudBlockBlob myBlob, ILogger log)
        {
            CloudBlockBlob x = await copy.CopyBlob(myBlob, log);
            if (await x.ExistsAsync()) log.LogInformation("Copy Done ...");
            else
                log.LogInformation("Copy do not Done ... ");

            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{myBlob.Name} \n Size Bytes");                          
        }
    }
}
