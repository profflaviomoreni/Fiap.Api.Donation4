using AutoMapper;
using Fiap.Api.Donation4.Models;
using Fiap.Api.Donation4.Repository.Interfaces;
using Fiap.Api.Donation4.ViewModel;
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
        private readonly IProdutoRepository produtoRepository;

        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository _produtoRepository, IMapper mapper1)
        {
            produtoRepository = _produtoRepository;
            _mapper = mapper1;
        }

        [ApiVersion("1.0",Deprecated = true)]
        [HttpGet]
        public async Task<ActionResult<ProdutoResponseVM>> Get()
        {
            var produtos = await produtoRepository.FindAllAsync();
            var produtosVM = _mapper.Map<IList<ProdutoResponseVM>>(produtos);

            return Ok(produtosVM);
        }

        [ApiVersion("2.0")]
        [ApiVersion("3.0")]
        [HttpGet]
        public async Task<ActionResult<ProdutoPaginacaoResponseVM>> Get([FromQuery] int pagina = 0, [FromQuery] int tamanho = 5)
        {
            var apiVersion = HttpContext.GetRequestedApiVersion()?.ToString();

            var totalGeral = await produtoRepository.CountAsync();
            var produtos = await produtoRepository.FindAllAsync(pagina, tamanho);

            var produtoVM = new ProdutoPaginacaoResponseVM();
            produtoVM.Produtos = _mapper.Map<IList<ProdutoResponseVM>>(produtos);
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




        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            else
            {
                var prod = await produtoRepository.FindByIdAsync(id);

                if (prod == null)
                {
                    return NotFound(id);
                }
                else
                {
                    return Ok(prod);
                }
            }
        }




    }
}
