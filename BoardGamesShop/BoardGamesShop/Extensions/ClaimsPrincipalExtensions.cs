namespace System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    public static Guid? Id(this ClaimsPrincipal user)
    {
        if (Guid.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
        {
            return userId;
        }

        return null;
    }
}