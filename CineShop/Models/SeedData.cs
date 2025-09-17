using CineShop.DataBase;
using Microsoft.EntityFrameworkCore;

namespace CineShop.Models
{
    public class SeedData
    {
       
        
        public static async Task InitializeAsync(MovieDbContext context)
        {
                if (context.Customers.Any() || context.Movies.Any()) return;

                var random = new Random();



               
                    var customers = new List<Customer>
                    {

                         new Customer { FirstName = "Amina", LastName = "Khan", BillingAddress = "12 Crescent Rd", BillingCity = "London", BillingZip = "NW10 3PT", DeliveryAddress = "12 Crescent Rd", DeliveryCity = "London", DeliveryZip = "NW10 3PT", EmailAddress = "amina.khan@example.com", PhoneNo = "+447700900001" },
                         new Customer { FirstName = "Carlos", LastName = "Ramírez", BillingAddress = "Av. Reforma 123", BillingCity = "Mexico City", BillingZip = "01000", DeliveryAddress = "Av. Reforma 123", DeliveryCity = "Mexico City", DeliveryZip = "01000", EmailAddress = "carlos.r@example.com", PhoneNo = "+525512345678" },
                         new Customer { FirstName = "Yuki", LastName = "Tanaka", BillingAddress = "1-2-3 Shibuya", BillingCity = "Tokyo", BillingZip = "150-0002", DeliveryAddress = "1-2-3 Shibuya", DeliveryCity = "Tokyo", DeliveryZip = "150-0002", EmailAddress = "yuki.t@example.com", PhoneNo = "+81312345678" },
                         new Customer { FirstName = "Léa", LastName = "Dubois", BillingAddress = "45 Rue Lafayette", BillingCity = "Paris", BillingZip = "75009", DeliveryAddress = "45 Rue Lafayette", DeliveryCity = "Paris", DeliveryZip = "75009", EmailAddress = "lea.d@example.com", PhoneNo = "+33123456789" },
                         new Customer { FirstName = "Mohammed", LastName = "Al-Farsi", BillingAddress = "Al Khuwair St", BillingCity = "Muscat", BillingZip = "112", DeliveryAddress = "Al Khuwair St", DeliveryCity = "Muscat", DeliveryZip = "112", EmailAddress = "mohammed.af@example.com", PhoneNo = "+96891234567" },
                         new Customer { FirstName = "Sofia", LastName = "Rossi", BillingAddress = "Via Roma 22", BillingCity = "Rome", BillingZip = "00185", DeliveryAddress = "Via Roma 22", DeliveryCity = "Rome", DeliveryZip = "00185", EmailAddress = "sofia.r@example.com", PhoneNo = "+390612345678" },
                         new Customer { FirstName = "Ethan", LastName = "Nguyen", BillingAddress = "123 Le Loi", BillingCity = "Ho Chi Minh City", BillingZip = "700000", DeliveryAddress = "123 Le Loi", DeliveryCity = "Ho Chi Minh City", DeliveryZip = "700000", EmailAddress = "ethan.n@example.com", PhoneNo = "+84812345678" },
                         new Customer { FirstName = "Anastasia", LastName = "Ivanova", BillingAddress = "Lenina St 10", BillingCity = "Moscow", BillingZip = "101000", DeliveryAddress = "Lenina St 10", DeliveryCity = "Moscow", DeliveryZip = "101000", EmailAddress = "anastasia.i@example.com", PhoneNo = "+74951234567" },
                         new Customer { FirstName = "David", LastName = "Smith", BillingAddress = "Main St 55", BillingCity = "New York", BillingZip = "10001", DeliveryAddress = "Main St 55", DeliveryCity = "New York", DeliveryZip = "10001", EmailAddress = "david.s@example.com", PhoneNo = "+12125551234" },
                         new Customer { FirstName = "Chen", LastName = "Wei", BillingAddress = "Zhongshan Rd 88", BillingCity = "Shanghai", BillingZip = "200000", DeliveryAddress = "Zhongshan Rd 88", DeliveryCity = "Shanghai", DeliveryZip = "200000", EmailAddress = "chen.wei@example.com", PhoneNo = "+862112345678" },
                         new Customer { FirstName = "Fatima", LastName = "El-Masri", BillingAddress = "Corniche Rd 5", BillingCity = "Beirut", BillingZip = "1103", DeliveryAddress = "Corniche Rd 5", DeliveryCity = "Beirut", DeliveryZip = "1103", EmailAddress = "fatima.em@example.com", PhoneNo = "+9611234567" },
                         new Customer { FirstName = "Jonas", LastName = "Bergström", BillingAddress = "Storgatan 9", BillingCity = "Stockholm", BillingZip = "11422", DeliveryAddress = "Storgatan 9", DeliveryCity = "Stockholm", DeliveryZip = "11422", EmailAddress = "jonas.b@example.com", PhoneNo = "+46701234567" },
                         new Customer { FirstName = "Isabella", LastName = "Martínez", BillingAddress = "Calle 8 #45", BillingCity = "Bogotá", BillingZip = "110111", DeliveryAddress = "Calle 8 #45", DeliveryCity = "Bogotá", DeliveryZip = "110111", EmailAddress = "isabella.m@example.com", PhoneNo = "+5712345678" },
                         new Customer { FirstName = "Mateo", LastName = "Silva", BillingAddress = "Rua das Flores 77", BillingCity = "São Paulo", BillingZip = "01001-000", DeliveryAddress = "Rua das Flores 77", DeliveryCity = "São Paulo", DeliveryZip = "01001-000", EmailAddress = "mateo.s@example.com", PhoneNo = "+5511999999999" },
                         new Customer { FirstName = "Nora", LastName = "Schmidt", BillingAddress = "Berliner Str. 12", BillingCity = "Berlin", BillingZip = "10115", DeliveryAddress = "Berliner Str. 12", DeliveryCity = "Berlin", DeliveryZip = "10115", EmailAddress = "nora.s@example.com", PhoneNo = "+493012345678" },
                         new Customer { FirstName = "Raj", LastName = "Patel", BillingAddress = "MG Road 101", BillingCity = "Mumbai", BillingZip = "400001", DeliveryAddress = "MG Road 101", DeliveryCity = "Mumbai", DeliveryZip = "400001", EmailAddress = "raj.p@example.com", PhoneNo = "+912212345678" },
                         new Customer { FirstName = "Anna", LastName = "Kowalska", BillingAddress = "Ul. Nowa 5", BillingCity = "Warsaw", BillingZip = "00-001", DeliveryAddress = "Ul. Nowa 5", DeliveryCity = "Warsaw", DeliveryZip = "00-001", EmailAddress = "anna.k@example.com", PhoneNo = "+48221234567" },
                         new Customer { FirstName = "Tom", LastName = "O'Connor", BillingAddress = "O'Connell St 33", BillingCity = "Dublin", BillingZip = "D01", DeliveryAddress = "O'Connell St 33", DeliveryCity = "Dublin", DeliveryZip = "D01", EmailAddress = "tom.oc@example.com", PhoneNo = "+35312345678" },
                         new Customer { FirstName = "Linda", LastName = "Johansen", BillingAddress = "Strandveien 10", BillingCity = "Oslo", BillingZip = "0250", DeliveryAddress = "Strandveien 10", DeliveryCity = "Oslo", DeliveryZip = "0250", EmailAddress = "linda.j@example.com", PhoneNo = "+4723123456" },
                         new Customer { FirstName = "Jin", LastName = "Park", BillingAddress = "Gangnam-daero 123", BillingCity = "Seoul", BillingZip = "06000", DeliveryAddress = "Gangnam-daero 123", DeliveryCity = "Seoul", DeliveryZip = "06000", EmailAddress = "jin.park@example.com", PhoneNo = "+82212345678" }








                    };


            var movies = new List<Movie>
            {
                new Movie { Title = "Inception", Genre = "Sci-Fi", Director = "Christopher Nolan", ReleaseYear = 2010, Price = 299.00m, Image = "/images/inception.jpg" },
                new Movie { Title = "Parasite", Genre = "Thriller", Director = "Bong Joon-ho", ReleaseYear = 2019, Price = 249.00m, Image = "/images/parasite.jpg" },
                new Movie { Title = "Amélie", Genre = "Romance", Director = "Jean-Pierre Jeunet", ReleaseYear = 2001, Price = 179.00m, Image = "/images/amelie.jpg" },
                new Movie { Title = "Spirited Away", Genre = "Fantasy", Director = "Hayao Miyazaki", ReleaseYear = 2001, Price = 229.00m, Image = "/images/spirited.jpg" },
                new Movie { Title = "Roma", Genre = "Drama", Director = "Alfonso Cuarón", ReleaseYear = 2018, Price = 199.00m, Image = "/images/roma.jpg" },
                new Movie { Title = "City of God", Genre = "Crime", Director = "Fernando Meirelles", ReleaseYear = 2002, Price = 189.00m, Image = "/images/guds stad.jpg" },
                new Movie { Title = "Pan's Labyrinth", Genre = "Fantasy", Director = "Guillermo del Toro", ReleaseYear = 2006, Price = 219.00m, Image = "/images/pan's lab.jpg" },
                new Movie { Title = "The Lives of Others", Genre = "Drama", Director = "Florian Henckel von Donnersmarck", ReleaseYear = 2006, Price = 209.00m, Image = "/images/love of others.jpg" },
                new Movie { Title = "Crouching Tiger, Hidden Dragon", Genre = "Action", Director = "Ang Lee", ReleaseYear = 2000, Price = 239.00m, Image = "/images/tiger.jpg" },
                new Movie { Title = "The Intouchables", Genre = "Comedy", Director = "Olivier Nakache & Éric Toledano", ReleaseYear = 2011, Price = 189.00m, Image = "/images/the intouchables.jpg" },
                new Movie { Title = "Life Is Beautiful", Genre = "Drama", Director = "Roberto Benigni", ReleaseYear = 1997, Price = 199.00m, Image = "/images/life is beautiful.jpg" },
                new Movie { Title = "Oldboy", Genre = "Thriller", Director = "Park Chan-wook", ReleaseYear = 2003, Price = 219.00m, Image = "/images/oldboy.jpg" },
                new Movie { Title = "The Secret in Their Eyes", Genre = "Mystery", Director = "Juan José Campanella", ReleaseYear = 2009, Price = 189.00m, Image = "/images/the secret.jpg" },
                new Movie { Title = "Your Name", Genre = "Animation", Director = "Makoto Shinkai", ReleaseYear = 2016, Price = 229.00m, Image = "/images/your name.jpg" },
                new Movie { Title = "The Hunt", Genre = "Drama", Director = "Thomas Vinterberg", ReleaseYear = 2012, Price = 199.00m, Image = "/images/the hunt.jpg" },
                new Movie { Title = "Incendies", Genre = "Mystery", Director = "Denis Villeneuve", ReleaseYear = 2010, Price = 209.00m, Image = "/images/incendies.jpg" },
                new Movie { Title = "The Great Beauty", Genre = "Drama", Director = "Paolo Sorrentino", ReleaseYear = 2013, Price = 189.00m, Image = "/images/the great beauty.jpg" },
                new Movie { Title = "Train to Busan", Genre = "Horror", Director = "Yeon Sang-ho", ReleaseYear = 2016, Price = 219.00m, Image = "/images/train.jpg" },
                new Movie { Title = "Wadjda", Genre = "Drama", Director = "Haifaa al-Mansour", ReleaseYear = 2012, Price = 179.00m, Image = "/images/wadjda.jpg" },
                new Movie { Title = "The Salesman", Genre = "Drama", Director = "Asghar Farhadi", ReleaseYear = 2016, Price = 199.00m, Image = "/images/the salesman.jpg" }
            };






            await context.Customers.AddRangeAsync(customers);
                await context.Movies.AddRangeAsync(movies);
                await context.SaveChangesAsync();

              
                var orderRows = new List<OrderRow>();

                foreach (var customer in customers)
                {
                    int orderCount = random.Next(1, 4);
                    for (int i = 0; i < orderCount; i++)
                    {
                        var order = new Order
                        {
                            CustomerId = customer.Id,
                            OrderDate = DateTime.Now.AddDays(-random.Next(1, 365))
                        };

                        context.Orders.Add(order);
                        await context.SaveChangesAsync();

                        int rowCount = random.Next(1, 6);
                        for (int j = 0; j < rowCount; j++)
                        {
                            var movie = movies[random.Next(movies.Count)];
                            orderRows.Add(new OrderRow
                            {
                                OrderId = order.Id,
                                MovieId = movie.Id,
                                Price = movie.Price
                            });
                        }
                    }
                }

                await context.OrderRows.AddRangeAsync(orderRows);
                await context.SaveChangesAsync();
        }
    }


    

}
