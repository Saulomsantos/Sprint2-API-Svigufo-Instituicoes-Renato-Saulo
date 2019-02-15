using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using senai.svigufo.webapi.Repositories;
using System.Collections.Generic;

namespace senai.svigufo.webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] //Implementa funcionalidades em nosso Controller
    public class TiposEventosController : ControllerBase
    {
        List<TipoEventoDomain> tiposEventos = new List<TipoEventoDomain>()
        // Cada par de { representa um arranjo JSON
        {
            // Cada par de { representa um objeto JSON
            new TipoEventoDomain{ Id = 1, Nome = "Tecnologia"},
            new TipoEventoDomain{ Id = 2, Nome = "Redes"},
            new TipoEventoDomain{ Id = 3, Nome = "Gestão"}
        };

        // Cria um objeto do tipo ITipoEventoRepository
        private ITipoEventoRepository TipoEventoRepository { get; set; }

        public TiposEventosController()
        {
            // Cria uma instancia de tipoeventorepository
            TipoEventoRepository = new TipoEventoRepository();
        }

        //[HttpGet]
        //public string Get()
        //{
        //    return "Recebi sua requisição";
        //}

        /// <summary>
        /// Retorna uma lista de eventos
        /// </summary>
        /// <returns>Lista de eventos</returns>
        [HttpGet]
        public IEnumerable<TipoEventoDomain> Get()
        {
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

        [HttpPost] // Verbo para inserir
        // [FromBody] Pega os dados enviados para a api (vai antes do TipoEventoDomain)
        public IActionResult Post(TipoEventoDomain tipoEvento)
        {
            // Adiciona o tipo de evento recebido na Api
            //tiposEventos.Add(new TipoEventoDomain
            //{
            //    Id = tiposEventos.Count + 1,
            //    Nome = tipoEvento.Nome
            //});

            TipoEventoRepository.Cadastrar(tipoEvento);

            // Retorna Ok e a lista com os tipos de eventos
            return Ok();
        }

        [HttpPut] // Verbo para atualizar
        public IActionResult Put(TipoEventoDomain tipoEventoRecebido)
        {
            TipoEventoRepository.Alterar(tipoEventoRecebido);

            return Ok(tipoEventoRecebido);
        }

        //[HttpPut("{id}")] // Verbo para alterar, passa o id no recurso
        //public IActionResult Put(int id, TipoEventoDomain tipoEventoRecebido)
        //{
        //    return Ok(tipoEventoRecebido);
        //}

        [HttpDelete("{id}")] // Verbo para deletar um registro, passa o id no recurso
        public IActionResult Delete(int id)
        {
            //tiposEventos.Remove(tiposEventos.Find(x => x.Id == id));

            TipoEventoRepository.Deletar(id);

            return Ok();
        }
    }
}