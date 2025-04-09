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
        public ActionResult<IList<CategoriaModel>> Get() {

            var lista = _categoriaRepository.FindAll();

            if (lista == null || lista.Count == 0) {
                return NoContent();
            } else
            {
                return Ok(lista);
            }
                
        }

        [HttpGet("{id:int}")]
        public ActionResult<CategoriaModel> Get([FromRoute] int id)
        {
            var categoriaModel = _categoriaRepository.FindById(id);

            if (categoriaModel != null)
            {
                return Ok(categoriaModel);
            } else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<CategoriaModel> Post([FromBody] CategoriaModel categoriaModel)
        {
            if (ModelState.IsValid == false)
            {
                var errors = ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(m => m.ErrorMessage);

                return BadRequest(errors);
            } else {

                categoriaModel.CategoriaId = _categoriaRepository.Insert(categoriaModel);

                return CreatedAtAction( nameof(Get) , new { id = categoriaModel.CategoriaId } , categoriaModel );
                
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult Put([FromRoute] int id, [FromBody] CategoriaModel categoriaModel)
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


            var categoria = _categoriaRepository.FindById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _categoriaRepository.Update(categoriaModel);
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if ( id == 0)
            {
                return BadRequest();
            }

            var categoria = _categoriaRepository.FindById(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _categoriaRepository.Delete(id);
            return NoContent();
        }

    }
}
