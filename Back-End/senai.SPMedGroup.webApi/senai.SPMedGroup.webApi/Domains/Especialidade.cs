using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class Especialidade
    {
        public Especialidade()
        {
            Medicos = new HashSet<Medico>();
        }

        public int IdEspecialidade { get; set; }

        [Required(ErrorMessage = "Informe a descrição da especialidade")]
        public string Descricao { get; set; }

        public virtual ICollection<Medico> Medicos { get; set; }
    }
}
