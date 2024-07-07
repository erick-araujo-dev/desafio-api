using SorteOnlineDesafio.Application.Models;
using SorteOnlineDesafio.Domain.Entities;

namespace SorteOnlineDesafio.Application.Interfaces
{
    public interface IUserService
    {
        UserModel CreateUser(string name, string email, string password);
    }
}
