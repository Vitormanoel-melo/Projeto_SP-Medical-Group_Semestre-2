using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using senai.SPMedGroup.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class EspecialidadesController : ControllerBase
    {
        private IEspecialidadeRepository _especialidadeRepository { get; set; }

        public EspecialidadesController()
        {
            _especialidadeRepository = new EspecialidadeRepository();
        }

        /// <summary>
        /// Lista todas as especialidades
        /// </summary>
        /// <returns>Um status code 200 - Ok e uma lista de habilidades</returns>
        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_especialidadeRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca uma especialidade atrávés de seu id
        /// </summary>
        /// <param name="id">Id da especialidade que será buscada</param>
        /// <returns>Um status code 200 - Ok e uma especialidade encontrada</returns>
        [Authorize(Roles = "administrador")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_especialidadeRepository.BuscarPorId(id));
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra uma nova especialidade
        /// </summary>
        /// <param name="novaEspecialidade">Objeto novaEspecialidade com as informações</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(Especialidade novaEspecialidade)
        {
            try
            {
                _especialidadeRepository.Cadastrar(novaEspecialidade);

                return Created(HttpStatusCode.Created.ToString(), novaEspecialidade);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Atualiza uma especialidade existente
        /// </summary>
        /// <param name="id">Id da habilidade que será atualizada</param>
        /// <param name="especialidadeAtualizada">Objeto especialidadeAtualizada com as novas informações</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "administrador")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Especialidade especialidadeAtualizada)
        {
            try
            {
                if (_especialidadeRepository.BuscarPorId(id) != null)
                {
                    _especialidadeRepository.Atualizar(id, especialidadeAtualizada);

                    return StatusCode(204);
                }

                return NotFound("Especialidade não encontrada!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Deleta uma especialidade existente
        /// </summary>
        /// <param name="id">Id da especialidade que será deletada</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Especialidade especialidadeBuscada = _especialidadeRepository.BuscarPorId(id);

                if (especialidadeBuscada != null)
                {
                    _especialidadeRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Especialidade não encontrada!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

    }
}
