using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using Senai.SviGufo.WebApi.Repositories;
using System;

namespace Senai.SviGufo.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos eventos
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] // Implementa funcionalidades ao Controller

    public class EventosController : ControllerBase
    {
        // Define um objeto EventoRepository para chamada dos métodos
        private IEventoRepository EventoRepository { get; set; }

        public EventosController()
        {
            // Cria uma instancia de EventoRepository
            EventoRepository = new EventoRepository();
        }

        /// <summary>
        /// Lista todos os eventos
        /// </summary>
        /// <returns>Retorna uma lista de eventos</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try // Tenta listar
            {
                // Retorna um status code 200 e a lista de eventos
                return Ok(EventoRepository.Listar());
            }
            catch // Caso não consiga
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }

        /// <summary>
        /// Cadastra um novo evento
        /// </summary>
        /// <param name="evento">Recebe um objeto evento na requisição</param>
        /// <returns>Retorna o Status Code</returns>
        [HttpPost]
        public IActionResult Post(EventoDomain evento)
        {
            try // Tenta cadastrar
            {
                // Faz a chamada para o método cadastrar passando o objeto evento recebido na requisição
                EventoRepository.Cadastrar(evento);

                // Retorna um status code 200
                return Ok();
            }
            catch // Caso dê erro
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }

        /// <summary>
        /// Atualiza um evento passando o id pela URL
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <param name="evento">Recebe um objeto evento na requisição</param>
        /// <returns>Retorna Status Code</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, EventoDomain evento)
        {
            try // Tenta arualizar
            {
                // Faz a chamada para o método passando o id do evento e os dados que serão atualizados
                EventoRepository.Atualizar(id, evento);

                // Retorna um status code 200
                return Ok();
            }
            catch (Exception ex) // Caso dê erro, armazena o erro na variável ex
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }
    }
}