using System;
using System.ComponentModel.DataAnnotations;


namespace WebMVC.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required] 
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "RG")]
        public string RG { get; set; }

        [Display(Name = "Data de Expedição")]
        public DateTime? DataExpedicao { get; set; }

        [Display(Name = "Orgão de Expedição")]
        public string OrgaoExpedicao { get; set; }

        [Display(Name = "UF de Expedição")]
        public string UF { get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Required]
        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }
        public Endereco Endereco { get; set; }
    }
}
