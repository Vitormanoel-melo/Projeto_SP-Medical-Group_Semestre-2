using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int IdMedico { get; set; }

        [Required(ErrorMessage = "Informe a especialidade do médico!")]
        public int? IdEspecialidade { get; set; }

        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "Informe a clínica do médico!")]
        public int? IdClinica { get; set; }

        [Required(ErrorMessage = "Informe o nome do médico!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CRM do médico!")]
        [StringLength(7, MinimumLength = 7, ErrorMessage = "O CRM deve conter 7 caracteres!")]
        public string Crm { get; set; }

        public virtual Clinica IdClinicaNavigation { get; set; }
        public virtual Especialidade IdEspecialidadeNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
