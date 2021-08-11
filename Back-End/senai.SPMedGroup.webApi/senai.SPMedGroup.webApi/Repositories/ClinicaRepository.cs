using senai.SPMedGroup.webApi.Contexts;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Repositories
{
    public class ClinicaRepository : IClinicaRepository
    {
        SpMedContext ctx = new SpMedContext();

        /// <summary>
        /// Atualiza uma clínica existente
        /// </summary>
        /// <param name="id">Id da clínica que será atualizada</param>
        /// <param name="clinicaAtualizada">Objeto com as novas informações</param>
        public void Atualizar(int id, Clinica clinicaAtualizada)
        {
            Clinica clinicaBuscada = BuscarPorId(id);

            if (clinicaAtualizada.NomeClinica != null)
            {
                clinicaBuscada.NomeClinica = clinicaAtualizada.NomeClinica;
            }

            if (clinicaAtualizada.DataAbertura != null)
            {
                clinicaBuscada.DataAbertura = clinicaAtualizada.DataAbertura;
            }

            if (clinicaAtualizada.Cnpj != null)
            {
                clinicaBuscada.Cnpj = clinicaAtualizada.Cnpj;
            }

            if (clinicaAtualizada.RazaoSocial != null)
            {
                clinicaBuscada.RazaoSocial = clinicaAtualizada.RazaoSocial;
            }

            if (clinicaAtualizada.Endereco != null)
            {
                clinicaBuscada.Endereco = clinicaAtualizada.Endereco;
            }

            if (clinicaAtualizada.HorarioAbertura.ToString() != "00:00:00")
            {
                clinicaBuscada.HorarioAbertura = clinicaAtualizada.HorarioAbertura;
            }

            if (clinicaAtualizada.HorarioFechamento != null)
            {
                clinicaBuscada.HorarioFechamento = clinicaAtualizada.HorarioFechamento;
            }

            ctx.Clinicas.Update(clinicaBuscada);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca uma clinica através de seu id
        /// </summary>
        /// <param name="id">Id da clínica que será buscada</param>
        /// <returns>Um clínica buscada</returns>
        public Clinica BuscarPorId(int id)
        {
            return ctx.Clinicas.FirstOrDefault(c => c.IdClinica == id);
        }

        /// <summary>
        /// Cadastra uma nova clínica
        /// </summary>
        /// <param name="novaClinica">Objeto novaClinica com as informações para cadastro</param>
        public void Cadastrar(Clinica novaClinica)
        {
            ctx.Clinicas.Add(novaClinica);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta uma clínica existente
        /// </summary>
        /// <param name="id">Id da clínica que será deletada</param>
        public void Deletar(int id)
        {
            ctx.Clinicas.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todas as clinicas
        /// </summary>
        /// <returns>Uma lista de clínicas</returns>
        public List<Clinica> Listar()
        {
            return ctx.Clinicas.ToList();
        }

        /// <summary>
        /// Lista todas as clínicas sem enviar dados sensíveis como CNPJ
        /// </summary>
        /// <returns>Uma lista de clínicas</returns>
        public List<Clinica> ListarClinicasPublic()
        {
            return ctx.Clinicas
                .Select(c => new Clinica 
                { 
                    IdClinica = c.IdClinica,
                    NomeClinica = c.NomeClinica,
                    DataAbertura = c.DataAbertura,
                    HorarioAbertura = c.HorarioAbertura,
                    HorarioFechamento = c.HorarioFechamento,
                    Endereco = c.Endereco,
                    RazaoSocial = c.RazaoSocial
                })
                .ToList();
        }
    }
}
