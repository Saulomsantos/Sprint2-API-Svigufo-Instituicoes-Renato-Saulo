using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using System.Collections.Generic;
using senai.svigufo.webapi.Repositories;

namespace senai.svigufo.webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicoesController : ControllerBase
    {

        List<TipoEventoDomain> Instituicoes = new List<TipoEventoDomain>();
        
        private IInstituicaoRepository InstituicaoRepository { get; set; }

        public InstituicoesController()
        {
            InstituicaoRepository = new InstituicaoRepository();
        }

        [HttpGet]
        public IEnumerable<InstituicaoDomain> Get()
        {
            return InstituicaoRepository.Listar();
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            // Busca um tipo de evento pelo seu id
            TipoEventoDomain Instituicao = Instituicoes.Find(x => x.Id == id);

            // Verifica se foi encontrado na lista a Instituicao
            if (Instituicao == null)
            {
                // Retorna não encontrado
                return NotFound();
            }

            // Retorna ok e a Instituicao
            return Ok(Instituicao);
        }

        [HttpPost] //Verbo para gravar
        public IActionResult Post(InstituicaoDomain InstituicaoRecebida)
        {
            //Chama o método para gravar passando a InstituicaoRecebida
            InstituicaoRepository.Gravar(InstituicaoRecebida);

            //Retorna Ok e a lista com as instituições
            return Ok();
        }

        [HttpPut("{id}")] //Verbo para editar
        public IActionResult Put(int id, InstituicaoDomain InstituicaoRecebida)
        {
            InstituicaoRecebida.Id = id;
            //Chama o método para editar passando a InstituicaoRecebida
            //InstituicaoRepository.Editar(InstituicaoRecebida);
            InstituicaoRepository.Editar(id, InstituicaoRecebida);

            return Ok();
        }

        [HttpDelete("{id}")] // Verbo para deletar um registro, passa o id no recurso
        public IActionResult Delete(int id)
        {
            

            return Ok();
        }
    }
}