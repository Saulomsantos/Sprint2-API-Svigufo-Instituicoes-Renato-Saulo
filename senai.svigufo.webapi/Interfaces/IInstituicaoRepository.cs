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
        /// Grava uma nova instituição
        /// </summary>
        /// <param name="Instituicao">Objeto Instituicao</param>
        void Gravar(InstituicaoDomain Instituicao);

        /// <summary>
        /// Edita uma instituição
        /// </summary>
        /// <param name="Instituicao">Objeto Instituicao</param>
        //void Editar(InstituicaoDomain Instituicao);
        void Editar(int id, InstituicaoDomain Instituicao);
    }
}
