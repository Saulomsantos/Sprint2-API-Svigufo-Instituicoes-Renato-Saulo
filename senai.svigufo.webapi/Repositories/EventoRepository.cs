using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Senai.SviGufo.WebApi.Repositories
{
    /// <summary>
    /// Repositório dos eventos
    /// </summary>
    public class EventoRepository : IEventoRepository
    {
        // Define a string de conexão
        private string StringConexao = "Data Source=.\\SqlDeveloper; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND; integrated security=true";

        /// <summary>
        /// Atualiza um evento
        /// </summary>
        /// <param name="id">Id do Evento</param>
        /// <param name="evento">Recebe um objeto evento</param>
        public void Atualizar(int id, EventoDomain evento)
        {
            // Define a query que será executada no banco
            string QueryUpdate = "UPDATE EVENTOS SET TITULO = @TITULO, DESCRICAO = @DESCRICAO, DATA_EVENTO = @DATA_EVENTO, ACESSO_LIVRE = @ACESSO_LIVRE, ID_INSTITUICAO = @ID_INSTITUICAO, ID_TIPO_EVENTO = @ID_TIPO_EVENTO WHERE ID = @ID";

            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Abre a conexão com o banco de dados
                con.Open();

                // Define o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QueryUpdate, con);

                // Passa os valores dos parâmetros
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@TITULO", evento.Titulo);
                cmd.Parameters.AddWithValue("@DESCRICAO", evento.Descricao);
                cmd.Parameters.AddWithValue("@DATA_EVENTO", evento.DataEvento);
                cmd.Parameters.AddWithValue("@ACESSO_LIVRE", evento.AcessoLivre);
                cmd.Parameters.AddWithValue("@ID_INSTITUICAO", evento.InstituicaoId);
                cmd.Parameters.AddWithValue("@ID_TIPO_EVENTO", evento.TipoEventoId);

                // Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cadastra um evento
        /// </summary>
        /// <param name="evento">Recebe um objeto evento</param>
        public void Cadastrar(EventoDomain evento)
        {
            // Define a query que será executada no banco
            string QueryInsert = @"INSERT INTO EVENTOS(TITULO, DESCRICAO, DATA_EVENTO, ACESSO_LIVRE, ID_INSTITUICAO, ID_TIPO_EVENTO) VALUES(@TITULO, @DESCRICAO, @DATA_EVENTO, @ACESSO_LIVRE, @ID_INSTITUICAO, @ID_TIPO_EVENTO)";

            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Abre a conexão com o banco de dados
                con.Open();

                // Define o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QueryInsert, con);

                // Passa os valores dos parâmetros
                cmd.Parameters.AddWithValue("@TITULO", evento.Titulo);
                cmd.Parameters.AddWithValue("@DESCRICAO", evento.Descricao);
                cmd.Parameters.AddWithValue("@DATA_EVENTO", evento.DataEvento);
                cmd.Parameters.AddWithValue("@ACESSO_LIVRE", evento.AcessoLivre);
                cmd.Parameters.AddWithValue("@ID_INSTITUICAO", evento.InstituicaoId);
                cmd.Parameters.AddWithValue("@ID_TIPO_EVENTO", evento.TipoEventoId);

                // Executa a query
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lista todos os eventos
        /// </summary>
        /// <returns>Retorna uma lista de eventos</returns>
        public List<EventoDomain> Listar()
        {
            // Define a query que será executada no banco
            string QuerySelect = @"SELECT 
	                                E.ID AS ID_EVENTO, 
	                                E.TITULO AS TITULO_EVENTO,
	                                E.DESCRICAO,
	                                E.DATA_EVENTO,
	                                E.ACESSO_LIVRE,
	                                TE.ID AS ID_TIPO_EVENTO,
	                                TE.TITULO AS TITULO_TIPO_EVENTO,
	                                I.ID AS ID_INSTITUICAO,
	                                I.NOME_FANTASIA,
	                                I.RAZAO_SOCIAL,
	                                I.CNPJ,
	                                I.LOGRADOURO,
	                                I.CEP,
	                                I.UF,
	                                I.CIDADE
                                   FROM EVENTOS E
                                    INNER JOIN TIPOS_EVENTOS TE ON 	E.ID_TIPO_EVENTO = TE.ID
                                    INNER JOIN INSTITUICOES I ON E.ID_INSTITUICAO = I.ID";

            // Define uma lista listaEventos
            List<EventoDomain> listaEventos = new List<EventoDomain>();

            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Abre a conexão com o banco de dados
                con.Open();

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    // Define o objeto para percorrer os dados
                    SqlDataReader sdr = cmd.ExecuteReader();

                    // Enquanto percorrer os dados
                    while (sdr.Read())
                    {
                        // Define um objeto evento
                        EventoDomain evento = new EventoDomain
                        {
                            // Atribui os dados do banco ao objeto
                            Id = Convert.ToInt32(sdr["ID_EVENTO"]),
                            Titulo = sdr["TITULO_EVENTO"].ToString(),
                            Descricao = sdr["DESCRICAO"].ToString(),
                            DataEvento = Convert.ToDateTime(sdr["DATA_EVENTO"]),
                            AcessoLivre = Convert.ToBoolean(sdr["ACESSO_LIVRE"]),
                            TipoEventoId = Convert.ToInt32(sdr["ID_TIPO_EVENTO"]),
                            TipoEvento = new TipoEventoDomain
                            {
                                Id = Convert.ToInt32(sdr["ID_TIPO_EVENTO"]),
                                Nome = sdr["TITULO_TIPO_EVENTO"].ToString()
                            },
                            InstituicaoId = Convert.ToInt32(sdr["ID_INSTITUICAO"]),
                            Instituicao = new InstituicaoDomain
                            {
                                Id = Convert.ToInt32(sdr["ID_INSTITUICAO"]),
                                NomeFantasia = sdr["NOME_FANTASIA"].ToString(),
                                RazaoSocial = sdr["RAZAO_SOCIAL"].ToString(),
                                CNPJ = sdr["CNPJ"].ToString(),
                                Logradouro = sdr["LOGRADOURO"].ToString(),
                                CEP = sdr["CEP"].ToString(),
                                UF = sdr["UF"].ToString(),
                                Cidade = sdr["CIDADE"].ToString()
                            }
                        };

                        // Adiciona o evento à lista
                        listaEventos.Add(evento);
                    }
                }
            }

            // Retorna a lista de eventos
            return listaEventos;

        }
    }
}
