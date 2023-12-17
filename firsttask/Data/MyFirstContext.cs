using firsttask.Models;
using Microsoft.EntityFrameworkCore;

namespace firsttask.Data
{
    public class MyFirstContext : DbContext
    {
        public MyFirstContext(DbContextOptions<MyFirstContext> options) : base(options)
        {
        }
        public DbSet<firsttask.Models.Category> Categories { get; set; }
        public DbSet<firsttask.Models.Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var lebas = new Category() { CategoryId = 1, CategoryName = "لباس" };
            var shalvar = new Category() { CategoryId = 2, CategoryName = "شلوار" };
            modelBuilder.Entity<Models.Category>().HasData(lebas, shalvar);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                ProductId = 1,
                ProductName = "پیراهن مردانه",
                ProductPrice = 5000,
                Description = "seed data1",
                CategoryId = lebas.CategoryId
            });
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                ProductId = 2,
                ProductName = "لی",
                ProductPrice = 1000,
                Description = "seed data2",
                CategoryId = shalvar.CategoryId
            });
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                ProductId = 3,
                ProductName = "پیراهن زنانه",
                ProductPrice = 5000,
                Description = "seed data1",
                CategoryId = lebas.CategoryId
            });
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                ProductId = 4,
                ProductName = "کتان",
                ProductPrice = 1000,
                Description = "seed data2",
                CategoryId = shalvar.CategoryId
            });
            modelBuilder.Entity<Models.Product>().HasData(new Models.Product()
            {
                ProductId = 5,
                ProductName = "هودی",
                ProductPrice = 5000,
                Description = "seed data1",
                CategoryId = lebas.CategoryId,
            });
        }
    }
}

