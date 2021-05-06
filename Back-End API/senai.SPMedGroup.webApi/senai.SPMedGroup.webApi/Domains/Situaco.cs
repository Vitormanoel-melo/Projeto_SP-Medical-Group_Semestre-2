using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class Situaco
    {
        public Situaco()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int IdSituacao { get; set; }

        [Required(ErrorMessage = "Informe a descrição da situação!")]
        public string Descricao { get; set; }

        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
