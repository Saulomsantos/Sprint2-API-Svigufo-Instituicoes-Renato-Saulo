using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using senai.svigufo.webapi.Repositories;
using System.Collections.Generic;

namespace senai.svigufo.webapi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos Tipos Eventos
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] // Implementa funcionalidades em nosso Controller

    public class TiposEventosController : ControllerBase
    {
        // Cria uma lista tiposEventos
        List<TipoEventoDomain> tiposEventos = new List<TipoEventoDomain>()

        // Cada par de { representa um arranjo JSON
        {
            // Cada par de { representa um objeto JSON
            new TipoEventoDomain{ Id = 1, Nome = "Tecnologia"},
            new TipoEventoDomain{ Id = 2, Nome = "Redes"},
            new TipoEventoDomain{ Id = 3, Nome = "Gestão"}
        };

        // Define um objeto TipoEventoRepository para chamada dos métodos
        private ITipoEventoRepository TipoEventoRepository { get; set; }

        public TiposEventosController()
        {
            // Cria uma instancia de tipoEventoRepository
            TipoEventoRepository = new TipoEventoRepository();
        }

        // [HttpGet]
        // public string Get()
        // {
        //    return "Recebi sua requisição";
        // }

        /// <summary>
        /// Retorna uma lista de tipos de eventos
        /// </summary>
        /// <returns>Lista de Eventos</returns>
        [HttpGet]
        public IEnumerable<TipoEventoDomain> Get()
        {
            // Retorna uma lista de tipos eventos
            return TipoEventoRepository.Listar();
        }

        /// <summary>
        /// Busca o tipo do evento pelo Id
        /// </summary>
        /// <param name="id">Id do tipo do evento</param>
        /// <returns>Retorna um tipo de evento</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Busca um tipo de evento pelo seu id
            TipoEventoDomain tipoEvento = tiposEventos.Find(x => x.Id == id);

            // Verifica se foi encontrado na lista o tipo de evento
            if (tipoEvento == null)
            {
                // Retorna não encontrado
                return NotFound();
            }

            // Retorna ok e o tipo do evento
            return Ok(tipoEvento);
        }

        /// <summary>
        /// Cadastra um tipo de evento
        /// </summary>
        /// <param name="tipoEventoRecebido">Recebe um objeto tipoEventoRecebido</param>
        /// <returns></returns>
        [HttpPost] // Verbo para inserir
        // [FromBody] Pega os dados enviados para a api (vai antes do TipoEventoDomain)
        public IActionResult Post(TipoEventoDomain tipoEventoRecebido)
        {
            // Adiciona o tipo de evento recebido na Api
            // tiposEventos.Add(new TipoEventoDomain
            // {
            //    Id = tiposEventos.Count + 1,
            //    Nome = tipoEvento.Nome
            // });

            // Cadastra um tipo de evento recebido na requisição
            TipoEventoRepository.Cadastrar(tipoEventoRecebido);

            // Retorna um status code 200
            return Ok();
        }

        /// <summary>
        /// Atualiza um tipo de evento passando o id no corpo do JSON
        /// </summary>
        /// <param name="tipoEventoRecebido">Recebe um objeto tipoEventoRecebido</param>
        /// <returns>Retorna um status code</returns>
        [HttpPut] // Verbo para atualizar
        public IActionResult Put(TipoEventoDomain tipoEventoRecebido)
        {
            // Altera um tipo de evento passando os dados do objeto da requisição
            TipoEventoRepository.Alterar(tipoEventoRecebido);

            // Retorna um status code 200
            return Ok();
        }

        // Outra forma
        // [HttpPut("{id}")] // Verbo para alterar, passa o id no recurso
        // public IActionResult Put(int id, TipoEventoDomain tipoEventoRecebido)
        // {
        //    return Ok(tipoEventoRecebido);
        // }

        /// <summary>
        /// Deleta um tipo de evento passando o id na URL
        /// </summary>
        /// <param name="id">Id do tipo de evento a ser excluído</param>
        /// <returns>Retorna um status code</returns>
        [HttpDelete("{id}")] // Verbo para deletar um registro, passa o id no recurso
        public IActionResult Delete(int id)
        {
            // Busca um tipo de evento pelo seu id
            TipoEventoDomain tipoEvento = tiposEventos.Find(x => x.Id == id);

            // Verifica se foi encontrado na lista o tipo de evento
            if (tipoEvento == null)
            {
                // Retorna um status code 404 Not Found
                return NotFound();
            }

            // Se for encontrado, deleta o tipo de evento
            TipoEventoRepository.Deletar(id);

            // Retorna um status code 200
            return Ok();
        }
    }
}