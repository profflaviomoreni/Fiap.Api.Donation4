using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AcessoController : ControllerBase
    {

        [HttpGet]
        [Route("Anonimo")]
        [AllowAnonymous]
        public string Anonimo()
        {
            return "Anonimo";
        }


        [HttpGet]
        [Route("Autenticado")]
        public string Autenticado()
        {
            return "Autenticado";
        }

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Admin()
        {
            return "Admin";
        }

        [HttpGet]
        [Route("Operador")]
        [Authorize(Roles = "admin, operador")]
        public string Operador()
        {
            return "Operador";
        }

        [HttpGet]
        [Route("revisor")]
        [Authorize(Roles = "admin, operador, revisor")]
        public string Revisor()
        {
            return "Revisor";
        }

    }
}
