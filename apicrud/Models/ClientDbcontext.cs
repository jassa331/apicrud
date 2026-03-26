using Microsoft.EntityFrameworkCore;

namespace apicrud.Models
{
    public class ClientDbcontext:DbContext
    {
        public ClientDbcontext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<info> infoo { get; set; }
        public DbSet<jds> jds { get; set; }
        public DbSet<KDS> KDS { get; set; }

        public DbSet<login> Userss { get; set; }
        public DbSet<BussinessProducts> BussinessProducts { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<login>().ToTable("Users");
        //}



    }


}
