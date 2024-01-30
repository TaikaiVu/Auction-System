using assignment1web.Models.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace assignment1web.Models
{
    public class ClientContext : IdentityDbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
        }
        public DbSet<Client> Client { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Item>().HasData(

                new Item
                {
                    ItemId = 1,
                    ProductName = "Nike Shoes",
                    MinimumBid = 25,
                    AuctionStartDate = DateTime.Now,
                    AuctionEndDate = DateTime.Now,
                    Category = "sport gadget",
                    Productdescription = "A soccer shoes worn twice only",
                    AssetCondition = "New",
                    ImageUrl = "/items/590450dd-7ba9-4097-b42b-66b7e17c3217_nikee2.jpg"

                },

               new Item
               {
                   ItemId = 2,
                   ProductName = "Adidas Shoes",
                   MinimumBid = 45,
                   AuctionStartDate = DateTime.Now,
                   AuctionEndDate = DateTime.Now,
                   Category = "sport gadget",
                   Productdescription = "A basketball shoes, never worn",
                   AssetCondition = "Brand New",
                   ImageUrl = "/items/15f0ad3b-da37-4e48-9fae-8437537d8473_addidas.jpg"


               }


            );


        }




    }
}
