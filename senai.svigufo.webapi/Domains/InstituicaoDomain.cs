using System.ComponentModel.DataAnnotations;

namespace senai.svigufo.webapi.Domains
{
    /// <summary>
    /// Classe que representa a tabela Instituições no banco de dados
    /// </summary>
    public class InstituicaoDomain
    {
        public int Id { get; set; }

        public string NomeFantasia { get; set; }

        // Data Annotation - Faz a validação no domínio
        // Required - Exige que o dado seja preenchido
        [Required(ErrorMessage = "Informe a Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage ="Informe o CNPJ")]
        public string CNPJ { get; set; }

        public string Logradouro { get; set; }

        public string CEP { get; set; }

        // Define a quantidade de caracteres do UF
        [StringLength(2, MinimumLength = 2, ErrorMessage ="O campo UF necessita ter 2 caracteres")]
        public string UF { get; set; }

        public string Cidade { get; set; }
    }
}
