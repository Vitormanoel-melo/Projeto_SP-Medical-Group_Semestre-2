using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace senai.SPMedGroup.webApi.Domains
{
    public partial class TiposUsuario
    {
        public TiposUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdTipoUsuario { get; set; }

        [Required(ErrorMessage = "O nome do tipo de usuário é obrigatório")]
        public string NomeTipo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
