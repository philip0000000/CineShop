using CineShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CineShop.DataBase
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {

        }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CineShop.Models.Movie> Movies { get; set; } = default!;
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public IEnumerable<object> Movie { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Alice",
                    LastName = "Andersson",
                    BillingAddress = "Storgatan 12",
                    BillingCity = "Stockholm",
                    BillingZip = "11122",
                    DeliveryAddress = "Storgatan 12",
                    DeliveryCity = "Stockholm",
                    DeliveryZip = "11122",
                    EmailAddress = "alice@example.com",
                    PhoneNo = "+46701234567",


                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Björn",
                    LastName = "Bergström",
                    BillingAddress = "Kungsgatan 45",
                    BillingCity = "Göteborg",
                    BillingZip = "41115",
                    DeliveryAddress = "Kungsgatan 45",
                    DeliveryCity = "Göteborg",
                    DeliveryZip = "41115",
                    EmailAddress = "bjorn@example.com",
                    PhoneNo = "+46709876543"

                },
                new Customer
                {
                    Id = 3,
                    FirstName = "Clara",
                    LastName = "Carlsson",
                    BillingAddress = "Lundavägen 8",
                    BillingCity = "Malmö",
                    BillingZip = "20520",
                    DeliveryAddress = "Lundavägen 8",
                    DeliveryCity = "Malmö",
                    DeliveryZip = "20520",
                    EmailAddress = "clara@example.com",
                    PhoneNo = "+46705551234",


                }
            );

            builder.Entity<Movie>().HasData(
                   new Movie
                   {
                       Id = 1,
                       Title = "The Northern Light",
                       Genre = "Drama",
                       Director = "Eva Lindström",
                       ReleaseYear = 2022,
                       Price = 129.00m,
                       Image = "https://example.com/images/northern-light.jpg"
                   },
                    new Movie
                    {
                        Id = 2,
                        Title = "Midnight Fjord",
                        Genre = "Drama",
                        Director = "Oskar Berg",
                        ReleaseYear = 2023,
                        Price = 149.50m,
                        Image = "https://example.com/images/midnight-fjord.jpg"
                    },

                    new Movie
                    {
                        Id = 3,
                        Title = "Echoes of Lapland",
                        Genre = "Drama",
                        Director = "Karin Nyström",
                        ReleaseYear = 2021,
                        Price = 99.99m,
                        Image = "https://example.com/images/echoes-lapland.jpg"
                    }
            );


        }
        public DbSet<CineShop.Models.User> User { get; set; } = default!;
    }

}
