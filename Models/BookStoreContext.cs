using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Project1.Models
{
    public class BookStoreContext:IdentityDbContext
    {
        public BookStoreContext()
        {
            
        }
        public BookStoreContext(DbContextOptions<BookStoreContext> option) : base(option) 
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Order_Items>()
                .HasKey("Order_id", "Book_id");
            builder.Entity<Catalog>().HasData(
                new Catalog() { Id=1,Name = "sport",Description="sporrrrrt"});
            //builder.Entity<IdentityRole>().HasData(
            //    new IdentityRole() {Id = "1", Name = "customer", NormalizedName = "CUSTOMER" },
            //    new IdentityRole() {Id="2", Name = "admin", NormalizedName = "ADMIN" }
            //    );
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Catalog Catalog { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_Items> Order_Items { get; set; }
        public virtual DbSet<Book> Books { get; set; }

    }
}
