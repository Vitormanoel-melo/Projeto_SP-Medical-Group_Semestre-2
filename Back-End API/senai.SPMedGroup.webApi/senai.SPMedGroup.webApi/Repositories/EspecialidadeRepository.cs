using senai.SPMedGroup.webApi.Contexts;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        SpMedContext ctx = new SpMedContext();

        /// <summary>
        /// Atualiza uma especialidade existente
        /// </summary>
        /// <param name="id">Id da especialidade que será atualizada</param>
        /// <param name="especialidadeAtualizada">Objeto com as novas informações</param>
        public void Atualizar(int id, Especialidade especialidadeAtualizada)
        {
            Especialidade especialidadeBuscada = BuscarPorId(id);

            if (especialidadeAtualizada.Descricao != null)
            {
                especialidadeBuscada.Descricao = especialidadeAtualizada.Descricao; 
            }

            ctx.Especialidades.Update(especialidadeBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca uma especialidade através de seu id
        /// </summary>
        /// <param name="id">Id da especialidade que será buscada</param>
        /// <returns>Um especialidade buscada</returns>
        public Especialidade BuscarPorId(int id)
        {
            return ctx.Especialidades.FirstOrDefault(e => e.IdEspecialidade == id);
        }

        /// <summary>
        /// Cadastra uma nova especialidade
        /// </summary>
        /// <param name="novaEspecialidade">Objeto novaEspecialidade com as informações para cadastro</param>
        public void Cadastrar(Especialidade novaEspecialidade)
        {
            ctx.Especialidades.Add(novaEspecialidade);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta uma especialidade existente
        /// </summary>
        /// <param name="id">Id da especialidade que será deletada</param>
        public void Deletar(int id)
        {
            ctx.Especialidades.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todas as especialidades
        /// </summary>
        /// <returns>Uma lista de especialidades</returns>
        public List<Especialidade> Listar()
        {
            return ctx.Especialidades.ToList();
        }
    }
}
