namespace VibeTrack.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(int userId, string email, string username, string role = "User");
    }
}
