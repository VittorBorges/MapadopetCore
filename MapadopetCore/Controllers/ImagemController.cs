using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MapadopetCore.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MapadopetCore.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Consumes("application/json","Application/json-patch+json","multipart/form-data")]
    public class ImagemController : Controller
    {
        private readonly IImagemRepository _ImagemRepository;

        public ImagemController(IImagemRepository imagemRepository)
        {
            _ImagemRepository = imagemRepository;
        }


        [HttpPost]
        public async Task<IActionResult> PostFotoPet(Microsoft.AspNetCore.Http.IFormFile file)
        {
            Models.Imagem i = new Models.Imagem();
            i.imgStream = file.OpenReadStream();
            i.caminho = $"c://imagens//{file.FileName}";

            _ImagemRepository.AddImagem(i);

            return null;
        }

        //public async Task<string> UploadFileAsBlob(Stream stream, string filename)
        //{
        //    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_configuration["ConnectionString:StorageConnectionString"]);
        //    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        //    CloudBlobContainer container = blobClient.GetContainerReference("profileimages");
        //    CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

        //    await blockBlob.UploadFromStreamAsync(stream);

        //    stream.Dispose();
        //    return blockBlob?.Uri.ToString();
        //}
    }
}
