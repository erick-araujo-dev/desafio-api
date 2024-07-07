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

        public StoreService(IClienteRepository clienteRepository, IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
        {
            _clienteRepository = clienteRepository;
            _pedidoRepository = pedidoRepository;
            _unitOfWork = unitOfWork;
        }

        #region Client

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

        #endregion

        #region Order

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

        public void DeleteOrder(int orderId)
        {
            var order = _pedidoRepository.Find(p => p.PedidoId == orderId).FirstOrDefault() ?? throw new BusinessException($"Pedido não encontrado.");

            _pedidoRepository.Delete(order);
        }

        #endregion

        #region ClientOrder

        public ClientOrderModel CreateClientAndOrder (string name, string email, decimal totalValue)
        {
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

                _unitOfWork.CommitTransaction();

                return modelReturn;

            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw; 
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public void DeleteClientAndAllTheirOrders(int clientId)
        {
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

                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
            finally
            {
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
