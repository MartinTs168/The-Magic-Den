using System.ComponentModel.DataAnnotations;

namespace BoardGamesShop.Core.Models.Statistic;

public class StatisticViewModel
{
    [Display(Name = "Count Clients")]
    public int CountClients { get; set; }
    
    [Display(Name = "Count Products")]
    public int CountProducts { get; set; }
}