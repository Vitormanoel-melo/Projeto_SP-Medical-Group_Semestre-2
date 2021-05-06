using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface IEspecialidadeRepository
    {
        /// <summary>
        /// Lista todas as especialidades
        /// </summary>
        /// <returns>Uma lista de especialidades</returns>
        List<Especialidade> Listar();

        /// <summary>
        /// Busca uma especialidade através de seu id
        /// </summary>
        /// <param name="id">Id da especialidade que será buscada</param>
        /// <returns>Um especialidade buscada</returns>
        Especialidade BuscarPorId(int id);

        /// <summary>
        /// Cadastra uma nova especialidade
        /// </summary>
        /// <param name="novaEspecialidade">Objeto novaEspecialidade com as informações para cadastro</param>
        void Cadastrar(Especialidade novaEspecialidade);

        /// <summary>
        /// Atualiza uma especialidade existente
        /// </summary>
        /// <param name="id">Id da especialidade que será atualizada</param>
        /// <param name="especialidadeAtualizada">Objeto com as novas informações</param>
        void Atualizar(int id, Especialidade especialidadeAtualizada);

        /// <summary>
        /// Deleta uma especialidade existente
        /// </summary>
        /// <param name="id">Id da especialidade que será deletada</param>
        void Deletar(int id);
    }
}
