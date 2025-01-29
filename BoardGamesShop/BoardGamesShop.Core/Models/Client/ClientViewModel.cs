using System.ComponentModel.DataAnnotations;

namespace BoardGamesShop.Core.Models.Client;

public class ClientViewModel
{
    public Guid Id { get; set; }

    [Display(Name = "Username")]
    public string UserName { get; set; } = null!;
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;
    
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    
    public string Email { get; set; } = null!;
}