using senai.SPMedGroup.webApi.Contexts;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Repositories
{
    public class SituacaoRepository : ISituacaoRepository
    {
        SpMedContext ctx = new SpMedContext();

        /// <summary>
        /// Atualiza uma situacao existente
        /// </summary>
        /// <param name="id">Id da situação que será atualizada</param>
        /// <param name="situacaoAtualizada">Objeto situacaoAtualizada com as novas informações</param>
        public void Atualizar(int id, Situaco situacaoAtualizada)
        {
            Situaco situacaoBuscada = BuscarPorId(id);

            if (situacaoAtualizada.Descricao != null)
            {
                situacaoBuscada.Descricao = situacaoAtualizada.Descricao;
            }

            ctx.Situacoes.Update(situacaoBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca uma situação através do Id
        /// </summary>
        /// <param name="id">Id da situação que será buscada</param>
        /// <returns>Uma situação encontrada</returns>
        public Situaco BuscarPorId(int id)
        {
            return ctx.Situacoes.FirstOrDefault(s => s.IdSituacao == id);
        }

        /// <summary>
        /// Cadastra uma nova situação
        /// </summary>
        /// <param name="novaSituacao">Objeto novaSituacao com as informações</param>
        public void Cadastrar(Situaco novaSituacao)
        {
            ctx.Situacoes.Add(novaSituacao);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta uma situação existente
        /// </summary>
        /// <param name="id">Id da situação que será deletada</param>
        public void Deletar(int id)
        {
            ctx.Situacoes.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todas as situações
        /// </summary>
        /// <returns>Uma lista de situações</returns>
        public List<Situaco> Listar()
        {
            return ctx.Situacoes.ToList();
        }
    }
}
