using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using MapadopetCore.Interfaces;
using MapadopetCore.Models;
using MongoDB.Bson;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

using Microsoft.Extensions.Configuration;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace MapadopetCore.Data
{
    public class ImagemAzureRepository : IImagemRepository
    {
        private readonly MapadopetContext _context = null;
        private CloudStorageAccount storageAccount;

        public ImagemAzureRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);

            storageAccount = new CloudStorageAccount(
     new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(
     "storagemapadopet",
     "iZV0pN9pJhUjxt2W1OZ+z8dzhf63t0cSOWjZ6x8Iuu788yzUKzMX/Tjb+BF/DLT2z+L6jz48dqIk2I/+XF6rXg=="), true);


        }

        public string AddImagem(Imagem item)
        {
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("pets");
            //container.SetPermissionsAsync(new BlobContainerPermissions
            //{
            //    PublicAccess = BlobContainerPublicAccessType.Blob
            //});

            CloudBlockBlob blockBlob = container.GetBlockBlobReference(item.fileName);
            blockBlob.UploadFromStreamAsync(item.imgStream).Wait();

            return $"https://storagemapadopet.blob.core.windows.net/pets/{item.fileName}";

        }

        public Task<IEnumerable<Imagem>> GetAllImagens(string petId)
        {
            throw new NotImplementedException();
        }

        public Imagem GetImagem(string petId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RemoveImagem(string id)
        {
            throw new NotImplementedException();
        }

        
    }
}
