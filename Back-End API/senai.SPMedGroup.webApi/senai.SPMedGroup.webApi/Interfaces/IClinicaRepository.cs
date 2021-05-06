using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface IClinicaRepository
    {
        /// <summary>
        /// Lista todas as clinicas
        /// </summary>
        /// <returns>Uma lista de clínicas</returns>
        List<Clinica> Listar();

        /// <summary>
        /// Busca uma clinica através de seu id
        /// </summary>
        /// <param name="id">Id da clínica que será buscada</param>
        /// <returns>Um clínica buscada</returns>
        Clinica BuscarPorId(int id);

        /// <summary>
        /// Cadastra uma nova clínica
        /// </summary>
        /// <param name="novaClinica">Objeto novaClinica com as informações para cadastro</param>
        void Cadastrar(Clinica novaClinica);

        /// <summary>
        /// Atualiza uma clínica existente
        /// </summary>
        /// <param name="id">Id da clínica que será atualizada</param>
        /// <param name="tipoUsuarioAtualizado">Objeto com as novas informações</param>
        void Atualizar(int id, Clinica clinicaAtualizada);

        /// <summary>
        /// Deleta uma clínica existente
        /// </summary>
        /// <param name="id">Id da clínica que será deletada</param>
        void Deletar(int id);
    }
}
