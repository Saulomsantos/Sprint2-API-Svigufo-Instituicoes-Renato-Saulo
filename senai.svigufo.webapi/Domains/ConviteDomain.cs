using senai.svigufo.webapi.Domains.Enums;

namespace senai.svigufo.webapi.Domains
{
    /// <summary>
    /// Classe que representa a tabela convites
    /// </summary>
    public class ConviteDomain
    {
        public int Id { get; set; }

        public int EventoId { get; set; }
        public EventoDomain Evento { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioDomain Usuario { get; set; }

        public EnSituacaoConvite Situacao { get; set; }
    }
}
