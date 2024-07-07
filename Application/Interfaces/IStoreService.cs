using SorteOnlineDesafio.Application.Models;

namespace SorteOnlineDesafio.Application.Interfaces
{
    public interface IStoreService
    {
        ClientModel CreateClient(string name, string email);
        ClientModel GetClientById(int clientId, bool includeOrders);
        IList<ClientModel> GetAllClient();
        OrderModel CreateOrder(int clientId, decimal totalValue);
        OrderModel GetOrderById(int orderId);
        IList<OrderModel> GetAllOrder();
        void DeleteOrder(int orderId);
        ClientOrderModel CreateClientAndOrder(string name, string email, decimal totalValue);
        void DeleteClientAndAllTheirOrders(int clientId);
    }
}
