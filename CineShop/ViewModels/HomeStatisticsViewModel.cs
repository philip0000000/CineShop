using System.Collections.Generic;
using CineShop.Models;

namespace CineShop.ViewModels
{
    public class HomeStatisticsViewModel
    {
        // Summary metrics
        public int TotalMovies { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalOrders { get; set; }
       

        // Highlighted movie sets
        public IEnumerable<Movie> MostPopularMovies { get; set; }
        public IEnumerable<Movie> NewestMovies { get; set; }
        public IEnumerable<Movie> OldestMovies { get; set; }


        public IEnumerable<Movie> CheapestMovies { get; set; }

        // Customer insights
        public Customer TopSpendingCustomer { get; set; }
    }
}
