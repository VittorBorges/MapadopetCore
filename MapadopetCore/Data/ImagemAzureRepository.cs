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
     "mapapetstorage",
     "ihV0FQiSwLbCWMv0QByo+LNqViFUjcPg3SSsWX0ig8LjuxblrlL9C/dbzjVbwOJ1OgRy6tXJlTnKvipARm/Xuw=="), true);


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

            return $"https://mapapetstorage.blob.core.windows.net/pets/{item.fileName}";

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
