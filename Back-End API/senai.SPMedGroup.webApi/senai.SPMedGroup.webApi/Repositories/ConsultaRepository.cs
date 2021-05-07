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
    public class ConsultaRepository : IConsultaRepository
    {
        SpMedContext ctx = new SpMedContext();

        /// <summary>
        /// Atualiza uma consulta existente
        /// </summary>
        /// <param name="id">Id da consulta que será atualizada</param>
        /// <param name="consultaAtualizada">Objeto consultaAtualizada com as novas informações</param>
        public void AtualizarConsulta(int id, Consulta consultaAtualizada)
        {
            Consulta consultaBuscada = ctx.Consultas.FirstOrDefault(c => c.IdConsulta == id);

            if (consultaAtualizada.IdMedico != null && ctx.Medicos.Find(consultaAtualizada.IdMedico) != null)
            {
                consultaBuscada.IdMedico = consultaAtualizada.IdMedico;
            }

            if (consultaAtualizada.IdPaciente != null && ctx.Pacientes.Find(consultaAtualizada.IdPaciente) != null)
            {
                consultaBuscada.IdPaciente = consultaAtualizada.IdPaciente;
            }

            if (consultaAtualizada.IdSituacao != null && ctx.Situacoes.Find(consultaAtualizada.IdSituacao) != null)
            {
                consultaBuscada.IdSituacao = consultaAtualizada.IdSituacao;
            }

            if (consultaAtualizada.DataConsulta != Convert.ToDateTime("0001-01-01"))
            {
                consultaBuscada.DataConsulta = consultaAtualizada.DataConsulta;
            }

            if (consultaAtualizada.HoraConsulta.ToString() != "00:00:00")
            {
                consultaBuscada.HoraConsulta = consultaAtualizada.HoraConsulta;
            }

            consultaBuscada.Descricao = consultaBuscada.Descricao;

            ctx.Consultas.Update(consultaBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Atualiza o status de uma consulta para 1 - Realizada, 2 - Agendada, 3 - Cancelada
        /// </summary>
        /// <param name="idConsulta">Id da consulta que será atualizada</param>
        /// <param name="status">Objeto que contém o Id da situação que a consulta terá</param>
        public void AtualizarSituacao(int idConsulta, int idSituacao)
        {
            Consulta consultaBuscada = ctx.Consultas.Find(idConsulta);

            switch (idSituacao)
            {
                case 1:
                    consultaBuscada.IdSituacao = 1;
                    break;

                case 2:
                    consultaBuscada.IdSituacao = 2;
                    break;

                case 3:
                    consultaBuscada.IdSituacao = 3;
                    break;

                default:
                    consultaBuscada.IdSituacao = consultaBuscada.IdSituacao;
                    break;
            }

            ctx.Consultas.Update(consultaBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca uma consulta através do Id
        /// </summary>
        /// <param name="id">Id da consulta que será buscada</param>
        /// <returns>Uma consulta encontrada</returns>
        public Consulta BuscarPorId(int id)
        {
            return ctx.Consultas
                .Include(c => c.IdMedicoNavigation)
                .Include(c => c.IdMedicoNavigation.IdEspecialidadeNavigation)
                .Select(c => new Consulta
                {
                    IdConsulta = c.IdConsulta,
                    IdMedicoNavigation = c.IdMedicoNavigation,
                    IdPacienteNavigation = c.IdPacienteNavigation,
                    IdSituacaoNavigation = c.IdSituacaoNavigation,
                    Descricao = c.Descricao,
                    DataConsulta = c.DataConsulta,
                    HoraConsulta = c.HoraConsulta
                })
                .FirstOrDefault(c => c.IdConsulta == id);
        }

        /// <summary>
        /// Cadastra uma nova consulta
        /// </summary>
        /// <param name="novaConsulta">Objeto novaConsulta com as informações</param>
        public void Cadastrar(Consulta novaConsulta)
        {
            novaConsulta.IdSituacao = 2;

            ctx.Consultas.Add(novaConsulta);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta uma consulta existente
        /// </summary>
        /// <param name="id">Id da consulta que será deletada</param>
        public void Deletar(int id)
        {
            ctx.Consultas.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        /// <summary>
        /// Insere uma descrição a uma consulta
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="descricao">Descricao da consulta</param>
        public void InserirDescricao(int id, Consulta descricao)
        {
            Consulta consultaBuscada = BuscarPorId(id);

            if (descricao.Descricao != null)
            {
                consultaBuscada.Descricao = descricao.Descricao;
            }

            ctx.Consultas.Update(consultaBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todas as consultas
        /// </summary>
        /// <returns>Uma lista de consultas</returns>
        public List<Consulta> Listar()
        {
            return ctx.Consultas
                .Include(c => c.IdMedicoNavigation)
                .Include(c => c.IdMedicoNavigation.IdEspecialidadeNavigation)
                .Select(c => new Consulta
                {
                    IdConsulta = c.IdConsulta,
                    IdMedicoNavigation = c.IdMedicoNavigation,
                    IdPacienteNavigation = c.IdPacienteNavigation,
                    IdSituacaoNavigation = c.IdSituacaoNavigation,
                    Descricao = c.Descricao,
                    DataConsulta = c.DataConsulta,
                    HoraConsulta = c.HoraConsulta
                })
                .ToList();
        }

        /// <summary>
        /// Lista consultas de acordo com o id do paciente ou médico recebido
        /// </summary>
        /// <param name="id">Id de um médico ou paciente para listar as consultas</param>
        /// <returns>Uma lista de consultas</returns>
        public List<Consulta> ListarMinhas(int id)
        {
            Paciente pacienteBuscado = ctx.Pacientes.FirstOrDefault(p => p.IdUsuario == id);

            Medico medicoBuscado = ctx.Medicos.FirstOrDefault(m => m.IdUsuario == id); 

            if (pacienteBuscado != null)
            {
                return ctx.Consultas.Where(p => p.IdPaciente == pacienteBuscado.IdPaciente).ToList();
            }

            if (medicoBuscado != null)
            {
                return ctx.Consultas.Where(c => c.IdMedico == medicoBuscado.IdMedico).ToList();
            }

            return null;
        }

    }
}
