using System.ComponentModel.DataAnnotations;
using static BoardGamesShop.Infrastructure.Constants.DataConstants;
using static BoardGamesShop.Core.Constants.MessageConstants;

namespace BoardGamesShop.Core.Models.Cart;

public class CheckoutViewModel
{
    [Required]
    [StringLength(AddressMaxLength, MinimumLength = AddressMinLength,
        ErrorMessage = LengthErrorMessage)]
    public string Address { get; set; } = null!;
}