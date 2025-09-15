using System.Collections.Generic;
using System.ComponentModel;
using CineShop.Models;

namespace CineShop.Services
{
    public interface IStatisticsService
    {
        IEnumerable<Movie> GetFiveMostPopularMovies(int topN);//Based on the number of orders 
        IEnumerable<Movie> GetFiveNewestMovies(int topN);//Based on realease year
        IEnumerable<Movie> GetFiveOldestMovies(int topN);//Based on realease year
        IEnumerable<Movie> GetFiveCheapestMovies(int topN);//Based on Price

        Customer GetTopCustomerByTotalSpend(); //Order sum for all orders for each customer is compared
    }
}
