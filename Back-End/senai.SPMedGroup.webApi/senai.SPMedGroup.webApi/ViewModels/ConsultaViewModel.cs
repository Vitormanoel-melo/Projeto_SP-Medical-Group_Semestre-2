using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.ViewModels
{
    public class ConsultaViewModel
    {
        public int IdConsulta { get; set; }
        public int IdMedico { get; set; }
        public int IdPaciente { get; set; }
        public int IdSituacao { get; set; }
        public DateTime DataConsulta { get; set; }
        public TimeSpan HoraConsulta { get; set; }

        public string Descricao { get; set; }
    }
}
