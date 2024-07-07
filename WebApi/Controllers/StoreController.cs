using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Application.Commom.Exceptions;
using SorteOnlineDesafio.Application.Interfaces;
using SorteOnlineDesafio.WebApi.Commom;
using SorteOnlineDesafio.WebApi.Models;
using SorteOnlineDesafio.WebApi.Models.Request;
using SorteOnlineDesafio.WebApi.Models.Response;

namespace SorteOnlineDesafio.WebApi.Controllers
{
    [Route("api/store")]
    [ApiController]
    //[Authorize]
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost("client/create")]
        public IActionResult CreateClient([FromBody] CreateClientRequest request)
        {
            
            try
            {
                var client = _storeService.CreateClient(request.Name, request.Email);
                return CreatedAtAction(nameof(CreateClient), new { id = client.ClientId }, client);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("order/create")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var order = _storeService.CreateOrder(request.ClientId, request.TotalValue);
                return CreatedAtAction(nameof(CreateOrder), new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("order/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                _storeService.DeleteOrder(orderId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("client-order/create")]
        public IActionResult CreateClientAndOrder([FromBody] CreateClientAndOrderRequest request)
        {
            try
            {
                var clientOrder = _storeService.CreateClientAndOrder(request.Name, request.Email, request.TotalValue);
                CreateClientAndOrderResponse response = new()
                {
                    ClientId = clientOrder.Client.ClientId,
                    OrderId = clientOrder.Order.OrderId,
                    ClientUri = $"api/store/client/{clientOrder.Client.ClientId}",
                    OrderUri = $"api/store/order/{clientOrder.Order.OrderId}"
                };

                return CreatedAtAction(nameof(CreateClientAndOrder), response);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpDelete("client-order/{clientId}")]
        public IActionResult DeleteClientAndAllTheirOrders(int clientId)
        {
            try
            {
                _storeService.DeleteClientAndAllTheirOrders(clientId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }
    }
}
