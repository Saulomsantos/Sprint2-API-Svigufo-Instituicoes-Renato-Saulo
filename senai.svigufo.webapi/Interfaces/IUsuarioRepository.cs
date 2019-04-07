using senai.svigufo.webapi.Domains;

namespace senai.svigufo.webapi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório Usuario
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Recebe um objeto usuario do tipo UsuarioDomain</param>
        void Cadastrar(UsuarioDomain usuario);

        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="email">email do usuário</param>
        /// <param name="senha">senha do usuário</param>
        /// <returns>Retorna um usuário</returns>
        UsuarioDomain BuscarPorEmailSenha(string email, string senha);
    }
}
