namespace SorteOnlineDesafio.Application.Interfaces
{
    public interface IAuthService
    {
        bool VerifyPassword(string email, string plainPassword);
    }
}
