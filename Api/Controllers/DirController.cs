using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace Api.Controller
{
    [Route("api/[controller]")]
    public class DirController : BaseController<Dir>
    {

        public DirController(ILogger<DirController> logger, FileServConfig config, FileDbContext dbContext) : base(logger, config, dbContext)
        {
        }

        protected override IQueryable<Dir> GetQuery(Dir input)
        {
            return _db
            .Include(item => item.Dirs)
            .Include(item => item.Files)
            .WhereIf(input.MapPath, item => item.MapPath == input.MapPath)
            .WhereIf(input.Id.HasValue, item => item.Id == input.Id)
            ;
        }

        [HttpPost]
        public async override Task<Dir> AddAsync([FromBody] Dir input)
        {
            await input.InitAsync();
            if (Directory.Exists(input.MapPath)) return null;

            await base.AddAsync(input);

            Directory.CreateDirectory(_config.FileStore + _config.FullPath + input.MapPath);
            return input;
        }

        [HttpGet, Route("GetRootDir")]
        public async Task<Dir> GetRootDir()
        {
            var query = _db.Where(item => item.MapPath == string.Empty);
            if (query.Any())
                return await query.Include(item => item.Dirs).Include(item => item.Files).FirstAsync();
            else return null;
        }

        [HttpGet, Route("GetDirTree")]
        public async Task<Dir> GetDirTree(Guid? id)
        {
            var dir = await _db.Where(item => item.Id == id).Include(item => item.FileType).Include(item => item.Dirs).Include(item => item.Files).FirstOrDefaultAsync();
            return dir;
        }


        public override async Task<Dir> FindAsync(Guid? id)
        {
            return await GetQuery(new Dir { Id = id }).FirstOrDefaultAsync();
        }
    }
}
