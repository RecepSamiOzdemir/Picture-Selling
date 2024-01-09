using Microsoft.EntityFrameworkCore;
using PictureSellingWeb.Models;

namespace PictureSellingWeb.Services
{
    public class PictureContext:DbContext
    {

        public DbSet<Picture> Pictures{ get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Stock> Stock{ get; set; }
        public DbSet<Customer> Customer{ get; set; }
        public DbSet<Artist> Artists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseMySQL("server=localhost;database=PictureSeller;user=root;password=sasa");
           //optionsBuilder.UseSqlServer("Server=DESKTOP-EBG03G2;Database=PictureSelling;Trusted_Connection=True;Encrypt=False");
           base.OnConfiguring(optionsBuilder);
        }

    }
}
