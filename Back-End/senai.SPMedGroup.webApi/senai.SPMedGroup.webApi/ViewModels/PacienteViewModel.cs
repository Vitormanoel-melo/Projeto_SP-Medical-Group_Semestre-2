using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.ViewModels
{
    public class PacienteViewModel
    {
        public int? idUsuario { get; set; }
        public string Nome { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "Um RG deve conter 9 caracteres!")]
        public string Rg { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "Um CPF deve conter 11 caracteres!")]
        public string Cpf { get; set; }

        [StringLength(11, MinimumLength = 11, ErrorMessage = "O número de telefone deve conter 11 dígitos. EX: 11912345678")]
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
    }
}
