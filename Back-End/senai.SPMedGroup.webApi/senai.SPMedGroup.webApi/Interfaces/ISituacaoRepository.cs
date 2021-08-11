using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface ISituacaoRepository
    {
        /// <summary>
        /// Lista todas as situações
        /// </summary>
        /// <returns>Uma lista de situações</returns>
        List<Situaco> Listar();

        /// <summary>
        /// Busca uma situação através do Id
        /// </summary>
        /// <param name="id">Id da situação que será buscada</param>
        /// <returns>Uma situação encontrada</returns>
        Situaco BuscarPorId(int id);

        /// <summary>
        /// Cadastra uma nova situação
        /// </summary>
        /// <param name="novaSituacao">Objeto novaSituacao com as informações</param>
        void Cadastrar(Situaco novaSituacao);

        /// <summary>
        /// Atualiza uma situacao existente
        /// </summary>
        /// <param name="id">Id da situação que será atualizada</param>
        /// <param name="situacaoAtualizada">Objeto situacaoAtualizada com as novas informações</param>
        void Atualizar(int id, Situaco situacaoAtualizada);

        /// <summary>
        /// Deleta uma situação existente
        /// </summary>
        /// <param name="id">Id da situação que será deletada</param>
        void Deletar(int id);
    }
}
