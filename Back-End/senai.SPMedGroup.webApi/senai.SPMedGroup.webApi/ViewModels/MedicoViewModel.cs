using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.ViewModels
{
    public class MedicoViewModel
    {
        public int? IdEspecialidade { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdClinica { get; set; }
        public string Nome { get; set; }

        [StringLength(7, MinimumLength = 7, ErrorMessage = "O CRM deve conter 7 caracteres!")]
        public string Crm { get; set; }
    }
}
