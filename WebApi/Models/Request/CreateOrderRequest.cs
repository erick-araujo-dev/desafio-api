namespace SorteOnlineDesafio.WebApi.Models.Request
{
    public class CreateOrderRequest
    {
        public int ClientId { get; set; }
        public decimal TotalValue { get; set; }
    }
}
