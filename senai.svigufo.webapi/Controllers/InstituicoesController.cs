using Microsoft.AspNetCore.Mvc;
using senai.svigufo.webapi.Domains;
using senai.svigufo.webapi.Interfaces;
using System.Collections.Generic;
using senai.svigufo.webapi.Repositories;

namespace senai.svigufo.webapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicoesController : ControllerBase
    {
        private IInstituicaoRepository InstituicaoRepository { get; set; }

        public InstituicoesController()
        {
            InstituicaoRepository = new InstituicaoRepository();
        }

        [HttpGet]
        public IEnumerable<InstituicaoDomain> Get()
        {
            return InstituicaoRepository.Listar();
        }

        [HttpPost] //Verbo para gravar
        public IActionResult Post(InstituicaoDomain InstituicaoRecebida)
        {
            //Chama o método para gravar passando a InstituicaoRecebida
            InstituicaoRepository.Gravar(InstituicaoRecebida);

            //Retorna Ok e a lista com as instituições
            return Ok();
        }

        [HttpPut("{id}")] //Verbo para editar
        public IActionResult Put(int id, InstituicaoDomain InstituicaoRecebida)
        {
            InstituicaoRecebida.Id = id;
            //Chama o método para editar passando a InstituicaoRecebida
            //InstituicaoRepository.Editar(InstituicaoRecebida);
            InstituicaoRepository.Editar(id, InstituicaoRecebida);

            return Ok();
        }
    }
}