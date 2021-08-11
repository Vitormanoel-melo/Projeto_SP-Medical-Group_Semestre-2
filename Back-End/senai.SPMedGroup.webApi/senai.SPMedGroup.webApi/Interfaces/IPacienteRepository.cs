using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface IPacienteRepository
    {
        /// <summary>
        /// Lista todos os pacientes
        /// </summary>
        /// <returns>Uma lista de pacientes</returns>
        List<Paciente> Listar();

        /// <summary>
        /// Busca um paciente através de seu id
        /// </summary>
        /// <param name="id">Id do paciente que será buscado</param>
        /// <returns>Um paciente encontrado</returns>
        Paciente BuscarPorId(int id);

        /// <summary>
        /// Busca um paciente através de seu rg e seu cpf
        /// </summary>
        /// <param name="rg">Rg do paciente que será buscado</param>
        /// <param name="cpf">Cpf do paciente que será buscado</param>
        /// <returns>0 - Paciente com o RG encontrado, 1 - Paciente com o CPF encontrado, 2 - nenhum paciente encontrado</returns>
        string BuscarPorRgCpf(string rg, string cpf);

        /// <summary>
        /// Cadastra um novo paciente
        /// </summary>
        /// <param name="novoPaciente">Objeto novoPaciente com as informações para cadastro</param>
        void Cadastrar(Paciente novoPaciente);

        /// <summary>
        /// Atualiza um paciente existente
        /// </summary>
        /// <param name="id">Id do paciente que será atualizado</param>
        /// <param name="pacienteAtualizado">Objeto pacienteAtualizado com as novas informações</param>
        void Atualizar(int id, Paciente pacienteAtualizado);

        /// <summary>
        /// Deleta um paciente existente
        /// </summary>
        /// <param name="id">Id do paciente que será deletado</param>
        void Deletar(int id);
    }
}
