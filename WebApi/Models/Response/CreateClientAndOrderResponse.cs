namespace SorteOnlineDesafio.WebApi.Models.Response
{
    public class CreateClientAndOrderResponse
    {
        public int ClientId { get; set; }
        public int OrderId { get; set; }
        public string ClientUri { get; set; }
        public string OrderUri { get; set; }
    }
    
}
