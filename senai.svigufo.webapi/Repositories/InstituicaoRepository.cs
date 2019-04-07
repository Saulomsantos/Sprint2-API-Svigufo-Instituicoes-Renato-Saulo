using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;

namespace senai.svigufo.webapi.Repositories
{
    /// <summary>
    /// Repositório da Instituição
    /// </summary>
    public class InstituicaoRepository : IInstituicaoRepository
    {
        // private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND; user id=sa; pwd=132";
        private string StringConexao = "Data Source=.\\SqlDeveloper; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND; integrated security=true";

        /// <summary>
        /// Lista todas as instituições
        /// </summary>
        /// <returns>Retorra uma lista de instituições</returns>
        public List<InstituicaoDomain> Listar()
        {
            // Cria a lista ListaInstituicoes com todas as instituicoes para serem listadas
            List<InstituicaoDomain> ListaInstituicoes = new List<InstituicaoDomain>();

            // Cria a conexao com o banco de dados
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Cria a query a ser comunicada com o banco de dados
                string QueryListar = "SELECT ID, NOME_FANTASIA, RAZAO_SOCIAL, CNPJ, LOGRADOURO, CEP, UF, CIDADE FROM INSTITUICOES";

                // Abre a conexão com o banco de dados
                con.Open();

                // Percorre a lista através de um SqlDataReader
                SqlDataReader sdr;

                // Cria um comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryListar, con))
                {
                    // Executa a query QueryListar através do comando cmd
                    sdr = cmd.ExecuteReader();

                    // Percorrer os dados
                    while (sdr.Read())
                    {
                        InstituicaoDomain instituicao = new InstituicaoDomain
                        {
                            Id = Convert.ToInt32(sdr["ID"]),
                            NomeFantasia = sdr["NOME_FANTASIA"].ToString(),
                            RazaoSocial = sdr["RAZAO_SOCIAL"].ToString(),
                            CNPJ = sdr["CNPJ"].ToString(),
                            Logradouro = sdr["LOGRADOURO"].ToString(),
                            CEP = sdr["CEP"].ToString(),
                            UF = sdr["UF"].ToString(),
                            Cidade = sdr["CIDADE"].ToString()
                            // É necessário converter os campos recebidos pelo BD para string mesmo
                            // que já estejam no BD como string, char ou varchar
                            // pois no C# é recebido como objeto, não como variável
                        };

                        ListaInstituicoes.Add(instituicao);
                    }
                }

            }

            return ListaInstituicoes;
        }

        /// <summary>
        /// Grava uma nova instituição
        /// </summary>
        /// <param name="Instituicao">Objeto instituicao</param>
        public void Gravar(InstituicaoDomain instituicao)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando o valor
                string QueryGravar = "INSERT INTO INSTITUICOES (NOME_FANTASIA,RAZAO_SOCIAL,CNPJ,LOGRADOURO,CEP,UF,CIDADE) VALUES (@NOME_FANTASIA,@RAZAO_SOCIAL,@CNPJ,@LOGRADOURO,@CEP,@UF,@CIDADE)";

                // Declara o command passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryGravar, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@NOME_FANTASIA", instituicao.NomeFantasia);
                    cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
                    cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);
                    cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
                    cmd.Parameters.AddWithValue("@CEP", instituicao.CEP);
                    cmd.Parameters.AddWithValue("@UF", instituicao.UF);
                    cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);

                    //Abre a conexão
                    con.Open();

                    //Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Edita uma instituição
        /// </summary>
        /// <param name="id">Id da instituicao a ser alterada</param>
        /// <param name="Instituicao">Objeto instituicao com os novos dados</param>
        public void Editar(int id, InstituicaoDomain Instituicao)
        {
            // Declara a conexão passando a string de conexão
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando o valor
                string QueryEditar = "UPDATE INSTITUICOES SET NOME_FANTASIA = @NOME_FANTASIA," +
                    "RAZAO_SOCIAL = @RAZAO_SOCIAL," +
                    "CNPJ = @CNPJ," +
                    "LOGRADOURO = @LOGRADOURO," +
                    "CEP = @CEP," +
                    "UF = @UF," +
                    "CIDADE = @CIDADE WHERE ID = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryEditar, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@NOME_FANTASIA", Instituicao.NomeFantasia);
                    cmd.Parameters.AddWithValue("@ID", Instituicao.Id);
                    cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", Instituicao.RazaoSocial);
                    cmd.Parameters.AddWithValue("@CNPJ", Instituicao.CNPJ);
                    cmd.Parameters.AddWithValue("@LOGRADOURO", Instituicao.Logradouro);
                    cmd.Parameters.AddWithValue("@CEP", Instituicao.CEP);
                    cmd.Parameters.AddWithValue("@UF", Instituicao.UF);
                    cmd.Parameters.AddWithValue("@CIDADE", Instituicao.Cidade);

                    // Abre a conexão
                    con.Open();

                    // Executa o comando 
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Deleta uma instituição
        /// </summary>
        /// <param name="id">Id da instituição a ser deletada</param>
        public void Deletar(int id)
        {
            // Declara a conexão com o banco de dados passando a string de conexao
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando o valor
                string QueryDeletar = "DELETE FROM INSTITUICOES WHERE ID = @ID";

                // Declara o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryDeletar, con))
                {
                    // Passa o valor do parâmetro
                    cmd.Parameters.AddWithValue("@ID", id);

                    // Abre a conexão com o banco de dados
                    con.Open();

                    // Executa o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Busca uma instituição pelo Id
        /// </summary>
        /// <param name="id">Id da instituição</param>
        /// <returns>Retorna uma instituição</returns>
        public InstituicaoDomain GetById(int id)
        {
            // Cria a conexao com o banco de dados
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Cria a query a ser comunicada com o banco de dados
                string QueryBuscar = "SELECT ID, NOME_FANTASIA, RAZAO_SOCIAL, CNPJ, LOGRADOURO, CEP, UF, CIDADE FROM INSTITUICOES";

                // Abre a conexão com o banco de dados
                con.Open();

                // Percorre a lista através de um SqlDataReader
                SqlDataReader sdr;

                // Cria um comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryBuscar, con))
                {
                    // Executa a query QueryListar através do comando cmd
                    sdr = cmd.ExecuteReader();                    

                    if (sdr.HasRows)
                    {
                        // Percorrer os dados
                        while (sdr.Read())
                        {
                            InstituicaoDomain instituicao = new InstituicaoDomain
                            {
                                Id = Convert.ToInt32(sdr["ID"]),
                                NomeFantasia = sdr["NOME_FANTASIA"].ToString(),
                                RazaoSocial = sdr["RAZAO_SOCIAL"].ToString(),
                                CNPJ = sdr["CNPJ"].ToString(),
                                Logradouro = sdr["LOGRADOURO"].ToString(),
                                CEP = sdr["CEP"].ToString(),
                                UF = sdr["UF"].ToString(),
                                Cidade = sdr["CIDADE"].ToString()
                                // É necessário converter os campos recebidos pelo BD para string mesmo
                                // que já estejam no BD como string, char ou varchar
                                // pois no C# é recebido como objeto, não como variável
                            };

                            return instituicao;
                        }
                    }

                    return null;
                }
            }
        }
    }
}
