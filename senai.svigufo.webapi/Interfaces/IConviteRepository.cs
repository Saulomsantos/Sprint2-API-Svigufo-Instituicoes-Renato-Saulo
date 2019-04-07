using senai.svigufo.webapi.Domains;
using System.Collections.Generic;

namespace senai.svigufo.webapi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório convites
    /// </summary>
    public interface IConviteRepository
    {
        /// <summary>
        /// Lista todos os convites
        /// </summary>
        /// <returns></returns>
        List<ConviteDomain> Listar();

        /// <summary>
        /// Lista todos os convites de um determinado usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>Retorna uma lista de convites</returns>
        List<ConviteDomain> ListarMeusConvites(int id);

        /// <summary>
        /// Cadastra um novo convite
        /// </summary>
        /// <param name="convite">Recebe um objeto convite</param>
        void Cadastrar(ConviteDomain convite);
    }
}
