using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Api.Controller
{
    [Route("api/[controller]")]
    public class DirController : ControllerBase
    {
        private readonly FileServConfig _config;
        private readonly FileDbContext _context;
        private DbSet<Model.File> _file;
        private DbSet<Dir> _dir;
        private readonly ILogger _log;
        public DirController(ILogger<FileController> logger, FileServConfig config, FileDbContext dbContext)
        {
            _log = logger;
            _config = config;
            _context = dbContext;
            _file = _context.Files;
            _dir = _context.Dirs;
        }

        [HttpPost, Route("CreateDir")]
        public async Task<Dir> CreateDir([FromBody] Dir input)
        {
            input.Init();
            input.MapPath = string.Empty;
            if (input.Parent != null)
            {
                input.MapPath += input.Parent.MapPath + "/" + input.FileName;
            }
            else if (input.ParentId.HasValue)
            {
                var dir = await _dir.FindAsync(input.ParentId);
                input.MapPath += dir.MapPath + "/" + input.FileName;
            }
            if (Directory.Exists(input.MapPath)) return null;


            await _dir.AddAsync(input);
            await _context.SaveChangesAsync();

            Directory.CreateDirectory(_config.FileStore + _config.FullPath + input.MapPath);
            return input;
        }

        [HttpGet, Route("GetRootDir")]
        public async Task<Dir> GetRootDir()
        {
            return await _dir.Where(item => item.MapPath == string.Empty).Include(item => item.Dirs).Include(item => item.Files).FirstAsync();
        }

        [HttpGet, Route("GetDirTree")]
        public async Task<Dir> GetDirTree(Guid? id)
        {
            var dir = await _dir.Where(item => item.Id == id).Include(item => item.Dirs).Include(item => item.Files).FirstOrDefaultAsync();
            return dir;
        }

        /// <summary>
        /// 根据Id获取文件信息
        /// </summary>
        [HttpGet("GetDirInfo")]
        public async Task<Dir> GetDirInfo(Guid? id)
        {
            return await _dir.FindAsync(id);
        }
    }
}
