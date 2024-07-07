namespace SorteOnlineDesafio.Application.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public string OrderDate { get; set; }
        public decimal TotalValue { get; set; }
    }
}
