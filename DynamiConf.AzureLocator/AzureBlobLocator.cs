using System;
using DynamiConf.Helpers;
using Microsoft.WindowsAzure.Storage;

namespace DynamiConf.AzureLocator
{
    public static class AzureBlobLocator
    {
        public static DynamiConfiguration AzureBlockBlob(this LocationSources provider, string connectionString, string container, string blobPath, bool optional = false)
        {
            var blobClient = CloudStorageAccount.Parse(connectionString).CreateCloudBlobClient();
            var bcontainer = blobClient.GetContainerReference(container);

            var str = string.Empty;
            try
            {
                var blockBlob = bcontainer.GetBlockBlobReference(blobPath);
                str = blockBlob.DownloadText();
            }
            catch (Exception)
            {
                if (!optional)
                    throw;
            }

            provider.RegisterConfiguration(provider.Interpreter.ParseConfiguration(str));

            return provider.Configuration;
        }

        public static DynamiConfiguration AzureBlockBlob(this LocationSources provider,
            Func<dynamic, dynamic> connectionStringSelector,
            Func<dynamic, dynamic> containerSelector,
            Func<dynamic, dynamic> blobPathSelector,
            bool optional = false)
        {
            try
            {
                return AzureBlockBlob(provider,
                    connectionStringSelector.Invoke(provider.Configuration.GetConfiguration()) as string,
                    containerSelector.Invoke(provider.Configuration.GetConfiguration()) as string,
                    blobPathSelector.Invoke(provider.Configuration.GetConfiguration()) as string,
                    optional
                    );
            }
            catch (Exception)
            {
                if (!optional)
                    throw;
            }

            return provider.Configuration;
        }
    }
}