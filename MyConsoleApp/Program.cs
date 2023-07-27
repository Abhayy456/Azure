using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;

namespace MyConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            var connectionString = configuration["StorageAccountConnectionString"];
            if (!CloudStorageAccount.TryParse(connectionString,
        out CloudStorageAccount storageAccount))
            {
                Console.WriteLine("Unable to parse connection string");
                return;
            }
            var blobClient = storageAccount.CreateCloudBlobClient();
            var blobContainer = blobClient.GetContainerReference("source");
            bool created = await blobContainer.CreateIfNotExistsAsync();
            Console.WriteLine(created ? "Created the Blob container" : "Blob container already exists.");

            //Create blockBlob Reference and Read data from local file and write to blob
            var blockBlob = blobContainer.GetBlockBlobReference("sample.csv");
            using (Stream myStream = await blockBlob.OpenWriteAsync())
            {
                byte[] data = File.ReadAllBytes(@"C:\Users\abhay.j.sharma\OneDrive - Accenture\Desktop\input\sample.csv");
                myStream.Write(data);
            }
            Console.WriteLine("File Uploaded successfully.");

        }
    }
}



























// using Azure.Storage.Blobs;
// using Azure.Storage.Blobs.Models;
// using System;
// using System.IO;

// namespace MyConsoleApp
// {
//     class Program
//     {
//         static async Task Main(string[] args)
//         {
//             string connectionString = "DefaultEndpointsProtocol=https;AccountName=abhaystorage2;AccountKey=DO7TNUuaikV8zLZOXX30u0qVZ0QsPdT4XgOucNXQ7/J9j2E2Eoani6WXrpRslkKfK76rZR3Z3Z1x+ASt55Tp1w==;EndpointSuffix=core.windows.net";
//             string containerName = "source";

//             BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
//             BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

//             string localDirectoryPath = @"C:\Users\abhay.j.sharma\OneDrive - Accenture\Desktop\input";
//             string[] files = Directory.GetFiles(localDirectoryPath);

//             foreach (string filePath in files)
//             {
//                 string fileName = Path.GetFileName(filePath);
//                 BlobClient blobClient = containerClient.GetBlobClient(fileName);
//                 using (FileStream fileStream = File.OpenRead(filePath))
//                 {
//                     BlobUploadOptions options = new BlobUploadOptions
//                     {
//                         HttpHeaders = new BlobHttpHeaders
//                         {
//                             Timeout = TimeSpan.FromMinutes(5)
//                         }
//                     };
//                     await blobClient.UploadAsync(fileStream, options);
//                 }
//             }
//         }
//     }
// }





// using Azure.Storage.Blobs;


// class Program
// {
//     static async Task Main(string[] args)
//     {
//         string connectionString = "DefaultEndpointsProtocol=https;AccountName=abhaystorage2;AccountKey=DO7TNUuaikV8zLZOXX30u0qVZ0QsPdT4XgOucNXQ7/J9j2E2Eoani6WXrpRslkKfK76rZR3Z3Z1x+ASt55Tp1w==;EndpointSuffix=core.windows.net";
//         string containerName = "source";

//         BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
//         BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

//         string localDirectoryPath = @"C:\Users\abhay.j.sharma\OneDrive - Accenture\Desktop\input";
//         string[] files = Directory.GetFiles(localDirectoryPath);

//         foreach (string filePath in files)
//         {
//             string fileName = Path.GetFileName(filePath);
//             BlobClient blobClient = containerClient.GetBlobClient(fileName);
//             using (FileStream fileStream = File.OpenRead(filePath))
//             {
//                 await blobClient.UploadAsync(fileStream, true);
//             }
//         }
//     }
// }





// using Azure.Storage.Blobs;

// string connectionString = "DefaultEndpointsProtocol=https;AccountName=abhaystorage2;AccountKey=DO7TNUuaikV8zLZOXX30u0qVZ0QsPdT4XgOucNXQ7/J9j2E2Eoani6WXrpRslkKfK76rZR3Z3Z1x+ASt55Tp1w==;EndpointSuffix=core.windows.net";
// string containerName = "source";

// BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
// BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

// string localDirectoryPath = "C:/Users\abhay.j.sharma/OneDrive - Accenture/Desktop/input";
// string[] files = Directory.GetFiles(localDirectoryPath);

// foreach (string filePath in files)
// {
//     string fileName = Path.GetFileName(filePath);
//     BlobClient blobClient = containerClient.GetBlobClient(fileName);
//     using (FileStream fileStream = File.OpenRead(filePath))
//     {
//         await blobClient.UploadAsync(fileStream, true);
//     }
// }

