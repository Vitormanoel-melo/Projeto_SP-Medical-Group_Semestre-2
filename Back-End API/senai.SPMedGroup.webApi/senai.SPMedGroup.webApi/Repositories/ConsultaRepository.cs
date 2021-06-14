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
        /// <param name="idSituacao">Id da situação que a consulta terá</param>
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
        /// <param name="idUsuario">Id de usuário do médico que tentou inserir a descrição</param>
        public void InserirDescricao(int id, Consulta descricao, int idUsuario)
        {
            Consulta consultaBuscada = ctx.Consultas.FirstOrDefault(c => c.IdConsulta == id);

            Medico medicoBuscado = ctx.Medicos.FirstOrDefault(m => m.IdUsuario == idUsuario);

            if (descricao.Descricao != null && consultaBuscada.IdMedico == medicoBuscado.IdMedico)
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
        public List<object> ListarMinhas(int id)
        {
            Paciente pacienteBuscado = ctx.Pacientes.FirstOrDefault(p => p.IdUsuario == id);

            Medico medicoBuscado = ctx.Medicos.FirstOrDefault(m => m.IdUsuario == id); 

            if (pacienteBuscado != null)
            {
                //return ctx.Consultas.Where(p => p.IdPaciente == pacienteBuscado.IdPaciente)
                    //.Include(m => m.IdMedicoNavigation)
                    //.Include(m => m.IdMedicoNavigation.IdEspecialidadeNavigation)
                    //.Select(c => new Consulta
                    //{
                    //    IdConsulta = c.IdConsulta,
                    //    IdPaciente = c.IdPaciente,
                    //    IdPacienteNavigation = c.IdPacienteNavigation,
                    //    IdMedico = c.IdMedico,
                    //    IdMedicoNavigation = c.IdMedicoNavigation,
                    //    IdSituacaoNavigation = c.IdSituacaoNavigation,
                    //    DataConsulta = c.DataConsulta,
                    //    HoraConsulta = c.HoraConsulta,
                    //    Descricao = c.Descricao
                    //}).ToList();

                List<Consulta> ListaConsulta = ctx.Consultas.Where(p => p.IdPaciente == pacienteBuscado.IdPaciente)
                    .Include(m => m.IdMedicoNavigation)
                    .Include(m => m.IdMedicoNavigation.IdEspecialidadeNavigation)
                    .Select(c => new Consulta
                    {
                        IdConsulta = c.IdConsulta,
                        IdPaciente = c.IdPaciente,
                        IdPacienteNavigation = c.IdPacienteNavigation,
                        IdMedico = c.IdMedico,
                        IdMedicoNavigation = c.IdMedicoNavigation,
                        IdSituacaoNavigation = c.IdSituacaoNavigation,
                        DataConsulta = c.DataConsulta,
                        HoraConsulta = c.HoraConsulta,
                        Descricao = c.Descricao
                    }).ToList();

                List<object> listaConsultasPaciente = new List<object>();

                foreach (var item in ListaConsulta)
                {
                    object newItem = new 
                    { 
                        idConsulta = item.IdConsulta,
                        nomePaciente = item.IdPacienteNavigation.Nome,
                        cpf = item.IdPacienteNavigation.Cpf,
                        dataNascimento = item.IdPacienteNavigation.DataNascimento,
                        nomeMedico = item.IdMedicoNavigation.Nome,
                        especialidade = item.IdMedicoNavigation.IdEspecialidadeNavigation.Descricao,
                        dataConsulta = item.DataConsulta,
                        horaConsulta = item.HoraConsulta,
                        descricao = item.Descricao,
                        situacao = item.IdSituacaoNavigation.Descricao
                    };

                    listaConsultasPaciente.Add(newItem);
                }

                return listaConsultasPaciente;
            }

            if (medicoBuscado != null)
            {
                List<Consulta> listaConsultas = ctx.Consultas.Where(c => c.IdMedico == medicoBuscado.IdMedico)
                            .Include(m => m.IdMedicoNavigation)
                            .Include(m => m.IdMedicoNavigation.IdEspecialidadeNavigation)
                            .Select(c => new Consulta
                            {
                                IdConsulta = c.IdConsulta,
                                IdPaciente = c.IdPaciente,
                                IdPacienteNavigation = c.IdPacienteNavigation,
                                IdMedico = c.IdMedico,
                                IdMedicoNavigation = c.IdMedicoNavigation,
                                IdSituacaoNavigation = c.IdSituacaoNavigation,
                                DataConsulta = c.DataConsulta,
                                HoraConsulta = c.HoraConsulta,
                                Descricao = c.Descricao
                            }).ToList();

                List<object> listaConsultasMedico = new List<object>();

                foreach (var item in listaConsultas)
                {
                    object newItem = new
                    {
                        idConsulta = item.IdConsulta,
                        nomePaciente = item.IdPacienteNavigation.Nome,
                        dataNascimento = item.IdPacienteNavigation.DataNascimento,
                        nomeMedico = item.IdMedicoNavigation.Nome,
                        especialidade = item.IdMedicoNavigation.IdEspecialidadeNavigation.Descricao,
                        crm = item.IdMedicoNavigation.Crm,
                        dataConsulta = item.DataConsulta,
                        horaConsulta = item.HoraConsulta,
                        descricao = item.Descricao,
                        situacao = item.IdSituacaoNavigation.Descricao
                    };

                    listaConsultasMedico.Add(newItem);
                }

                return listaConsultasMedico;
            }

            return null;
        }

    }
}
