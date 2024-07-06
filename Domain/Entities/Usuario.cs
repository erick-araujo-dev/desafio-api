namespace SorteOnlineDesafio.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        private string _senha;
        public string Senha
        {
            get => _senha;
            set => _senha = BCrypt.Net.BCrypt.HashPassword(value);
        }
        public DateTime DataCriacao { get; set; }
    }
}
