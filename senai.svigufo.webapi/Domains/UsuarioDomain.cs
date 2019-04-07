using System.ComponentModel.DataAnnotations;

namespace senai.svigufo.webapi.Domains
{
    /// <summary>
    /// Classe responsável pela tabela Usuarios
    /// </summary>
    public class UsuarioDomain
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe o tipo do usuário")]
        public string TipoUsuario { get; set; }
    }
}
