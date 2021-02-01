using Api.Common;
using Api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controller
{
    [Route("api/[controller]")]
    public class FileTypeController : BaseController<FileType>
    {
        public FileTypeController(ILogger<FileController> logger, FileServConfig config, FileDbContext dbContext) : base(logger, config, dbContext)
        {
        }
    }
}
