using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.ViewModels
{
    public class UsuarioViewModel
    {
        public string Email { get; set; }

        [StringLength(30, MinimumLength = 5, ErrorMessage = "A senha deve conter de 5 a 30 caracteres!")]
        public string Senha { get; set; }

        public int idTipoUsuario { get; set; }
    }
}
