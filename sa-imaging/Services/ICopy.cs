using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace sa_imaging.Services
{
    public interface ICopy
    {
        public Task<CloudBlockBlob> CopyBlob(CloudBlockBlob nitBlob, ILogger log);
    }
}
