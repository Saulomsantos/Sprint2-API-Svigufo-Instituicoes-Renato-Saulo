using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using senai.svigufo.webapi.Repositories;

namespace senai.svigufo.webapi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes aos Usuários
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] // Implementa funcionalidades ao controller
    public class UsuariosController : ControllerBase
    {
        // Define um objeto UsuarioRepository para chamada dos métodos
        public IUsuarioRepository UsuarioRepository { get; set; }

        public UsuariosController()
        {
            // Cria uma instância de UsuarioRepository
            UsuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Recebe um objeto usuario</param>
        /// <returns>Retorna um Status Code</returns>
        [HttpPost]
        public IActionResult Post(UsuarioDomain usuario)
        {
            try // Tenta cadastrar
            {
                // Chama o repositorio para efetuar o cadastro do usuário passando o objeto da requisição
                UsuarioRepository.Cadastrar(usuario);

                //Retorna um status code 200 informando que o usuário foi cadastrado
                return Ok(new
                {
                    mensagem = "Usuário Cadastrado"
                });
            }
            catch // Caso dê erro
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }
    }
}