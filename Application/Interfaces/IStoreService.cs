using SorteOnlineDesafio.Application.Models;

namespace SorteOnlineDesafio.Application.Interfaces
{
    public interface IStoreService
    {
        ClientOrderModel CreateClientAndOrder(string name, string email, decimal totalValue);
        ClientModel CreateClient(string name, string email);
        OrderModel CreateOrder(int clientId, decimal totalValue);
        void DeleteClientAndAllTheirOrders(int clientId);
        void DeleteOrder(int orderId);
    }
}
