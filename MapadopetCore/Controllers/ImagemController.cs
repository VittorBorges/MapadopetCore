using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MapadopetCore.Interfaces;
using Microsoft.AspNetCore.StaticFiles;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MapadopetCore.Controllers
{

    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [Consumes("application/json","Application/json-patch+json","multipart/form-data")]
    public class ImagemController : Controller
    {

        private readonly IMarcaRepository _marcaRepository;
        private readonly IImagemRepository _ImagemRepository;

        public ImagemController(IImagemRepository imagemRepository, IMarcaRepository marcaRepository)
        {
            _ImagemRepository = imagemRepository;
            _marcaRepository = marcaRepository;
        }

        [HttpGet("{filename}")]
        public FileStreamResult show(string filename)
        {
            string contentType;
            filename = $"c:\\images\\{filename}";
            new FileExtensionContentTypeProvider().TryGetContentType(filename, out contentType);
            Stream stream = new MemoryStream(System.IO.File.ReadAllBytes(filename));
            return new FileStreamResult(stream, contentType);
        }

        [HttpPost]
        public async Task<string> PostFotoPet(Microsoft.AspNetCore.Http.IFormFile file)
        {
            if (validaArquivo(file))
            {
                Guid g;
                Models.Imagem i = new Models.Imagem();
                i.imgStream = file.OpenReadStream();
                i.fileName  = $"{Guid.NewGuid().ToString()}.{file.FileName.Split('.')?[1]}";
                i.patch = _ImagemRepository.AddImagem(i);
                return i.patch;
            }
            else return "erro";
        }

        public bool validaArquivo(Microsoft.AspNetCore.Http.IFormFile file)
        {
            //tipos suportados
            var type = "image/jpeg;image/png;image/bmp";
            if (file.Length > 4194304) return false;
            if (!type.Contains(file.ContentType)) return false;
            return true;
        }

        [HttpGet]
        public string categorizar()
        {
            string r = "";
            var l = _marcaRepository.GetMarcaAvalia();
            https://eastus.contentmoderator.cognitive.microsoft.com/mapadopet
            return r;
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
