namespace SorteOnlineDesafio.WebApi.Models
{
    public class CreateClientAndOrderRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal TotalValue { get; set; }
    }
}
