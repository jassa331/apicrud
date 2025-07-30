using Microsoft.EntityFrameworkCore;

namespace apicrud.Models
{
    public class ClientDbcontext:DbContext
    {
        public ClientDbcontext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<info> info { get; set; }
        public DbSet<jds> jds { get; set; }

    }


}
