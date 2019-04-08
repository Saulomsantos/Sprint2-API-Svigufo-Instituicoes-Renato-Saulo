using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using senai.svigufo.webapi.Repositories;
using senai.svigufo.webapi.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Senai.SviGufo.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints referentes ao Login
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] // Implementa funcionalidades ao controller
    public class LoginController : ControllerBase
    {
        // Define um objeto UsuarioRepository para chamada dos métodos
        private IUsuarioRepository UsuarioRepository { get; set; }

        public LoginController()
        {
            // Cria uma instância de UsuarioRepository
            UsuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Faz a autenticação de um usuário
        /// </summary>
        /// <param name="login">Recebe um objeto login</param>
        /// <returns>Retorna um status code e um token em caso de sucesso</returns>
        [HttpPost]
        public IActionResult Post(LoginViewModel login)
        {
            try // Tenta autenticar
            {
                // Busca o usuário pelo email e senha
                UsuarioDomain usuarioBuscado = UsuarioRepository.BuscarPorEmailSenha(login.Email, login.Senha);

                // Verifica se o usuário não foi encontrado
                if (usuarioBuscado == null)
                {
                    // Neste caso retorna um status code 404 Not Found
                    return NotFound(new
                    {
                        mensagem = "Email ou senha inválido"
                    });
                }

                // Define os dados que serão fornecidos no token - PayLoad
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.TipoUsuario),
                    new Claim("teste", "laranja")
                };

                // Chave de acesso do token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("svigufo-chave-autenticacao"));

                // Credenciais do Token - Header
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Gera o token
                var token = new JwtSecurityToken(
                    issuer: "SviGufo.WebApi",
                    audience: "SviGufo.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );

                // Retorna Ok com o Token
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex) // Caso dê erro
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }
    }
}