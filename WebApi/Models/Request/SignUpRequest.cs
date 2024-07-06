using SorteOnlineDesafio.Application.Commom;
using System.ComponentModel.DataAnnotations;

namespace SorteOnlineDesafio.WebApi.Models.Request
{
    public class SignUpRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
