using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;

namespace senai.svigufo.webapi.Repositories
{
    public class InstituicaoRepository : IInstituicaoRepository
    {
        private string StringConexao = "Data Source=.\\SqlExpress; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND; user id=sa; pwd=132";
        /// <summary>
        /// Lista todas as instituições
        /// </summary>
        /// <returns>List<TiposEventoDomain></returns>
        public List<InstituicaoDomain> Listar()
        {
            //Cria a lista Instituicoes com todas as instituicoes para serem listadas
            List<InstituicaoDomain> Instituicoes = new List<InstituicaoDomain>();

            //Cria a conexao com o banco de dados
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                //Cria a query a ser comunicada com o banco de dados
                string QueryListar = "SELECT ID, NOME_FANTASIA, RAZAO_SOCIAL, CNPJ, LOGRADOURO, CEP, UF, CIDADE FROM INSTITUICOES";

                //Abre a conexão com o banco de dados
                con.Open();

                //Percorre a lista através de um SqlDataReader
                SqlDataReader rdr;

                //Cria um comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryListar,con))
                {
                    //Executa a query QueryListar através do comando cmd
                    rdr = cmd.ExecuteReader();

                    //Percorrer os dados
                    while (rdr.Read())
                    {
                        InstituicaoDomain Instituicao = new InstituicaoDomain
                        {
                            Id = Convert.ToInt32(rdr["ID"]),
                            NomeFantasia = rdr["NOME_FANTASIA"].ToString(),
                            RazaoSocial = rdr["RAZAO_SOCIAL"].ToString(),
                            CNPJ = rdr["CNPJ"].ToString(),
                            Logradouro = rdr["LOGRADOURO"].ToString(),
                            CEP = rdr["CEP"].ToString(),
                            UF = rdr["UF"].ToString(),
                            Cidade = rdr["CIDADE"].ToString()
                        };

                        Instituicoes.Add(Instituicao);
                    }
                }

            }

            return Instituicoes;
        }

        /// <summary>
        /// Grava uma nova instituição
        /// </summary>
        /// <param name="Instituicao"></param>
        public void Gravar(InstituicaoDomain Instituicao)
        {
            // Declara a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando o valor como parâmetro
                string QueryGravar = "INSERT INTO INSTITUICOES (NOME_FANTASIA,RAZAO_SOCIAL,CNPJ,LOGRADOURO,CEP,UF,CIDADE) VALUES (@NOME_FANTASIA,@RAZAO_SOCIAL,@CNPJ,@LOGRADOURO,@CEP,@UF,@CIDADE)";

                // Declara o command passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QueryGravar, con);

                //Passa o valor do parâmetro
                cmd.Parameters.AddWithValue("@NOME_FANTASIA", Instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", Instituicao.RazaoSocial);
                cmd.Parameters.AddWithValue("@CNPJ", Instituicao.CNPJ);
                cmd.Parameters.AddWithValue("@LOGRADOURO", Instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP", Instituicao.CEP);
                cmd.Parameters.AddWithValue("@UF", Instituicao.UF);
                cmd.Parameters.AddWithValue("@CIDADE", Instituicao.Cidade);

                //Abre a conexão
                con.Open();

                //Executa o comando
                cmd.ExecuteNonQuery();
            }
        }
        
        /// <summary>
        /// Edita uma instituição
        /// </summary>
        /// <param name="id">Id da instituicao a ser alterada</param>
        /// <param name="Instituicao">Objeto instituicao com os novos dados</param>
        public void Editar(int id, InstituicaoDomain Instituicao)
        {
            // Declara a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Declara a query passando a string e a conexão
                string QueryEditar = "UPDATE INSTITUICOES SET NOME_FANTASIA = @NOME_FANTASIA," +
                    "RAZAO_SOCIAL = @RAZAO_SOCIAL," +
                    "CNPJ = @CNPJ," + 
                    "LOGRADOURO = @LOGRADOURO," +
                    "CEP = @CEP," +
                    "UF = @UF," +
                    "CIDADE = @CIDADE WHERE ID = @ID";

                // Declara o comando passando a query e a conexão
                SqlCommand cmd = new SqlCommand(QueryEditar, con);

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
}
