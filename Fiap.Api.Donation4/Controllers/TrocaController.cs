using AutoMapper;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.Services;
using Fiap.Api.Donation4.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fiap.Api.Donation4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrocaController : ControllerBase
    {

        private readonly IMapper _mapper;

        private readonly ITrocaService _trocaService;

        private readonly ITrocaRepository _trocaRepository;


        public TrocaController(ITrocaService trocaService, IMapper mapper, ITrocaRepository trocaRepository)
        {
            _mapper = mapper;
            _trocaService = trocaService;
            _trocaRepository = trocaRepository;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<TrocaResponseViewModel>> Get(Guid id)
        {
            var trocaModel = await _trocaRepository.FindById(id);

            var vm = _mapper.Map<TrocaResponseViewModel>(trocaModel);

            return Ok(vm);

        }




            [HttpPost]
        public async Task<ActionResult<Guid>> Post([FromBody] TrocaRequestViewModel trocaRequestViewModel)
        {
            try
            {
                var trocaModel = _mapper.Map<TrocaModel>(trocaRequestViewModel);
                trocaModel.UsuarioId = GetUsuarioId();

                var idTroca = await _trocaService.Trocar(trocaModel);

                return Ok(idTroca);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        


        
        private int GetUsuarioId() {

            int userId = 0;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst("UsuarioId");
                if (userIdClaim != null && userIdClaim.Value != null ) { 
                    userId = Int16.Parse(userIdClaim.Value);
                }
            }

            return userId; 
        }
        

    }
}
