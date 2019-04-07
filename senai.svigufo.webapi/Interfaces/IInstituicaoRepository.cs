using senai.svigufo.webapi.Domains;
using System.Collections.Generic;

namespace senai.svigufo.webapi.Interfaces
{
    interface IInstituicaoRepository
    {
        /// <summary>
        /// Lista todas as Instituições
        /// </summary>
        /// <returns>Retorna uma lista com todas as instituições</returns>
        List<InstituicaoDomain> Listar();

        /// <summary>
        /// Busca uma instituição pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        InstituicaoDomain GetById(int id);

        /// <summary>
        /// Grava uma nova instituição
        /// </summary>
        /// <param name="instituicao">Objeto Instituicao</param>
        void Gravar(InstituicaoDomain instituicao);

        /// <summary>
        /// Edita uma instituição
        /// </summary>
        /// <param name="instituicao">Objeto Instituicao</param>
        //void Editar(InstituicaoDomain Instituicao);
        void Editar(int id, InstituicaoDomain instituicao);

        /// <summary>
        /// Deleta uma instituicao
        /// </summary>
        /// <param name="instituicao">Id da Instituicao</param>
        void Deletar(int id);
    }
}
