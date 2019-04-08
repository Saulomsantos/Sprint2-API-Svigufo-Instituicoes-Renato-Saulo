using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using System.Collections.Generic;
using senai.svigufo.webapi.Repositories;

namespace senai.svigufo.webapi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes à Instituição
    /// </summary>
    [Authorize(Roles = "ADMINISTRADOR")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] // Implementa funcionalidades ao controller
    public class InstituicoesController : ControllerBase
    {
        // Define um objeto InstituicaoRepository para chamada dos métodos
        private IInstituicaoRepository InstituicaoRepository { get; set; }

        public InstituicoesController()
        {
            // Instancia o repositório referente à Instituição
            InstituicaoRepository = new InstituicaoRepository();
        }

        /// <summary>
        /// Retorna uma lista de Instiuições
        /// </summary>
        /// <returns>Retorna uma list de Instituições</returns>
        [HttpGet]
        // Outra forma
        // public IEnumerable<InstituicaoDomain> Get()

        public IActionResult Get()
        {
            // Outra forma
            // return InstituicaoRepository.Listar();

            // Recomendado usar returnOk() pelas boas práticas de retornar um status code
            return Ok(InstituicaoRepository.Listar());
        }

        /// <summary>
        /// Busca uma instituição pelo id
        /// </summary>
        /// <param name="id">Id da instituição buscada</param>
        /// <returns>Retorna uma instituição</returns>
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

            // Retorna ok (status code 200) e a Instituicao
            return Ok();
        }

        /// <summary>
        /// Cadastra uma nova instituição
        /// </summary>
        /// <param name="instituicao">Recebe uma instituição</param>
        /// <returns>Retorna um status code</returns>
        [HttpPost] //Verbo para gravar
        public IActionResult Post(InstituicaoDomain instituicao)
        {
            // Tenta fazer uma operação
            try
            {
                // Chama o método para gravar passando a InstituicaoRecebida na requisição
                InstituicaoRepository.Gravar(instituicao);

                // Retorna o status code 200
                return Ok();
            }
            // Caso não consiga
            catch
            {
                // Retorna um status code com uma mensagem
                return BadRequest("Deu ruim");
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
            {
                // Outra forma
                // instituicao.Id = id;
                // Chama o método para editar passando a InstituicaoRecebida
                // InstituicaoRepository.Editar(InstituicaoRecebida);
                // InstituicaoRepository.Editar(id, instituicao);

                // return Ok();
            }

            // Busca uma instituição pelo seu id
            InstituicaoDomain instituicaoBuscada = InstituicaoRepository.GetById(id);

            // Verifica se a instituição buscada é nula
            if (instituicaoBuscada == null)
            {
                // Retorna com o status code 404 Not Found passando um json na resposta
                return NotFound(new
                {
                    mensagem = "A instituição não foi encontrada.",
                    erro = true
                });
            }

            try // Tenta caso não seja nula
            {
                // Altera uma instituição passando como parâmetro seus dados e o id da instituição
                InstituicaoRepository.Editar(id, instituicao);
                // Retorna um status code 200
                return Ok();
            }
            catch // Caso dê erro
            {
                // Retorna um status code 400
                return BadRequest();
            }

        }

        /// <summary>
        /// Deleta uma instituição pelo seu id
        /// </summary>
        /// <param name="id">id da instituição que será deletada</param>
        /// <returns>Retorna um status code</returns>
        [HttpDelete("{id}")] // Verbo para deletar um registro, passa o id no recurso
        public IActionResult Delete(int id)
        {
            // Busca uma instituição pelo seu id
            InstituicaoDomain instituicaoBuscada = InstituicaoRepository.GetById(id);

            // Verifica se a instituição buscada é nula
            if (instituicaoBuscada == null)
            {
                // Retorna com o status code 404 Not Found passando um json na resposta
                return NotFound(new
                {
                    mensagem = "A instituição não foi encontrada.",
                    erro = true
                });
            }

            try // Tenta excluir
            {
                // Exclui uma instituição
                InstituicaoRepository.Deletar(id);
                // Retorna um status code 200
                return Ok();
            }
            catch // Caso não consiga
            {
                // Retorna um status code 400
                return BadRequest();
            }
        }
    }
}