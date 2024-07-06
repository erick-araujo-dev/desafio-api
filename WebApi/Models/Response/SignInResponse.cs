using SorteOnlineDesafio.Domain.Entities;

namespace SorteOnlineDesafio.WebApi.Models.Response
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
