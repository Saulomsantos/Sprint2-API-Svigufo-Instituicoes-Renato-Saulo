using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Domains.Enums;
using senai.svigufo.webapi.Interfaces;

namespace Senai.SviGufo.WebApi.Repositories
{
    /// <summary>
    /// Repositório dos convites
    /// </summary>
    public class ConviteRepository : IConviteRepository
    {
        // Define a string de conexão
        private string StringConexao = "Data Source=.\\SqlDeveloper; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND; integrated security=true";

        /// <summary>
        /// Cadastra um convite
        /// </summary>
        /// <param name="convite"></param>
        public void Cadastrar(ConviteDomain convite)
        {
            // Define a query que será executada no banco de dados
            string QueryInsert = @"INSERT INTO CONVITES(ID_EVENTO,ID_USUARIO,SITUACAO) VALUES (@ID_EVENTO, @ID_USUARIO, @SITUACAO)";

            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Abre a conexão com o banco de dados
                con.Open();

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryInsert, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID_EVENTO", convite.EventoId);
                    cmd.Parameters.AddWithValue("@ID_USUARIO", convite.UsuarioId);
                    cmd.Parameters.AddWithValue("@SITUACAO", convite.Situacao);

                    // Executa a query
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lista todos os convites
        /// </summary>
        /// <returns>Retorna uma lista de Convites</returns>
        public List<ConviteDomain> Listar()
        {
            // Define a query que será executada no banco
            string QuerySelect = @"SELECT
	                                C.ID AS ID_CONVITE,
	                                C.SITUACAO,
	                                E.TITULO AS TITULO_EVENTO,
	                                E.DATA_EVENTO,
	                                TE.ID AS ID_TIPO_EVENTO,
	                                TE.TITULO AS TITULO_TIPO_EVENTO,
	                                U.NOME AS NOME_USUARIO,
	                                U.EMAIL AS EMAIL_USUARIO,
	                                U.ID AS ID_USUARIO
                                   FROM CONVITES C
                                   INNER JOIN EVENTOS E
                                   ON C.ID_EVENTO = E.ID
                                   INNER JOIN USUARIOS U
                                   ON C.ID_USUARIO = U.ID
                                   INNER JOIN TIPOS_EVENTOS TE
                                   ON TE.ID = E.ID_TIPO_EVENTO;";

            // Define uma lista de convites
            List<ConviteDomain> convites = new List<ConviteDomain>();

            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Abre a conexão com o banco de dados
                con.Open();

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    // Define um objeto para percorrer as os dados
                    SqlDataReader sdr = cmd.ExecuteReader();

                    // Enquanto estiver lendo
                    while (sdr.Read())
                    {
                        // Define um objeto convite
                        ConviteDomain convite = new ConviteDomain
                        {
                            // Passa os valores buscados no banco e atribui aos convite
                            Id = Convert.ToInt32(sdr["ID_CONVITE"]),
                            Situacao = (EnSituacaoConvite)Convert.ToInt32(sdr["SITUACAO"]),
                            Usuario = new UsuarioDomain
                            {
                                Id = Convert.ToInt32(sdr["ID_USUARIO"]),
                                Nome = sdr["NOME_USUARIO"].ToString(),
                                Email = sdr["EMAIL_USUARIO"].ToString()
                            },
                            Evento = new EventoDomain
                            {
                                Titulo = sdr["TITULO_EVENTO"].ToString(),
                                DataEvento = Convert.ToDateTime(sdr["DATA_EVENTO"]),
                                TipoEvento = new TipoEventoDomain
                                {
                                    Id = Convert.ToInt32(sdr["ID_TIPO_EVENTO"]),
                                    Nome = sdr["TITULO_TIPO_EVENTO"].ToString()
                                }
                            }
                        };

                        // Adiciona o convite à lista
                        convites.Add(convite);
                    }
                }
            }

            // Retorna a lista de convites
            return convites;
        }

        /// <summary>
        /// Lista os convites de um determinado usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns></returns>
        public List<ConviteDomain> ListarMeusConvites(int id)
        {
            // Define a query a ser executada no banco
            string QuerySelect = @"SELECT
	                                C.ID AS ID_CONVITE,
	                                C.SITUACAO,
	                                E.TITULO AS TITULO_EVENTO,
	                                E.DATA_EVENTO,
	                                TE.ID AS ID_TIPO_EVENTO,
	                                TE.TITULO AS TITULO_TIPO_EVENTO,
	                                U.NOME AS NOME_USUARIO,
	                                U.EMAIL AS EMAIL_USUARIO,
	                                U.ID AS ID_USUARIO
                                   FROM CONVITES C
                                   INNER JOIN EVENTOS E
                                   ON C.ID_EVENTO = E.ID
                                   INNER JOIN USUARIOS U
                                   ON C.ID_USUARIO = U.ID
                                   INNER JOIN TIPOS_EVENTOS TE
                                   ON TE.ID = E.ID_TIPO_EVENTO
                                   WHERE C.ID_USUARIO = @ID;";

            // Define uma lista de convitess
            List<ConviteDomain> convites = new List<ConviteDomain>();

            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Abre a conexão com o banco de dados
                con.Open();

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    // Passa os valores dos parâmetros
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Define um objeto para leitura dos dados
                    SqlDataReader sdr = cmd.ExecuteReader();

                    // Enquanto estiver lendo
                    while (sdr.Read())
                    {
                        // Define um objeto convite
                        ConviteDomain convite = new ConviteDomain
                        {
                            // Passa os valores buscados no banco ao convite
                            Id = Convert.ToInt32(sdr["ID_CONVITE"]),
                            Situacao = (EnSituacaoConvite)Convert.ToInt32(sdr["SITUACAO"]),
                            Usuario = new UsuarioDomain
                            {
                                Id = Convert.ToInt32(sdr["ID_USUARIO"]),
                                Nome = sdr["NOME_USUARIO"].ToString(),
                                Email = sdr["EMAIL_USUARIO"].ToString()
                            },
                            Evento = new EventoDomain
                            {
                                Titulo = sdr["TITULO_EVENTO"].ToString(),
                                DataEvento = Convert.ToDateTime(sdr["DATA_EVENTO"]),
                                TipoEvento = new TipoEventoDomain
                                {
                                    Id = Convert.ToInt32(sdr["ID_TIPO_EVENTO"]),
                                    Nome = sdr["TITULO_TIPO_EVENTO"].ToString()
                                }
                            }
                        };

                        // Adiciona o convite à lista
                        convites.Add(convite);
                    }
                }
            }

            // Retorna a lista
            return convites;
        }
    }
}
