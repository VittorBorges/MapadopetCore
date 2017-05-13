﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using MapadopetCore.Interfaces;
using MapadopetCore.Models;
using MongoDB.Bson;
using System.IO;


namespace MapadopetCore.Data
{
    public class ImagemLocalRepository : IImagemRepository
    {
        private readonly MapadopetContext _context = null;

        public ImagemLocalRepository(IOptions<Settings> settings)
        {
            _context = new MapadopetContext(settings);
        }

        public void AddImagem(Imagem item)
        {
            SaveFileStream(item.caminho, item.imgStream);
        }

        private void SaveFileStream(String path, Stream stream)
        {
            var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            stream.CopyTo(fileStream);
            fileStream.Dispose();
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
