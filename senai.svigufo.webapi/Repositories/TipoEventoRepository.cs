using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;

namespace senai.svigufo.webapi.Repositories
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND;user id=sa; pwd=132";

        public void Alterar(TipoEventoDomain tipoEvento)
        {
            using(SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "UPDATE TIPOS_EVENTOS SET TITULO = @TITULO WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@TITULO", tipoEvento.Nome);

                cmd.Parameters.AddWithValue("@ID", tipoEvento.Id);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Cadastra um novo tipo de evento
        /// </summary>
        /// <param name="tipoEvento">TipoEventoDomain</param>
        public void Cadastrar(TipoEventoDomain tipoEvento)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Não usar dessa forma pois pode causar o efeito Joana D'arc
                //string QuerySerExecutada = "INSERT INTO TIPOS_EVENTOS(TITULO) VALUES ('" + tipoEvento.Nome + "')";
                //Declara a query passando o valor como parametro
                string QuerySerExecutada = "INSERT INTO TIPOS_EVENTOS(TITULO) VALUES (@TITULO)";

                //Declara o command passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QuerySerExecutada, con);

                cmd.Parameters.AddWithValue("@TITULO", tipoEvento.Nome);

                con.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                string Query = "DELETE FROM TIPOS_EVENTOS WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@ID", id);

                con.Open();

                cmd.ExecuteNonQuery();
            }
        }

        // Na dúvida, procurar no site connection string qual a string de conexão do banco utilizado
        public List<TipoEventoDomain> Listar()
        {
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

                        tiposEventos.Add(tipoEvento);
                    }
                }
            }

            return tiposEventos;
        }
    }
}
