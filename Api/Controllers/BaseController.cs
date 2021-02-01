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
    public class BaseController<T> : ControllerBase where T : BaseEntity
    {
        protected readonly FileServConfig _config;
        protected readonly FileDbContext _context;
        protected readonly ILogger _log;
        protected readonly DbSet<T> _db;
        public BaseController(ILogger<FileController> logger, FileServConfig config, FileDbContext dbContext)
        {
            _log = logger;
            _config = config;
            _context = dbContext;
            _db = _context.Set<T>();
        }

        protected async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        protected virtual IQueryable<T> GetQuery(T input)
        {
            return _db
                .WhereIf(input.Id.HasValue, item => item.Id == input.Id)
                .WhereIf(input.AddTime.HasValue, item => item.AddTime.HasValue || (item.AddTime.Value.Date == input.AddTime.Value.Date));
        }


        [HttpPost]
        public virtual async Task<T> AddAsync(T entity)
        {
            await _db.AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }
        [HttpGet, Route("{id}")]
        public virtual async Task<T> FindAsync(object id)
        {
            return await _db.FindAsync(id);
        }
        [HttpDelete, Route("{id}")]
        public virtual async Task DeleteAsync(object id)
        {
            _db.Remove(await FindAsync(id));
            await SaveChangesAsync();
        }
    }
}
