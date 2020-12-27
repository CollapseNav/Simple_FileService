using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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

        /// <summary>
        /// 多文件上传
        /// </summary>
        [DisableRequestSizeLimit]
        [HttpPost("PostFiles")]
        public async Task<ReturnFileMapPath> PostFiles(IFormFile[] files)
        {
            var fileIds = new List<string>();
            foreach (var file in files)
                fileIds.Add(await SaveFile(file));
            return new ReturnFileMapPath { Total = fileIds.Count, FileId = fileIds };
        }
        /// <summary>
        /// 单文件上传
        /// </summary>
        [DisableRequestSizeLimit]
        [HttpPost("PostSingleFile")]
        public async Task<string> PostFile(IFormFile file)
        {
            return await SaveFile(file);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            string fileName = GetFileName(file.FileName);
            try
            {
                using FileStream fs = new FileStream(_config.FullPath + "/" + fileName, FileMode.CreateNew);
                await file.CopyToAsync(fs);
            }
            catch (IOException)
            {
                throw new IOException("文件上传失败,请稍候重试.");
            }
            var mod = new Model.File
            {
                AddTime = DateTime.Now,
                Ext = extension,
                FileName = file.FileName,
                MapPath = _config.FrontExt + "/" + fileName,
                Size = file.Length.ToString(),
                Id = Guid.NewGuid(),
                ContentType = file.ContentType
            };
            _context.Files.Add(mod);
            _context.SaveChanges();
            _log.LogInformation($"Save File {file.FileName} , size: {file.Length}; type: {extension};");
            return mod.Id.ToString();
        }

        private string GetFileName(string fileName)
        {
            var filePath = _config.FullPath + "/";
            string fileExt = Path.GetExtension(fileName);
            if (_config.UseRawName)
            {
                fileName = fileName.Split(".")[0..^1].Join(".");
                if (System.IO.File.Exists(filePath + fileName + fileExt))
                    fileName += "_" + DateTime.Now.Millisecond;
            }
            else
                fileName += Guid.NewGuid().ToString();
            fileName += fileExt;
            return fileName;
        }
        /// <summary>
        /// 根据文件Id获取文件
        /// </summary>
        [HttpGet("GetFile")]
        public async Task<IActionResult> GetFile(Guid? id)
        {
            var range = Request.Headers["Range"].ToString();
            if (range.Length > 5)
                range = range[(range.IndexOf("=") + 1)..range.IndexOf("-")];
            var file = _context.Files.First(item => item.Id == id);
            var filepath = _config.FileStore + "/" + file.MapPath;
            var memoryStream = new MemoryStream();
            using var stream = new FileStream(filepath, FileMode.Open);
            await stream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            _log.LogInformation($"Download File {file.FileName} , Id : {id.Value}");


            Response.Headers.Add("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(file.FileName, Encoding.UTF8));
            Response.Headers.Add("Content-Type", file.ContentType);
            Response.ContentLength = long.Parse(file.Size);
            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.Headers.Add("Content-Range", ((string.IsNullOrEmpty(range.ToString()) ? 0 : int.Parse(range)) + (long.Parse(file.Size) - 1)).ToString());
            return new FileStreamResult(memoryStream, "application/octet-stream");
        }

        /// <summary>
        /// 根据Id获取文件信息
        /// </summary>
        [HttpGet("GetFileInfo")]
        public async Task<Model.File> GetFileInfo(Guid? id)
        {
            return await _context.Files.FindAsync(id);
        }
    }
}