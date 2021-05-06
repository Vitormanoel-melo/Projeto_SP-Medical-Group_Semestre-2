using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class Clinica
    {
        public Clinica()
        {
            Medicos = new HashSet<Medico>();
        }

        public int IdClinica { get; set; }

        [Required(ErrorMessage = "Infome o nome da clínica")]
        public string NomeClinica { get; set; }

        [Required(ErrorMessage = "Informe a data de abertura. EX: seg à seg")]
        public string DataAbertura { get; set; }

        [Required(ErrorMessage = "Infome o horário de abertura da clínica!")]
        public TimeSpan HorarioAbertura { get; set; }

        [Required(ErrorMessage = "Infome o horário de fechamento da clínica!")]
        public TimeSpan? HorarioFechamento { get; set; }

        [Required(ErrorMessage = "Infome o CNPJ da clínica")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Um CPNJ tem que ter 14 caracteres!")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Infome a razão social da clínica")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "Infome o endereço da clínica")]
        public string Endereco { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
