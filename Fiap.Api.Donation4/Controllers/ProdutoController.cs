using AutoMapper;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Donation4.Controllers
{
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper1)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper1;
        }

        [ApiVersion("1.0",Deprecated = true)]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProdutoResponseViewModel>> Get()
        {
            var produtos = await _produtoRepository.FindAllAsync();
            var produtosVM = _mapper.Map<IList<ProdutoResponseViewModel>>(produtos);

            return Ok(produtosVM);
        }

        [ApiVersion("2.0")]
        [ApiVersion("3.0")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<ProdutoPaginacaoResponseViewModel>> Get([FromQuery] int pagina = 0, [FromQuery] int tamanho = 5)
        {
            var apiVersion = HttpContext.GetRequestedApiVersion()?.ToString();

            var totalGeral = await _produtoRepository.CountAsync();
            var produtos = await _produtoRepository.FindAllAsync(pagina, tamanho);

            var produtoVM = new ProdutoPaginacaoResponseViewModel();
            produtoVM.Produtos = _mapper.Map<IList<ProdutoResponseViewModel>>(produtos);
            produtoVM.TotalGeral = totalGeral;
            produtoVM.TotalPaginas = Convert.ToInt16(Math.Ceiling((double)(totalGeral / tamanho)));
            produtoVM.LinkProximo = (pagina < produtoVM.TotalPaginas - 1) ? $"/api/v{apiVersion}/produto?pagina={pagina + 1}&tamanho={tamanho}" : "";
            produtoVM.LinkAnterior = (pagina > 0) ? $"/api/v{apiVersion}/produto?pagina={pagina - 1}&tamanho={tamanho}" : "";

            return Ok(produtoVM);
        }

        //[HttpGet]
        //public async Task<ActionResult<dynamic>> Get([FromQuery] int produtoIdRef = 0, [FromQuery] int tamanho = 5)
        //{
        //    var apiVersion = HttpContext.GetRequestedApiVersion()?.ToString();
        //    var produtos = await produtoRepository.FindAllByIdRefAsync(produtoIdRef, tamanho);
        //    var ultimoProdutoId = produtos.LastOrDefault().ProdutoId;
        //    var retorno = new
        //    {
        //        produtoRefId = ultimoProdutoId,
        //        proximaPagina = $"/api/v{apiVersion}/produto?produtoIdRef={ultimoProdutoId}&tamanho={tamanho}",
        //        produtos
        //    };
        //    return Ok(retorno);
        //}




        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoResponseViewModel>> GetById(int id)
        {
            var produto = await _produtoRepository.FindByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            var produtoResponse = _mapper.Map<ProdutoResponseViewModel>(produto);
            return Ok(produtoResponse);
        }

        // Métodos Post e Patch - Acesso permitido para admin e operador
        [Authorize(Roles = "admin, operador")]
        [HttpPost]
        public async Task<ActionResult<ProdutoResponseViewModel>> Post([FromBody] ProdutoRequestViewModel produtoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var produtoModel = _mapper.Map<ProdutoModel>(produtoRequest);
            var produtoId = await _produtoRepository.InsertAsync(produtoModel);
            var produtoResponse = _mapper.Map<ProdutoResponseViewModel>(produtoModel);

            return CreatedAtAction(nameof(GetById), new { id = produtoId }, produtoResponse);
        }

        [Authorize(Roles = "admin, operador")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ProdutoPatchViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest("Invalid patch document.");
            }

            var produtoExistente = await _produtoRepository.FindByIdAsync(id);
            if (produtoExistente == null)
            {
                return NotFound();
            }

            var produtoPatchVM = _mapper.Map<ProdutoPatchViewModel>(produtoExistente);
            patchDoc.ApplyTo(produtoPatchVM, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(produtoPatchVM, produtoExistente);
            await _produtoRepository.UpdateAsync(produtoExistente);

            return NoContent();
        }

        // Métodos Put e Delete - Acesso permitido apenas para admin
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProdutoRequestViewModel produtoRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produtoRequest.ProdutoId)
            {
                return BadRequest();
            }

            var produtoExistente = await _produtoRepository.FindByIdAsync(id);
            if (produtoExistente == null)
            {
                return NotFound();
            }

            var produto = _mapper.Map<ProdutoModel>(produtoRequest);
            await _produtoRepository.UpdateAsync(produto);

            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _produtoRepository.FindByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            await _produtoRepository.DeleteAsync(id);
            return NoContent();
        }




    }
}
