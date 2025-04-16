using AutoMapper;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.Services;
using Fiap.Api.Donation4.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
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
        [AllowAnonymous]
        public ActionResult<LoginResponseVM> Login([FromBody] LoginRequestVM loginRequest )
        {
            if (ModelState.IsValid)
            {

                var usuarioModel = _usuarioRepository.FindByEmailAndSenha(loginRequest.EmailUsuario, loginRequest.Senha);

                if (usuarioModel != null)
                {

                    var loginResponse = _mapper.Map<LoginResponseVM>(usuarioModel);
                    loginResponse.Token = AutenticationService.GetToken(usuarioModel);

                    return Ok(loginResponse);

                }
                else
                {
                    return Unauthorized();
                }

            } else {

                var errors = ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(m => m.ErrorMessage);

                return BadRequest(errors);

            }

        }

        
    }
}
