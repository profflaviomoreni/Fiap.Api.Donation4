using AutoMapper;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Donation4.Services;
using Microsoft.AspNetCore.JsonPatch;

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

        // Método para validação do ModelState
        private ActionResult ValidateModelState()
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new { Errors = errorMessages });
            }
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IList<UsuarioResponseViewModel>>> Get()
        {
            var usuarios = await _usuarioRepository.FindAllAsync();

            if (usuarios != null && usuarios.Count > 0)
            {
                var usuariosResponse = _mapper.Map<IList<UsuarioResponseViewModel>>(usuarios);
                return Ok(usuariosResponse);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseViewModel>> Post([FromBody] UsuarioRequestViewModel usuarioRequest)
        {
            var validationError = ValidateModelState();
            if (validationError != null)
                return validationError;

            var usuarioModel = _mapper.Map<UsuarioModel>(usuarioRequest);
            var usuarioId = await _usuarioRepository.InsertAsync(usuarioModel);
            usuarioModel.UsuarioId = usuarioId;

            var usuarioResponse = _mapper.Map<UsuarioResponseViewModel>(usuarioModel);
            return CreatedAtAction(nameof(Get), new { id = usuarioId }, usuarioResponse);
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UsuarioResponseViewModel>> Patch([FromRoute] int id, [FromBody] JsonPatchDocument<UsuarioPatchViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var usuarioExistente = await _usuarioRepository.FindByIdAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }

            // Mapeia o modelo atual para o ViewModel de patch
            var usuarioPatchVM = _mapper.Map<UsuarioPatchViewModel>(usuarioExistente);

            // Aplica o patch
            patchDoc.ApplyTo(usuarioPatchVM);

            // Valida o ModelState após a aplicação do patch
            var validationError = ValidateModelState();
            if (validationError != null)
                return validationError;

            // Atualiza o modelo original com os dados do patch
            _mapper.Map(usuarioPatchVM, usuarioExistente);

            // Salva as alterações
            await _usuarioRepository.UpdateAsync(usuarioExistente);

            var usuarioResponse = _mapper.Map<UsuarioResponseViewModel>(usuarioExistente);
            return Ok(usuarioResponse);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioResponseViewModel>> Put([FromRoute] int id, [FromBody] UsuarioRequestViewModel usuarioRequest)
        {
            var validationError = ValidateModelState();
            if (validationError != null)
                return validationError;

            var usuarioExistente = await _usuarioRepository.FindByIdAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }

            var usuarioModel = _mapper.Map(usuarioRequest, usuarioExistente);
            usuarioModel.UsuarioId = id; // Assegura que o ID não seja alterado

            await _usuarioRepository.UpdateAsync(usuarioModel);

            var usuarioResponse = _mapper.Map<UsuarioResponseViewModel>(usuarioModel);
            return Ok(usuarioResponse);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginResponseViewModel>> Login([FromBody] LoginRequestViewModel loginRequestVM)
        {
            var validationError = ValidateModelState();
            if (validationError != null)
                return validationError;

            var usuarioModel = await _usuarioRepository.FindByEmailAndSenhaAsync(loginRequestVM.EmailUsuario, loginRequestVM.Senha);

            if (usuarioModel != null)
            {
                var tokenJWT = AutenticationService.GetToken(usuarioModel);

                var loginResponseVM = _mapper.Map<LoginResponseViewModel>(usuarioModel);
                loginResponseVM.Token = tokenJWT;

                return Ok(loginResponseVM);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
