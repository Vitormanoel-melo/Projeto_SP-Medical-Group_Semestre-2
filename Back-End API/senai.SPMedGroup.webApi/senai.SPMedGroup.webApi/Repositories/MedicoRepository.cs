using Microsoft.EntityFrameworkCore;
using senai.SPMedGroup.webApi.Contexts;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        SpMedContext ctx = new SpMedContext();


        /// <summary>
        /// Atualiza um médico existente
        /// </summary>
        /// <param name="id">Id do médico que será atualizado</param>
        /// <param name="medicoAtualizado">Objeto medicoAtualizado com as novas informações</param>
        public void Atualizar(int id, Medico medicoAtualizado)
        {
            Medico medicoBuscado = ctx.Medicos.FirstOrDefault(m => m.IdMedico == id);

            if (medicoAtualizado.IdEspecialidade != null && ctx.Especialidades.Find(medicoAtualizado.IdEspecialidade) != null)
            {
                medicoBuscado.IdEspecialidade = medicoAtualizado.IdEspecialidade;
            }

            if (medicoAtualizado.IdUsuario != null && ctx.Usuarios.Find(medicoAtualizado.IdUsuario) != null)
            {
                medicoBuscado.IdUsuario = medicoAtualizado.IdUsuario;
            }

            if (medicoAtualizado.IdClinica != null && ctx.Clinicas.Find(medicoAtualizado.IdClinica) != null)
            {
                medicoBuscado.IdClinica = medicoAtualizado.IdClinica;
            }

            if (medicoAtualizado.Nome != null)
            {
                medicoBuscado.Nome = medicoAtualizado.Nome;
            }

            if (medicoAtualizado.Crm != null && BuscarPorCrm(medicoAtualizado.Crm) == null)
            {
                medicoBuscado.Crm = medicoAtualizado.Crm;
            }

            ctx.Medicos.Update(medicoBuscado);

            ctx.SaveChanges();
        }

        public Medico BuscarPorCrm(string crm)
        {
            return ctx.Medicos.FirstOrDefault(m => m.Crm == crm);
        }

        /// <summary>
        /// Busca um médico através de seu Id
        /// </summary>
        /// <param name="id">Id do médico que será buscado</param>
        /// <returns>Um médico buscado</returns>
        public Medico BuscarPorId(int id)
        {
            return ctx.Medicos.Select(m => new Medico
            {
                IdMedico = m.IdMedico,
                IdClinica = m.IdClinica,
                IdEspecialidade = m.IdEspecialidade,
                IdUsuario = m.IdUsuario,
                IdEspecialidadeNavigation = m.IdEspecialidadeNavigation,
                Nome = m.Nome,
                Crm = m.Crm
            }).FirstOrDefault(m => m.IdMedico == id);
        }

        /// <summary>
        /// Cadastra um novo médico
        /// </summary>
        /// <param name="novoMedico">Objeto novoMedico com as informações</param>
        public void Cadastrar(Medico novoMedico)
        {
            ctx.Medicos.Add(novoMedico);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um médico existente
        /// </summary>
        /// <param name="id">Id do médico que será deletado</param>
        public void Deletar(int id)
        {
            ctx.Medicos.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todos os médicos
        /// </summary>
        /// <returns>Uma lista de médicos</returns>
        public List<Medico> Listar()
        {
            return ctx.Medicos.Select(m => new Medico 
            { 
                IdMedico = m.IdMedico,
                IdClinica = m.IdClinica,
                IdEspecialidade = m.IdEspecialidade,
                IdUsuario = m.IdUsuario,
                IdEspecialidadeNavigation = m.IdEspecialidadeNavigation,
                Nome = m.Nome,
                Crm = m.Crm
            }).ToList();
        }
    }
}
