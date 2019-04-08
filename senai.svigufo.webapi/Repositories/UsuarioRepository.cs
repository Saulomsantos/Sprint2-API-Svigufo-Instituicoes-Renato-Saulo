using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using System;
using System.Data.SqlClient;

namespace senai.svigufo.webapi.Repositories
{
    /// <summary>
    /// Repositório dos usuarios
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        // Define a string de conexão
        private string StringConexao = "Data Source=.\\SqlDeveloper; initial catalog=SENAI_SVIGUFO_MANHA_BACKEND; integrated security=true;";

        /// <summary>
        /// Busca um usuário passando o email e a senha
        /// </summary>
        /// <param name="email">email do usuário</param>
        /// <param name="senha">senha do usuário</param>
        /// <returns>Retorna um usuário</returns>
        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Define a query a ser executada no banco
                string QuerySelect = "SELECT ID, NOME, EMAIL, SENHA, TIPO_USUARIO FROM USUARIOS WHERE EMAIL=@EMAIL AND SENHA=@SENHA";

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    // Passa o valor dos parâmetros
                    cmd.Parameters.AddWithValue("@EMAIL", email);
                    cmd.Parameters.AddWithValue("@SENHA", senha);

                    // Abre a conexão com o banco
                    con.Open();

                    // Executa o comando
                    SqlDataReader sdr = cmd.ExecuteReader();

                    // Caso haja linhas a serem lidas
                    if (sdr.HasRows)
                    {
                        // Cria um objeto usuario
                        UsuarioDomain usuario = new UsuarioDomain();

                        // Enquanto estiver percorrendo as linhas
                        while (sdr.Read())
                        {
                            // Atribui os valores das colunas encontrados no banco às propriedade do objeto usuario
                            usuario.Id = Convert.ToInt32(sdr["ID"]);
                            usuario.Nome = sdr["NOME"].ToString();
                            usuario.Email = sdr["EMAIL"].ToString();
                            usuario.TipoUsuario = sdr["TIPO_USUARIO"].ToString();
                        }

                        // Retorna o objeto usuario
                        return usuario;
                    }
                }

                // Caso não encontre um email e senha correspondente, retorna null
                return null;
            }

            
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Recebe um objeto usuario</param>
        public void Cadastrar(UsuarioDomain usuario)
        {
            // Define a conexão passando a string
            using (SqlConnection con = new SqlConnection(StringConexao))
            {
                // Define a string que será enviada ao banco para inserir um usuario
                string QueryCadastrar = "INSERT INTO USUARIOS(NOME, EMAIL, SENHA, TIPO_USUARIO) VALUES (@NOME, @EMAIL, @SENHA, @TIPO_USUARIO);";

                // Define o comando passando a query e a conexão
                using (SqlCommand cmd = new SqlCommand(QueryCadastrar, con))
                {
                    // Passa o valor aos parâmetros
                    cmd.Parameters.AddWithValue("@NOME", usuario.Nome);
                    cmd.Parameters.AddWithValue("@EMAIL", usuario.Email);
                    cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                    cmd.Parameters.AddWithValue("@TIPO_USUARIO", usuario.TipoUsuario);

                    // Abre a conexão com o banco
                    con.Open();

                    // Execuda o comando
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
