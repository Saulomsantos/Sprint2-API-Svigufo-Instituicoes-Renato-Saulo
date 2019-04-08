using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Domains.Enums;
using senai.svigufo.webapi.Interfaces;
using Senai.SviGufo.WebApi.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Senai.SviGufo.WebApi.Controllers
{
    /// <summary>
    /// Classe responsável pelos endpoints referentes aos convites
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] // Implementa funcionalidades ao Controller
    public class ConvitesController : ControllerBase
    {
        // Define um objeto ConviteRepository para chamada dos métodos
        private IConviteRepository ConviteRepository { get; set; }

        public ConvitesController()
        {
            // Cria uma instancia de ConviteRepository
            ConviteRepository = new ConviteRepository();
        }

        /// <summary>
        /// Lista todos os convites
        /// </summary>
        /// <returns>Retorna uma lista de Convites</returns>
        [Authorize(Roles = "ADMINISTRADOR")] // Define que somente o administrador tenha acesso a esse endpoint
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try // Tenta listar
            {
                // Retorna um status code 200 e a lista de todos os convites
                return Ok(ConviteRepository.Listar());
            }
            catch (System.Exception ex) // Caso dê erro
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }

        /// <summary>
        /// Lista os convites de um determinado usuário
        /// </summary>
        /// <returns>Retorna uma lista de convites de um usuário</returns>
        [Authorize] // Define que somente usuários autenticados tenham acesso a esse endpoint
        [HttpGet]
        [Route("meus")] // url/meus
        public IActionResult ListarMeusConvites()
        {
            try // Tenta listar
            {
                // Define o id do usuário pegando do token gerado na autenticação
                int usuarioid = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                // string teste = HttpContext.User.Claims.First(c => c.Type == "teste").Value;

                // Retorna um status code 200 e a lista dos convites deste usuário passando no método o id atribuído anteriormente
                return Ok(ConviteRepository.ListarMeusConvites(usuarioid));
            }
            catch (System.Exception ex) // Caso dê erro
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }

        /// <summary>
        /// Cadastra um usuário em um evento
        /// </summary>
        /// <param name="eventoid">Id do evento que o usuário será inscrito passado na URL</param>
        /// <returns></returns>
        [Authorize] // Define que somente usuários autenticados tenham acesso a esse endpoint
        [HttpPost("inscricao/{eventoid}")] // url/inscricao/IdDoEvento
        public IActionResult Inscricao(int eventoid)
        {
            try // Tenta inscrever
            {
                // Define um convite
                ConviteDomain convite = new ConviteDomain();
                // Atribui ao EventoId do convite o id passado na URL
                convite.EventoId = eventoid;
                // Define o UsuarioId do convite o id armazenado no token gerado na autenticação
                convite.UsuarioId = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
                // Define que a situação do convite seja automaticamente AGUARDANDO
                convite.Situacao = EnSituacaoConvite.AGUARDANDO;

                // Cadastra este convite
                ConviteRepository.Cadastrar(convite);

                // Retorna um status code 200
                return Ok();
            }
            catch (Exception ex) // Caso dê erro
            {
                // Retorna um status code 400 Bad Request
                return BadRequest();
            }
        }

        /// <summary>
        /// Convida outro usuário a um determinado evento
        /// </summary>
        /// <param name="convite">Recebe um objeto convite</param>
        /// <returns>Retorna um status code</returns>
        [Authorize] // Define que somente usuários autenticados tenham acesso a esse endpoint
        [HttpPost("convidar")] // url/convidar
        public IActionResult Convidar(ConviteDomain convite)
        {
            try // Tenta convidar
            {
                // Faz a chamada para o método cadastrar um convite passando o id do convidado, o id do evento e a situação
                // Recebidos na requisição (convite)
                ConviteRepository.Cadastrar(convite);
                
                // Retorna um status code 200
                return Ok();
            }
            catch (Exception ex) // Caso dê erro
            {
                // Retorna um status code 400 - Bad Request
                return BadRequest();
            }
        }

    }
}