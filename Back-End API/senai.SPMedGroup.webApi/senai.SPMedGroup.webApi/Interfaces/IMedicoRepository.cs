using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface IMedicoRepository
    {
        /// <summary>
        /// Lista todos os médicos
        /// </summary>
        /// <returns>Uma lista de médicos</returns>
        List<Medico> Listar();

        /// <summary>
        /// Busca um médico através de seu Id
        /// </summary>
        /// <param name="id">Id do médico que será buscado</param>
        /// <returns>Um médico buscado</returns>
        Medico BuscarPorId(int id);

        /// <summary>
        /// Cadastra um novo médico
        /// </summary>
        /// <param name="novoMedico">Objeto novoMedico com as informações</param>
        void Cadastrar(Medico novoMedico);

        /// <summary>
        /// Atualiza um médico existente
        /// </summary>
        /// <param name="id">Id do médico que será atualizado</param>
        /// <param name="medicoAtualizado">Objeto medicoAtualizado com as novas informações</param>
        void Atualizar(int id, Medico medicoAtualizado);

        /// <summary>
        /// Deleta um médico existente
        /// </summary>
        /// <param name="id">Id do médico que será deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Busca um médico pelo CRM
        /// </summary>
        /// <param name="crm">Crm do médico que será buscado</param>
        /// <returns>Um médico encontrado</returns>
        Medico BuscarPorCrm(string crm);
    }
}
