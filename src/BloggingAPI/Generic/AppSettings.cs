using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BloggingAPI.Generic
{
    public class AppSettings
    {
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

      
    }
}
