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
    public class DirController : BaseController<Dir>
    {
        private readonly DbSet<Model.File> _file;

        public DirController(ILogger<FileController> logger, FileServConfig config, FileDbContext dbContext) : base(logger, config, dbContext)
        {
            _file = _context.Files;
        }

        protected override IQueryable<Dir> GetQuery(Dir input)
        {
            return _db.WhereIf(input.MapPath, item => item.MapPath == input.MapPath);
        }

        [HttpPost]
        public async override Task<Dir> AddAsync([FromBody] Dir input)
        {
            input.Init();
            input.MapPath = string.Empty;
            if (input.Parent != null)
            {
                input.MapPath += input.Parent.MapPath + "/" + input.FileName;
            }
            else if (input.ParentId.HasValue)
            {
                var dir = await FindAsync(input.ParentId);
                input.MapPath += dir.MapPath + "/" + input.FileName;
            }
            if (Directory.Exists(input.MapPath)) return null;

            await base.AddAsync(input);

            Directory.CreateDirectory(_config.FileStore + _config.FullPath + input.MapPath);
            return input;
        }

        [HttpGet, Route("GetRootDir")]
        public async Task<Dir> GetRootDir()
        {
            var query = _db.Where(item => item.MapPath == string.Empty);
            if (query.Count() > 0)
                return await query.Include(item => item.Dirs).Include(item => item.Files).FirstAsync();
            else return null;
        }

        [HttpGet, Route("GetDirTree")]
        public async Task<Dir> GetDirTree(Guid? id)
        {
            var dir = await _db.Where(item => item.Id == id).Include(item => item.FileType).Include(item => item.Dirs).Include(item => item.Files).FirstOrDefaultAsync();
            return dir;
        }
    }
}
