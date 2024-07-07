using SorteOnlineDesafio.Application.Commom.Exceptions;
using SorteOnlineDesafio.Application.Commom;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.Domain.Entities;
using SorteOnlineDesafio.Domain.Interfaces.Repository;
using SorteOnlineDesafio.Infra.Repositories;
using SorteOnlineDesafio.Domain.Interfaces.Commom;
using SorteOnlineDesafio.Application.Models;

namespace SorteOnlineDesafio.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _unitOfWork;

        #region Constructor
        public StoreService(IClienteRepository clienteRepository, IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Client

        // Criar um cliente
        public ClientModel CreateClient(string name, string email)
        {
            ValidateEmail(email);

            Cliente client = new Cliente 
            {
                Nome = name,
                Email = email,
            };

            var clientEntitie = _clienteRepository.AddAndReturnEntity(client);

            ClientModel clientModel = new()
            {
                ClientId = clientEntitie.ClienteId,
                Name = clientEntitie.Nome,
                Email = clientEntitie.Email
            };

            return clientModel;
        }


        // Obter um cliente pelo ClientId, com a opção de incluir os pedidos do cliente
        public ClientModel GetClientById(int clientId, bool includeOrders)
        {
            var client = _clienteRepository.Find(c => c.ClienteId == clientId).FirstOrDefault() ?? throw new NotFoundException("Cliente não encontrado."); ;

            if (!includeOrders)
                return new ClientModel
                {
                    ClientId = client.ClienteId,
                    Name = client.Nome,
                    Email = client.Email
                };

            // Incluir pedidos
            var orders = _pedidoRepository.Find(p => p.ClienteId == clientId).Select(o => new OrderModel
            {
                OrderId = o.PedidoId,
                ClientId = o.ClienteId,
                OrderDate = o.DataPedido.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalValue = o.ValorTotal
            }).ToList();

            var clientWithOrders = new ClientModel
            {
                ClientId = client.ClienteId,
                Name = client.Nome,
                Email = client.Email,
                Order = orders
            };

            return clientWithOrders;
        }


        // Obter todos os clientes
        public IList<ClientModel> GetAllClient()
        {
            var clients = _clienteRepository.All().Select(c => new ClientModel
            {
                ClientId = c.ClienteId,
                Name = c.Nome,
                Email = c.Email
            }).ToList();

            return clients;
        }

        #endregion

        #region Order

        //Criar um pedido
        public OrderModel CreateOrder(int clientId, decimal totalValue)
        {
            

            var client = _clienteRepository.Find(c => c.ClienteId == clientId).FirstOrDefault() ?? throw new BusinessException("Para inserir um pedido é preciso informar um cliente.");

            if (totalValue <= 0)
            {
                throw new BusinessException("Insira um valor maior que zero para cadastrar um pedido.");
            }

            Pedido order = new()
            {
                ClienteId = client.ClienteId,
                Cliente = client,
                ValorTotal = totalValue,
                DataPedido = DateTime.Now
            };

            var orderEntite = _pedidoRepository.AddAndReturnEntity(order);

            OrderModel orderModel = new()
            {
                OrderId = orderEntite.PedidoId,
                ClientId = orderEntite.ClienteId,
                OrderDate= orderEntite.DataPedido.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalValue = orderEntite.ValorTotal
            };

            return orderModel;
        }


        // Obter um pedido pelo PedidoId
        public OrderModel GetOrderById(int orderId)
        {
            var order = _pedidoRepository.Find(p => p.PedidoId == orderId).FirstOrDefault() ?? throw new NotFoundException("Pedido não encontrado.");

            var orderResponse = new OrderModel
            {
                OrderId = order.PedidoId,
                ClientId = order.ClienteId,
                OrderDate = order.DataPedido.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalValue = order.ValorTotal
            };

            return orderResponse;
        }
        

        //Obter todos os pedidos
        public IList<OrderModel> GetAllOrder()
        {
            var orders = _pedidoRepository.All().Select(o => new OrderModel
            {
                OrderId = o.PedidoId,
                ClientId = o.ClienteId,
                OrderDate = o.DataPedido.ToString("yyyy-MM-dd HH:mm:ss"),
                TotalValue = o.ValorTotal
            }).ToList();

            return orders;
        }


        //Excluir um pedido pelo PedidoId
        public void DeleteOrder(int orderId)
        {
            var order = _pedidoRepository.Find(p => p.PedidoId == orderId).FirstOrDefault() ?? throw new BusinessException($"Pedido não encontrado.");

            _pedidoRepository.Delete(order);
        }

        #endregion

        #region ClientOrder

        //Cria um cliente juntamente com um pedido para o mesmo.
        public ClientOrderModel CreateClientAndOrder (string name, string email, decimal totalValue)
        {
            //Abre a transacao
            _unitOfWork.BeginTransaction();
            
            try
            {
                var client = CreateClient(name, email);

                var order = CreateOrder(client.ClientId, totalValue);

                ClientOrderModel modelReturn = new()
                {
                    Client = client,
                    Order = order
                };

                //Comita a transacao
                _unitOfWork.CommitTransaction();

                return modelReturn;

            }
            catch
            {
                //Faz rollback e repassa a ex gerada
                _unitOfWork.RollbackTransaction();
                throw; 
            }
            finally
            {
                //Certifica que a transacao se encerre
                _unitOfWork.Dispose();
            }
        }


        //Excluir um cliente juntamente com todos os pedidos do mesmo
        public void DeleteClientAndAllTheirOrders(int clientId)
        {
            //Abre a transacao
            _unitOfWork.BeginTransaction();

            try
            {
                var client = _clienteRepository.Find(c => c.ClienteId == clientId).FirstOrDefault() ?? throw new NotFoundException($"Cliente não encontrado.");

                var orders = _pedidoRepository.Find(p => p.ClienteId == clientId).ToList();

                foreach (var order in orders)
                {
                    _pedidoRepository.Delete(order);
                }

                _clienteRepository.Delete(client);

                //Comita a transacao
                _unitOfWork.CommitTransaction();
            }
            catch
            {
                //Faz rollback e repassa a ex gerada
                _unitOfWork.RollbackTransaction();
                throw;
            }
            finally
            {
                //Certifica que a transacao se encerre
                _unitOfWork.Dispose();
            }           
        }

        #endregion

        #region Private Methods
        private void ValidateEmail(string email)
        {
            var emailValid = Util.ValidateEmail(email);

            if (!emailValid)
            {
                throw new BusinessException("Formato de e-mail inválido.");
            }

            var emailAlreadyUsed = _clienteRepository.Find(u => u.Email == email).FirstOrDefault();

            if (emailAlreadyUsed != null)
            {
                throw new BusinessException("E-mail já cadastrado.");
            }
        }

        #endregion
    }
}
