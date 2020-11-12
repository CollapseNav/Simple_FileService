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
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Api.Controller
{
    [Route("File")]
    public class FileController : ControllerBase
    {
        private readonly FileServConfig _config;
        private readonly FileDbContext _context;
        private readonly ILogger _log;
        public FileController(ILogger<FileController> logger, FileServConfig config, FileDbContext dbContext)
        {
            _log = logger;
            _config = config;
            _context = dbContext;
        }

        [DisableRequestSizeLimit]
        [HttpPost("PostFiles")]
        public async Task<ReturnFileMapPath> PostFiles(IFormFile[] files)
        {
            var fileIds = new List<string>();
            foreach (var file in files)
                fileIds.Add(await SaveFile(file));
            return new ReturnFileMapPath { Total = fileIds.Count, FileId = fileIds };
        }

        [DisableRequestSizeLimit]
        [HttpPost("PostSingleFile")]
        public async Task<string> PostFile(IFormFile file)
        {
            return await SaveFile(file);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string fileName = GetFileName(file.FileName);
            try
            {
                using FileStream fs = new FileStream(fileName, FileMode.CreateNew);
                await file.CopyToAsync(fs);
            }
            catch (IOException)
            {
                throw new IOException("文件上传失败,请稍候重试.");
            }
            var mod = new Model.File
            {
                AddTime = DateTime.Now,
                Ext = file.FileName.Split(".")[^1],
                FileName = file.FileName,
                MapPath = _config.FrontExt + "/" + file.FileName,
                Id = Guid.NewGuid(),
            };
            _context.Files.Add(mod);
            _context.SaveChanges();
            _log.LogInformation($"Save File {file.FileName} , size: {file.Length}; type: {file.FileName.Split(".")[^1]};");
            return mod.Id.ToString();
        }

        private string GetFileName(string fileName)
        {
            var filePath = _config.FullPath + "/";
            string fileExt = "." + fileName.Split(".")[^1];
            if (_config.UseRawName)
            {
                filePath += fileName.Split(".")[0..^1].Join("");
                if (System.IO.File.Exists(filePath + fileExt))
                    filePath += "_" + DateTime.Now.Millisecond;
            }
            else
                filePath += Guid.NewGuid().ToString();
            filePath += fileExt;
            return filePath;
        }

        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(Guid? id)
        {
            var file = _context.Files.First(item => item.Id == id);
            var filepath = _config.FileStore + "/" + file.MapPath;
            var memoryStream = new MemoryStream();
            using var stream = new FileStream(filepath, FileMode.Open);
            await stream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            _log.LogInformation($"Download File {file.FileName} , Id : {id.Value}");
            Response.Headers.Add("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.FileName, Encoding.UTF8));
            return new FileStreamResult(memoryStream, "application/octet-stream");
        }

        [HttpGet("GetFileInfo")]
        public async Task<Model.File> GetFileInfo(Guid? id)
        {
            return await _context.Files.FindAsync(id);
        }
    }
}