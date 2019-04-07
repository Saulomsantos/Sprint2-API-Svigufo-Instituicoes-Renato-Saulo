using senai.svigufo.webapi.Domains;
using System.Collections.Generic;

namespace senai.svigufo.webapi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório da Instituição
    /// </summary>
    public interface IInstituicaoRepository
    {
        /// <summary>
        /// Lista todas as Instituições
        /// </summary>
        /// <returns>Retorna uma lista com todas as instituições</returns>
        List<InstituicaoDomain> Listar();

        /// <summary>
        /// Busca uma instituição pelo id
        /// </summary>
        /// <param name="id">Id da instituição</param>
        /// <returns>Retorna uma instituição</returns>
        InstituicaoDomain GetById(int id);

        /// <summary>
        /// Grava uma nova instituição
        /// </summary>
        /// <param name="instituicao">Recebe um objeto instituicao</param>
        void Gravar(InstituicaoDomain instituicao);

        /// <summary>
        /// Edita uma instituição
        /// </summary>
        /// <param name="id">Id da instituição</param>
        /// <param name="instituicao">Recebe um objeto instituição</param>
        void Editar(int id, InstituicaoDomain instituicao);

        /// <summary>
        /// Deleta uma instituição
        /// </summary>
        /// <param name="id">Id da instituição</param>
        void Deletar(int id);
    }
}
