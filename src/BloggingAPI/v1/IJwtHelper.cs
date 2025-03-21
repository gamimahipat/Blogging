namespace BloggingAPI.v1
{
    public interface IJwtHelper
    {
        string GenerateJwtToken(int userId, string userName, string email, string mobile, List<string> roleNames, List<string> permissions);
    }
}
