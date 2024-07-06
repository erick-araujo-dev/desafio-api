using Microsoft.AspNetCore.Mvc;
using SorteOnlineDesafio.Application.Commom.Exceptions;
using System.Net;

namespace SorteOnlineDesafio.WebApi.Commom
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleException(Exception ex)
        {
            if (ex is BusinessException)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            } 
            else if(ex is NotFoundException)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            else
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Erro não previsto.");
            }
        }
    }
}
