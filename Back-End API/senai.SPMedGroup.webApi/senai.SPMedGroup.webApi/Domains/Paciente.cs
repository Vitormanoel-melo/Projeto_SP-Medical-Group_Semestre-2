using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class Paciente
    {
        public Paciente()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int IdPaciente { get; set; }
        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "Informe o nome do paciente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o RG do paciente")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Um RG deve conter 9 caracteres!")]
        public string Rg { get; set; }

        [Required(ErrorMessage = "Informe o CPF do paciente")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Um CPF deve conter 11 caracteres!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o telefone do paciente")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O número de telefone deve conter 11 dígitos. EX: 11912345678")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento do paciente")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Informe o endereço do paciente")]
        public string Endereco { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
