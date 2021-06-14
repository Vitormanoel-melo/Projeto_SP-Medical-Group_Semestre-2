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
    public class ClinicasController : ControllerBase
    {
        private IClinicaRepository _clinicaRepository { get; set; }

        public ClinicasController()
        {
            _clinicaRepository = new ClinicaRepository();
        }

        /// <summary>
        /// Lista todas as clínicas
        /// </summary>
        /// <returns>Um status code 200 - Ok e uma lista de clinicas</returns>
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_clinicaRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca uma clínica através de seu Id
        /// </summary>
        /// <param name="id">Id da clinica que será buscada</param>
        /// <returns>Um status code 200 - Ok com a clinica buscada</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                Clinica clinicaBuscada = _clinicaRepository.BuscarPorId(id);

                if (clinicaBuscada == null)
                {
                    return NotFound("Clínica não encontrada!");
                }

                return Ok(clinicaBuscada);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra uma nova clínica
        /// </summary>
        /// <param name="novaClinica">Objeto novaClinica com as informações</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Clinica novaClinica)
        {
            try
            {
                _clinicaRepository.Cadastrar(novaClinica);

                return StatusCode(201);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Atualiza uma clínica existente
        /// </summary>
        /// <param name="id">Id da clínica que será atualizada</param>
        /// <param name="clinicaAtualizada">Objeto com as novas informações</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "1")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ClinicaViewModel clinicaAtualizada)
        {
            try
            {
                Clinica clinicaBuscada = _clinicaRepository.BuscarPorId(id);

                if (clinicaBuscada == null)
                {
                    return NotFound("Clínica não encontrada!");
                }

                clinicaBuscada = new Clinica
                {
                    NomeClinica = clinicaAtualizada.NomeClinica,
                    DataAbertura = clinicaAtualizada.DataAbertura,
                    HorarioAbertura = clinicaAtualizada.HorarioAbertura,
                    HorarioFechamento = clinicaAtualizada.HorarioFechamento,
                    Cnpj = clinicaAtualizada.Cnpj,
                    RazaoSocial = clinicaAtualizada.RazaoSocial,
                    Endereco = clinicaAtualizada.Endereco
                };

                _clinicaRepository.Atualizar(id, clinicaBuscada);

                return StatusCode(204);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deleta uma clínica existente
        /// </summary>
        /// <param name="id">Id da clínica que será deletada</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_clinicaRepository.BuscarPorId(id) != null)
                {
                    _clinicaRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Clínica não encontrada!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

    }
}
