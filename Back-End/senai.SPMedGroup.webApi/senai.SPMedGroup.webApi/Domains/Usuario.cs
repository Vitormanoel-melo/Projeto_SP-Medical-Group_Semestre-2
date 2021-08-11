using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Medicos = new HashSet<Medico>();
            Pacientes = new HashSet<Paciente>();
        }

        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "Informe o tipo do usuário!")]
        public int? IdTipoUsuario { get; set; }

        [Required(ErrorMessage = "O e-mail do usuário é obrigatório!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha do usuário é obrigatória!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "A senha deve conter de 5 a 30 caracteres!")]
        public string Senha { get; set; }

        public virtual TiposUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<Medico> Medicos { get; set; }
        public virtual ICollection<Paciente> Pacientes { get; set; }
    }
}
