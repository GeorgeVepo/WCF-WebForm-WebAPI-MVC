using System;
using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class Endereco
    {
        [Key]
        public int EnderecoId { get; set; }

        [Required]
        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Required]
        [Display(Name = "Rua")]
        public string Logradouro { get; set; }

        [Required]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Required]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "UF")]
        public string UF { get; set; }
        public Cliente Cliente { get; set; }

    }
}