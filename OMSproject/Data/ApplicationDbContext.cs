using Microsoft.EntityFrameworkCore;
using OMSproject.DTO;
using OMSproject.Models;
using OMSproject.Models.ViewModels;


namespace OMSproject.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Color>()
                .HasKey(x => new { x.Product_Id,x.ColorName});

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithMany(p => p.Products)
                .UsingEntity<OrderDetails>(

                j => j
                    .HasOne(od => od.Orders)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.OrderId),

                j => j
                    .HasOne(od => od.Products)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId),
                j =>
                {
                    j.HasKey(t => new { t.OrderId , t.ProductId , t.ClrName});
                });

        }


        public DbSet<Client>? Clients { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Color>? Colors { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderDetails>? Details { get; set; } 
        //public DbSet<OMSproject.Models.ViewModels.OrderClientViewModel>? OrderClientViewModel { get; set; }
        //public DbSet<OMSproject.Models.ViewModel.OrderViewModel> OrderViewModel { get; set; }
        //public DbSet<OMSproject.Models.ViewModel.ProductColorViewModel> ProductColorViewModel { get; set; }


    }
        
}