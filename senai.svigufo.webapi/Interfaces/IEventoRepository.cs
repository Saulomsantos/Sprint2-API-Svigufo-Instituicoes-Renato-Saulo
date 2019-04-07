using senai.svigufo.webapi.Domains;
using System.Collections.Generic;

namespace senai.svigufo.webapi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório dos eventos 
    /// </summary>
    public interface IEventoRepository
    {
        /// <summary>
        /// Lista os eventos
        /// </summary>
        /// <returns>Retorna uma lista de eventos</returns>
        List<EventoDomain> Listar();

        /// <summary>
        /// Cadastra um novo evento
        /// </summary>
        /// <param name="evento">Recebe um objeto evento</param>
        void Cadastrar(EventoDomain evento);

        /// <summary>
        /// Atualiza um evento
        /// </summary>
        /// <param name="id">Id do evento</param>
        /// <param name="evento">Recebe um objeto evento</param>
        void Atualizar(int id, EventoDomain evento);
    }
}
