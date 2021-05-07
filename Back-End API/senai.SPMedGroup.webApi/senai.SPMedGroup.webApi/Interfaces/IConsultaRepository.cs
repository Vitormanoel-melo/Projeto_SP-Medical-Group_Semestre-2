using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface IConsultaRepository
    {
        /// <summary>
        /// Lista todas as consultas
        /// </summary>
        /// <returns>Uma lista de consultas</returns>
        List<Consulta> Listar();

        /// <summary>
        /// Busca uma consulta através do Id
        /// </summary>
        /// <param name="id">Id da consulta que será buscada</param>
        /// <returns>Uma consulta encontrada</returns>
        Consulta BuscarPorId(int id);

        /// <summary>
        /// Cadastra uma nova consulta
        /// </summary>
        /// <param name="novaConsulta">Objeto novaConsulta com as informações</param>
        void Cadastrar(Consulta novaConsulta);

        /// <summary>
        /// Atualiza uma consulta existente
        /// </summary>
        /// <param name="id">Id da consulta que será atualizada</param>
        /// <param name="consultaAtualizada">Objeto consultaAtualizada com as novas informações</param>
        void AtualizarConsulta(int id, Consulta consultaAtualizada);

        /// <summary>
        /// Deleta uma consulta existente
        /// </summary>
        /// <param name="id">Id da consulta que será deletada</param>
        void Deletar(int id);

        /// <summary>
        /// Atualiza o status de uma consulta
        /// </summary>
        /// <param name="idConsulta">Id da consulta que será atualizada</param>
        /// <param name="idSituacao">Id da situação que a consulta terá</param>
        void AtualizarSituacao(int idConsulta, int idSituacao);

        /// <summary>
        /// Insere uma descrição a uma consulta
        /// </summary>
        /// <param name="id">Id da consulta</param>
        /// <param name="descricao">Descricao da consulta</param>
        void InserirDescricao(int id, Consulta descricao);

        /// <summary>
        /// Lista consultas de acordo com o id recebido
        /// </summary>
        /// <param name="id">Id de um médico ou paciente para listar as consultas</param>
        /// <returns>Uma lista de consultas</returns>
        List<Consulta> ListarMinhas(int id);
    }
}
