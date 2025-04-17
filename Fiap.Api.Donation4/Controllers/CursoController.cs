using Fiap.Api.Donation4.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fiap.Api.Donation3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {

        private static string endpoint = "https://5cb544bd07f233001424ceb8.mockapi.io/fiap/curso";

        private readonly HttpClient httpClient;

        public CursoController()
        {
            httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<ActionResult<IList<CursoModel>>> Get()
        {
            var resposta = await httpClient.GetAsync(endpoint);

            if (resposta.IsSuccessStatusCode)
            {

                var conteudoJson = await resposta.Content.ReadAsStringAsync();
                var cursos = JsonConvert.DeserializeObject<List<CursoModel>>(conteudoJson);

                return Ok(cursos);

            }
            else
            {
                return BadRequest();
            }

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CursoModel>> Get(int id)
        {
            var resposta = await httpClient.GetAsync($"{endpoint}/{id}");

            if (resposta.IsSuccessStatusCode)
            {

                var conteudoJson = await resposta.Content.ReadAsStringAsync();
                var curso = JsonConvert.DeserializeObject<CursoModel>(conteudoJson);

                return Ok(curso);
            }
            else
            {
                return NotFound();
            }

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CursoModel>> Delete(int id)
        {
            var resposta = await httpClient.DeleteAsync($"{endpoint}/{id}");

            if (resposta.IsSuccessStatusCode)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult<CursoModel>> Post(CursoModel cursoModel)
        {
            var conteudoJson = JsonConvert.SerializeObject(cursoModel);
            var conteudoJsonString = new StringContent(conteudoJson, System.Text.Encoding.UTF8, "application/json");

            var resposta = await httpClient.PostAsync(endpoint, conteudoJsonString);

            if (resposta.IsSuccessStatusCode)
            {
                var respostaSucesso = await resposta.Content.ReadAsStringAsync();
                var curso = JsonConvert.DeserializeObject<CursoModel>(respostaSucesso);

                return Ok(curso);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CursoModel>> Put([FromRoute] int id, CursoModel cursoModel)
        {
            var conteudoJson = JsonConvert.SerializeObject(cursoModel);
            var conteudoJsonString = new StringContent(conteudoJson, System.Text.Encoding.UTF8, "application/json");

            var resposta = await httpClient.PutAsync($"{endpoint}/{id}", conteudoJsonString);

            if (resposta.IsSuccessStatusCode)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }

        }



    }
}
