namespace Middleware;

public interface IJwtBuilder
{
    string GetToken(string userId ,string UserName, string UserEmail);
    string ValidateToken(string token);
}