using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.ViewModels
{
    public class ClinicaViewModel
    {
        public int IdClinica { get; set; }
        public string NomeClinica { get; set; }
        public string DataAbertura { get; set; }
        public TimeSpan HorarioAbertura { get; set; }
        public TimeSpan? HorarioFechamento { get; set; }

        [StringLength(14, MinimumLength = 14, ErrorMessage = "Um CPNJ tem que ter 14 caracteres!")]
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string Endereco { get; set; }
    }
}
