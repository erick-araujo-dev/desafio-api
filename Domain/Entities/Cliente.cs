namespace SorteOnlineDesafio.Domain.Entities
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public ICollection<Pedido> Pedidos { get; set; }
    }
}
