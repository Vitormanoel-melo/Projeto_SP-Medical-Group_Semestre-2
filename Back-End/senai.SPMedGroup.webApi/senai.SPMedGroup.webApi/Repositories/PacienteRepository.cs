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
    public class PacienteRepository : IPacienteRepository
    {
        SpMedContext ctx = new SpMedContext();

        /// <summary>
        /// Atualiza um paciente existente
        /// </summary>
        /// <param name="id">Id do paciente que será atualizado</param>
        /// <param name="pacienteAtualizado">Objeto pacienteAtualizado com as novas informações</param>
        public void Atualizar(int id, Paciente pacienteAtualizado)
        {
            Paciente pacienteBuscado = BuscarPorId(id);

            if (pacienteAtualizado.IdUsuario != null)
            {
                if (ctx.Usuarios.Find(id) != null)
                {
                    pacienteBuscado.IdUsuario = pacienteAtualizado.IdUsuario;
                }
            }

            if (pacienteAtualizado.Nome != null)
            {
                pacienteBuscado.Nome = pacienteAtualizado.Nome;
            }

            if (pacienteAtualizado.Rg != null)
            {
                if(ctx.Pacientes.Find(pacienteAtualizado.Rg) == null)
                {
                    pacienteBuscado.Rg = pacienteAtualizado.Rg;
                }
            }

            if (pacienteAtualizado.Cpf != null)
            {
                if (ctx.Pacientes.Find(pacienteAtualizado.Cpf) == null)
                {
                    pacienteBuscado.Cpf = pacienteAtualizado.Cpf;
                }
            }

            if (pacienteAtualizado.Telefone != null)
            {
                pacienteBuscado.Telefone = pacienteAtualizado.Telefone;
            }

            if (pacienteAtualizado.Endereco != null)
            {
                pacienteBuscado.Endereco = pacienteAtualizado.Endereco;
            }

            if (pacienteAtualizado.DataNascimento != Convert.ToDateTime("0001-01-01"))
            {
                pacienteBuscado.DataNascimento = pacienteAtualizado.DataNascimento;
            }

            ctx.Pacientes.Update(pacienteBuscado);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca um paciente através de seu id
        /// </summary>
        /// <param name="id">Id do paciente que será buscado</param>
        /// <returns>Um paciente encontrado</returns>
        public Paciente BuscarPorId(int id)
        {
            return ctx.Pacientes.Include(p => p.IdUsuarioNavigation).FirstOrDefault(p => p.IdPaciente == id);
        }

        public string BuscarPorRgCpf(string rg, string cpf)
        {
            Paciente pacienteBuscadoRg      = ctx.Pacientes.FirstOrDefault(p => p.Rg == rg);
            Paciente pacienteBuscadoCpf     = ctx.Pacientes.FirstOrDefault(p => p.Cpf == cpf);

            if (pacienteBuscadoRg != null)
            {
                return "0";
            }

            if (pacienteBuscadoCpf != null)
            {
                return "1";
            }

            return "2";
        }

        /// <summary>
        /// Cadastra um novo paciente
        /// </summary>
        /// <param name="novoPaciente">Objeto novoPaciente com as informações para cadastro</param>
        public void Cadastrar(Paciente novoPaciente)
        {
            ctx.Pacientes.Add(novoPaciente);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um paciente existente
        /// </summary>
        /// <param name="id">Id do paciente que será deletado</param>
        public void Deletar(int id)
        {
            ctx.Pacientes.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todos os pacientes
        /// </summary>
        /// <returns>Uma lista de pacientes</returns>
        public List<Paciente> Listar()
        {
            return ctx.Pacientes.Include(p => p.IdUsuarioNavigation).ToList();
        }

    }
}
