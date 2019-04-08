using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;

namespace senai.svigufo.webapi.Repositories
{
    /// <summary>
    /// Repositório dos tipos eventos
    /// </summary>
    public class TipoEventoRepository : ITipoEventoRepository
    {
        // Na dúvida, procurar no site connection string qual a string de conexão do banco utilizado
        // private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND;user id=sa; pwd=132";
        private string StringConexao = "Data Source=.\\SqlDeveloper; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND; integrated security=true;";

        /// <summary>
        /// Altera um tipo de evento
        /// </summary>
        /// <param name="tipoEvento">Recebe um objeto tipoEvento</param>
        public void Alterar(TipoEventoDomain tipoEvento)
        {
            // Define a conexão passando a string
            using(SqlConnection con = new SqlConnection(StringConexao))
            {
                // Define a query que será comunicada com o banco de dados
                string Query = "UPDATE TIPOS_EVENTOS SET TITULO = @TITULO WHERE ID = @ID";

                // Define o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(Query, con);

                // Passa o valor dos parâmetros
                cmd.Parameters.AddWithValue("@TITULO", tipoEvento.Nome);
                cmd.Parameters.AddWithValue("@ID", tipoEvento.Id);

                // Abre a conexão com o banco
                con.Open();
                
                // Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cadastra um novo tipo de evento
        /// </summary>
        /// <param name="tipoEvento">TipoEventoDomain</param>
        public void Cadastrar(TipoEventoDomain tipoEvento)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Não usar dessa forma pois pode causar o efeito Joana D'arc
                // string QuerySerExecutada = "INSERT INTO TIPOS_EVENTOS(TITULO) VALUES ('" + tipoEvento.Nome + "')";
                
                // Declara a query passando o valor como parametro
                string QuerySerExecutada = "INSERT INTO TIPOS_EVENTOS(TITULO) VALUES (@TITULO)";

                // Declara o command passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QuerySerExecutada, con);

                // Passa o valor do parâmetro
                cmd.Parameters.AddWithValue("@TITULO", tipoEvento.Nome);

                // Abre a conexão com o banco
                con.Open();

                // Executa o comando
                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Deleta um tipo de evento
        /// </summary>
        /// <param name="id">Id do tipo de evento a ser deletado</param>
        public void Deletar(int id)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query a ser executada
                string Query = "DELETE FROM TIPOS_EVENTOS WHERE ID = @ID";

                // Declara o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(Query, con);

                // Passa o valor do parâmetro
                cmd.Parameters.AddWithValue("@ID", id);

                // Abre a conexão com o banco
                con.Open();

                // Executa o comando
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Lista os tipos de eventos
        /// </summary>
        /// <returns>Retorna uma lista com os tipos de eventos</returns>
        public List<TipoEventoDomain> Listar()
        {
            // Cria uma lista tiposEventos onde serão armazenados os dados
            List<TipoEventoDomain> tiposEventos = new List<TipoEventoDomain>();

            // Declaro a SqlConnection passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a instrução a ser executada
                string QueryaSerExecutada = "SELECT ID, TITULO FROM TIPOS_EVENTOS";

                // Abre o banco de dados
                con.Open();

                // Declaro um SqlDtaReader para percorrer a lista
                SqlDataReader rdr;

                // Declaro um command passando o comando a ser executado e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryaSerExecutada, con))
                {
                    // Executa a query
                    rdr = cmd.ExecuteReader();

                    // Não precisa do rdr.Read() == true pois sendo verdadeiro o == true pode ser suprimido
                    while (rdr.Read())
                    {
                        TipoEventoDomain tipoEvento = new TipoEventoDomain()
                        {
                            Id = Convert.ToInt32(rdr["ID"]),
                            Nome = rdr["TITULO"].ToString()
                        };

                        // Adiciona um tipo de evento à lista
                        tiposEventos.Add(tipoEvento);
                    }
                }
            }

            // Retorna a lista
            return tiposEventos;
        }
    }
}
