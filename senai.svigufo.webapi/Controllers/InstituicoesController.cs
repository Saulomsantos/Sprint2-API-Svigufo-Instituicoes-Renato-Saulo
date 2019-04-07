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

        /// <summary>
        /// Retorna uma lista de Instiuições
        /// </summary>
        /// <returns>Retorna uma list de Instituições</returns>
        [HttpGet]
        //public IEnumerable<InstituicaoDomain> Get()
        public IActionResult Get()
        {
            //return InstituicaoRepository.Listar();
            //Recomendado usar returnOk() pelas boas práticas de retornar um status code
            return Ok(InstituicaoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Busca uma instituição pelo seu id
            InstituicaoDomain instituicao = InstituicaoRepository.GetById(id);

            // Verifica se foi encontrado na lista a Instituicao
            if (instituicao == null)
            {
                // Retorna não encontrado
                return NotFound();
            }

            // Retorna ok e a Instituicao
            return Ok();
        }

        /// <summary>
        /// Cadastra uma instituição
        /// </summary>
        /// <param name="instituicao">Recebe uma instituição</param>
        /// <returns>Retorna um status code</returns>
        [HttpPost] //Verbo para gravar
        public IActionResult Post(InstituicaoDomain instituicao)
        {
            try
            {
                //Chama o método para gravar passando a InstituicaoRecebida
                InstituicaoRepository.Gravar(instituicao);

                //Retorna Ok e a lista com as instituições
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }

        /// <summary>
        /// Atualiza uma instituição
        /// </summary>
        /// <param name="id">id da instituição a ser alterada</param>
        /// <param name="instituicao">dados da instituição</param>
        /// <returns>Retorna um status code</returns>
        [HttpPut("{id}")] //Verbo para editar
        public IActionResult Put(int id, InstituicaoDomain instituicao)
        {
            instituicao.Id = id;
            //Chama o método para editar passando a InstituicaoRecebida
            //InstituicaoRepository.Editar(InstituicaoRecebida);
            InstituicaoRepository.Editar(id, instituicao);

            return Ok();
        }

        /// <summary>
        /// Deleta uma instituição
        /// </summary>
        /// <param name="id">id da instituição que será deletada</param>
        /// <returns>Retorna um status code</returns>
        [HttpDelete("{id}")] // Verbo para deletar um registro, passa o id no recurso
        public IActionResult Delete(int id)
        {
            InstituicaoRepository.Deletar(id);

            return Ok();
        }
    }
}