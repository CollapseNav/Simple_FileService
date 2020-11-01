using Microsoft.EntityFrameworkCore;

namespace Api.Model
{
    public class FileDbContext : DbContext
    {
        public DbSet<File> Files { get; set; }
        public FileDbContext(DbContextOptions<FileDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}