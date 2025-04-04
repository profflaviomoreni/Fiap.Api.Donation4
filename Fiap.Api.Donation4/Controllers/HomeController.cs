using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {


        [HttpGet]
        public string Consultar()
        {
            return "GET";
        }

        [HttpPost]
        public string Cadastrar()
        {
            return "POST";
        }

        [HttpPut]
        public string Alterar()
        {
            return "PUT";
        }

        [HttpDelete]
        public string Remover()
        {
            return "DELETE";
        }

    }
}
