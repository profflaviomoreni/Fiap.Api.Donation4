using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }


        [HttpGet]
        public ActionResult<IList<UsuarioModel>> GetAll()
        {
            var usuarios = _usuarioRepository.FindAll();
            return Ok(usuarios);
        }


        [HttpGet("{id}")]
        public ActionResult<UsuarioModel> GetById(int id)
        {
            var usuario = _usuarioRepository.FindById(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }


        [HttpPost]
        public ActionResult<UsuarioModel> Post([FromBody] UsuarioModel usuarioModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioId = _usuarioRepository.Insert(usuarioModel);
            usuarioModel.UsuarioId = usuarioId;

            return CreatedAtAction(nameof(GetById), new { id = usuarioId }, usuarioModel);
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UsuarioModel usuarioModel)
        {
            if (id != usuarioModel.UsuarioId)
                return BadRequest("ID da URL diferente do corpo da requisição.");

            _usuarioRepository.Update(usuarioModel);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.FindById(id);
            if (usuario == null)
                return NotFound();

            _usuarioRepository.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<dynamic> Login([FromBody] UsuarioModel usuarioModel )
        {
            var usuario = _usuarioRepository.FindByEmailAndSenha(usuarioModel.EmailUsuario, usuarioModel.Senha);

            if (usuario != null) {

                usuario.Senha = string.Empty;

                var tokenJTW = AutenticationService.GetToken(usuario);

                var retorno = new
                {
                    tokenJTW = tokenJTW,
                    usuarioId = usuario.UsuarioId,
                    usuarioEmail = usuario.EmailUsuario,
                    regra = usuario.Regra
                };

                return Ok(retorno);

            } else
            {
                return Unauthorized();
            }

        }

        
    }
}
