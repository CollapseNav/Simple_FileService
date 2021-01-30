using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Controller
{
    [Route("api/[controller]")]
    public class FileTypeController : BaseController<FileType>
    {
        public FileTypeController(ILogger<FileController> logger, FileServConfig config, FileDbContext dbContext) : base(logger, config, dbContext)
        {
        }

        [HttpPost, Route("[action]")]
        public async Task AddFileType([FromBody] FileType input)
        {
            await AddAsync(input);
        }
    }
}
