namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static Guid Id(this ClaimsPrincipal user)
    {
        var id = Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        
        return id;
    }
}