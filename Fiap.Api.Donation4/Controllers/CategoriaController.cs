using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;            
        }


        [HttpGet]
        public async Task<ActionResult<IList<CategoriaModel>>> Get() {

            var lista = await _categoriaRepository.FindAllAsync();

            if (lista == null || lista.Count == 0) {
                return NoContent();
            } else
            {
                return Ok(lista);
            }
                
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaModel>> Get([FromRoute] int id)
        {
            var categoriaModel = await _categoriaRepository.FindByIdAsync(id);

            if (categoriaModel != null)
            {
                return Ok(categoriaModel);
            } else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaModel>> Post([FromBody] CategoriaModel categoriaModel)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(m => m.ErrorMessage);

                return BadRequest(errors);

            } else {

                categoriaModel.CategoriaId = await _categoriaRepository.InsertAsync(categoriaModel);

                return CreatedAtAction( nameof(Get) , new { id = categoriaModel.CategoriaId } , categoriaModel );
                
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromRoute] int id, [FromBody] CategoriaModel categoriaModel)
        {

            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select( m=> m.ErrorMessage);

                return BadRequest(errors);
            }

            if (id != categoriaModel.CategoriaId) { 
                return BadRequest( new { erro = "IDs divergentes" });
            }


            var categoria = await _categoriaRepository.FindByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _categoriaRepository.UpdateAsync(categoriaModel);
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            if ( id == 0)
            {
                return BadRequest();
            }

            var categoria = await _categoriaRepository.FindByIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _categoriaRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
