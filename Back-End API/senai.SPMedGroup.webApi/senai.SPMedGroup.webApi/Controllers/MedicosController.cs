using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using senai.SPMedGroup.webApi.Repositories;
using senai.SPMedGroup.webApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private IMedicoRepository _medicoRepository { get; set; }

        public MedicosController()
        {
            _medicoRepository = new MedicoRepository();
        }

        /// <summary>
        /// Lista todos os médicos
        /// </summary>
        /// <returns>Um status code 200 - Ok e uma lista de médicos</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_medicoRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca um médico através do seu Id
        /// </summary>
        /// <param name="id">Id do médico que será buscado</param>
        /// <returns>Um status code 200 - Ok com um médico encontrado</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_medicoRepository.BuscarPorId(id));
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra um novo médico
        /// </summary>
        /// <param name="novoMedico">Objeto com as informações</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Medico novoMedico)
        {
            try
            {
                if (_medicoRepository.BuscarPorCrm(novoMedico.Crm) == null)
                {
                    _medicoRepository.Cadastrar(novoMedico);

                    return StatusCode(201);
                }

                return BadRequest("Já existe um médico cadastrado com este CRM!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Atualiza um médico existente
        /// </summary>
        /// <param name="id">Id do médico que será atualizado</param>
        /// <param name="medicoAtualizado">Objeto com as novas informações</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "1")]
        [HttpPatch("{id}")]
        public IActionResult Put(int id, MedicoViewModel medicoAtualizado)
        {
            try
            {
                Medico medicoBuscado = _medicoRepository.BuscarPorId(id);

                if (medicoBuscado != null)
                {
                    if (medicoBuscado.Crm == medicoAtualizado.Crm)
                    {
                        return BadRequest("O médico que está sendo atualizado já possui este CRM!");
                    }

                    if (_medicoRepository.BuscarPorCrm(medicoAtualizado.Crm) != null)
                    {

                        return BadRequest("Já existe um médico cadastrado com este CRM!");
                    }

                    Medico medico = new Medico
                    {
                        IdEspecialidade     = medicoAtualizado.IdEspecialidade,
                        IdUsuario           = medicoAtualizado.IdUsuario,
                        IdClinica           = medicoAtualizado.IdClinica,
                        Nome                = medicoAtualizado.Nome,
                        Crm                 = medicoAtualizado.Crm
                    };

                    _medicoRepository.Atualizar(id, medico);

                    return StatusCode(204);
                }

                return NotFound("Médico não encontrado");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Deleta um médico existente
        /// </summary>
        /// <param name="id">Id do médico que será deletado</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_medicoRepository.BuscarPorId(id) != null)
                {
                    _medicoRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Médico não encontrado!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

    }
}
