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
    public class PacientesController : ControllerBase
    {
        private IPacienteRepository _pacienteRepository { get; set; }

        public PacientesController()
        {
            _pacienteRepository = new PacienteRepository();
        }

        /// <summary>
        /// Lista todos os pacientes
        /// </summary>
        /// <returns>Um status code 200 - Ok e uma lista de pacientes</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_pacienteRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca um paciente através de seu id
        /// </summary>
        /// <param name="id">Id do paciente que será buscado</param>
        /// <returns>Um status code 200 - Ok com o paciente encontrado</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_pacienteRepository.BuscarPorId(id));
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra um novo paciente
        /// </summary>
        /// <param name="novoPaciente">Objeto com as novas informações</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Paciente novoPaciente)
        {
            try
            {
                string pacienteBuscado = _pacienteRepository.BuscarPorRgCpf(novoPaciente.Rg, novoPaciente.Cpf);

                if (pacienteBuscado == "0")
                {
                    return BadRequest("Não foi possível cadastrar! Já existe um paciente cadastrado com esse RG!");
                }

                if (pacienteBuscado == "1")
                {
                    return BadRequest("Não foi possível cadastrar! Já existe um paciente cadastrado com esse CPF!");
                }

                _pacienteRepository.Cadastrar(novoPaciente);

                return StatusCode(201);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Atualiza um paciente existente
        /// </summary>
        /// <param name="id">Id do paciente que será atualizado</param>
        /// <param name="pacienteAtualizado">Objeto com as novas informações</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, PacienteViewModel pacienteAtualizado)
        {
            try
            {
                if (_pacienteRepository.BuscarPorId(id) == null)
                {
                    return NotFound("Paciente não encontrado!");
                }

                Paciente paciente = new Paciente
                {
                    IdUsuario       = pacienteAtualizado.idUsuario,
                    Nome            = pacienteAtualizado.Nome,
                    Rg              = pacienteAtualizado.Rg,
                    Cpf             = pacienteAtualizado.Cpf,
                    Telefone        = pacienteAtualizado.Telefone,
                    DataNascimento = pacienteAtualizado.DataNascimento,
                    Endereco        = pacienteAtualizado.Endereco
                };

                _pacienteRepository.Atualizar(id, paciente);

                return StatusCode(204);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Deleta um paciente existente
        /// </summary>
        /// <param name="id">Id do paciente que será atualizado</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_pacienteRepository.BuscarPorId(id) == null)
                {
                    return NotFound("Paciente não encontrado!");
                }

                _pacienteRepository.Deletar(id);

                return StatusCode(204);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

    }
}
