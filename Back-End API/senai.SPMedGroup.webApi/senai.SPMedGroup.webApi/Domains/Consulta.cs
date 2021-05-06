using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class Consulta
    {
        public int IdConsulta { get; set; }

        [Required(ErrorMessage = "Informe o médico que fará a consulta!")]
        public int? IdMedico { get; set; }

        [Required(ErrorMessage = "Informe o paciente que fará a consulta!")]
        public int? IdPaciente { get; set; }
        public int? IdSituacao { get; set; }

        [Required(ErrorMessage = "Informe a data da consulta!")]
        public DateTime DataConsulta { get; set; }

        [Required(ErrorMessage = "Informe a hora da consulta!")]
        public TimeSpan HoraConsulta { get; set; }

        public string Descricao { get; set; }

        public virtual Medico IdMedicoNavigation { get; set; }
        public virtual Paciente IdPacienteNavigation { get; set; }
        public virtual Situaco IdSituacaoNavigation { get; set; }
    }
}
