using System;
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
    public class FileTypeController : ControllerBase
    {
        private readonly FileServConfig _config;
        private readonly FileDbContext _context;
        private readonly ILogger _log;
        public FileTypeController(ILogger<FileController> logger, FileServConfig config, FileDbContext dbContext)
        {
            _log = logger;
            _config = config;
            _context = dbContext;
        }
    }
}
