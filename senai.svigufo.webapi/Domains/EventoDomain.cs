using System;
using System.ComponentModel.DataAnnotations;

namespace senai.svigufo.webapi.Domains
{
    /// <summary>
    /// Classe que faz referência à tabela Eventos
    /// </summary>
    public class EventoDomain
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o título do evento")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Informe a descrição do evento")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe a data do evento")]
        [DataType(DataType.Date)]
        public DateTime DataEvento { get; set; }

        [Required(ErrorMessage = "Informe se o acesso ao evento é livre")]
        public bool AcessoLivre { get; set; }

        [Required(ErrorMessage = "Informe o Id do Tipo de Evento")]
        public int TipoEventoId { get; set; }
        public TipoEventoDomain TipoEvento { get; set; }

        [Required(ErrorMessage = "Informe o Id da insituição")]
        public int InstituicaoId { get; set; }
        public InstituicaoDomain Instituicao { get; set; }
    }
}
