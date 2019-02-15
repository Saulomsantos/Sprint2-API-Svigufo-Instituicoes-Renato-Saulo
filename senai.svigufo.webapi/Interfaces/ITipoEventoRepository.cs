using senai.svigufo.webapi.Domains;
using System.Collections.Generic;

namespace senai.svigufo.webapi.Interfaces
{
    public interface ITipoEventoRepository
    {
        /// <summary>
        /// Lista todos os tipos de eventos
        /// </summary>
        /// <returns>Retorna uma lista de tipo eventos</returns>
        List<TipoEventoDomain> Listar();

        /// <summary>
        /// Cadastra um novo tipo de evento
        /// </summary>
        /// <param name="tipoEvento">Objeto TipoEvento</param>
        void Cadastrar(TipoEventoDomain tipoEvento);

        /// <summary>
        /// Altera um tipo de evento
        /// </summary>
        /// <param name="tipoEvento">Objeto TipoEvento</param>
        void Alterar(TipoEventoDomain tipoEvento);

        /// <summary>
        /// Deleta um tipo de evento
        /// </summary>
        /// <param name="tipoEvento">Id do tipo de evento</param>
        void Deletar(int id);
    }
}
