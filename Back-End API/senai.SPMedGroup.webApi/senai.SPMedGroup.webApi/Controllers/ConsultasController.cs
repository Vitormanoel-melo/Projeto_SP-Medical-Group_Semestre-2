using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using senai.SPMedGroup.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private IConsultaRepository _consultaRepository { get; set; }

        public ConsultasController()
        {
            _consultaRepository = new ConsultaRepository();
        }

        /// <summary>
        /// Lista todas as consultas
        /// </summary>
        /// <returns>Um status code 200 - Ok e uma lista de consultas</returns>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_consultaRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca uma consulta através do id dela
        /// </summary>
        /// <param name="id">Id da consulta que será deletada</param>
        /// <returns>Um status code 200 - Ok e uma consulta encontrada</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_consultaRepository.BuscarPorId(id));
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Lista todas as consultas de um determinado paciente ou médico que está logado
        /// </summary>
        /// <returns>Uma lista de consultas</returns>
        [Authorize(Roles = "2,3")] 
        [HttpGet("minhas")]
        public IActionResult GetMy()
        {
            try
            {
                int idUsuario = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                return Ok(_consultaRepository.ListarMinhas(idUsuario));
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra uma nova consulta
        /// </summary>
        /// <param name="novaConsulta">Objeto novaConsulta com as informações para cadastro</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Consulta novaConsulta)
        {
            try
            {
                if (novaConsulta.DataConsulta < DateTime.Now)
                {
                    return BadRequest("Informe uma data válida para consulta");
                }

                _consultaRepository.Cadastrar(novaConsulta);

                return StatusCode(201);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Deleta uma consulta existente
        /// </summary>
        /// <param name="id">Id da consulta que será deletada</param>
        /// <returns>Um status code 204 - No Content</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_consultaRepository.BuscarPorId(id) != null)
                {
                    _consultaRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Consulta não encontrada");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

    }
}