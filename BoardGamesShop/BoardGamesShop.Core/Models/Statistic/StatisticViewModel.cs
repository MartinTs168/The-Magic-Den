using System.ComponentModel.DataAnnotations;

namespace BoardGamesShop.Core.Models.Statistic;

public class StatisticViewModel
{
    [Display(Name = "Count Clients")]
    public int CountClients { get; set; }
    
    [Display(Name = "Count Products")]
    public int CountGames { get; set; }
    
    [Display(Name = "Count Orders")]
    public int CountOrders { get; set; }

    [Display(Name = "Total Sum Orders")] 
    public string TotalSumOrders { get; set; } = null!;
}