using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
{
    [Route("File")]
    public class FileController : ControllerBase
    {
        private readonly FileServConfig _config;
        private readonly FileDbContext _context;
        public FileController(FileServConfig config, FileDbContext dbContext)
        {
            _config = config;
            _context = dbContext;
        }

        [DisableRequestSizeLimit]
        [HttpPost("PostFiles")]
        public async Task<ReturnFileMapPath> PostFiles(IFormFile[] files)
        {
            var fileIds = new List<Guid>();
            foreach (var file in files)
            {
                string fileName = "/" + Guid.NewGuid().ToString() + "." + file.FileName.Split(".")[^1];
                using FileStream fs = new FileStream(_config.FullPath + fileName, FileMode.CreateNew);
                await file.CopyToAsync(fs);
                var mod = new Model.File
                {
                    AddTime = DateTime.Now,
                    Ext = file.FileName.Split(".")[^1],
                    FileName = file.FileName,
                    MapPath = _config.FrontExt + fileName,
                    Id = Guid.NewGuid(),
                };
                _context.Files.Add(mod);
                fileIds.Add(mod.Id.Value);
                _context.SaveChanges();
            }
            return new ReturnFileMapPath { Total = fileIds.Count, FileId = fileIds };
        }

        [DisableRequestSizeLimit]
        [HttpPost("PostSingleFile")]
        public async Task<Guid> PostFile(IFormFile file)
        {
            string fileName = "/" + Guid.NewGuid().ToString() + "." + file.FileName.Split(".")[^1];
            using FileStream fs = new FileStream(_config.FullPath + fileName, FileMode.CreateNew);
            await file.CopyToAsync(fs);
            var mod = new Model.File
            {
                AddTime = DateTime.Now,
                Ext = file.FileName.Split(".")[^1],
                FileName = file.FileName,
                MapPath = _config.FrontExt + fileName,
                Id = Guid.NewGuid(),
            };
            _context.Files.Add(mod);
            _context.SaveChanges();
            return mod.Id.Value;
        }

        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(Guid? id)
        {
            var file = _context.Files.First(item => item.Id == id);
            var filepath = _config.FileStore + file.MapPath;
            var memoryStream = new MemoryStream();
            using var stream = new FileStream(filepath, FileMode.Open);
            await stream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.FileName, Encoding.UTF8));
            return new FileStreamResult(memoryStream, "application/octet-stream");
        }
    }
}