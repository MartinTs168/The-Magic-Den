using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;

namespace BoardGamesShop.Infrastructure.Data.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    [PersonalData]
    [MaxLength(FirstNameMaxLength)]
    public string? FirstName { get; set; }
    
    [PersonalData]
    [MaxLength(LastNameMaxLength)]
    public string? LastName { get; set; }
    
    [PersonalData]
    public string? Address { get; set; }

    [Comment("Used to track if the user is deleted")]
    public bool IsDeleted { get; set; } = false;

}