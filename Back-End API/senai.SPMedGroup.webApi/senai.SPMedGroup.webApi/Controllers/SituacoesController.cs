using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using senai.SPMedGroup.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class SituacoesController : ControllerBase
    {
        private ISituacaoRepository _situacaoRepository { get; set; }

        public SituacoesController()
        {
            _situacaoRepository = new SituacaoRepository();
        }

        /// <summary>
        /// Lista todas as situações
        /// </summary>
        /// <returns>Um status code 200 - Ok e uma lista de situações</returns>
        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_situacaoRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca uma situação através do Id
        /// </summary>
        /// <param name="id">Id da situação que será buscada</param>
        /// <returns>Um status code 200 - Ok e uma situação encontrada</returns>
        [Authorize(Roles = "administrador")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_situacaoRepository.BuscarPorId(id));
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra uma nova situação
        /// </summary>
        /// <param name="novaSituacao">Objeto com as informações</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(Situaco novaSituacao)
        {
            try
            {
                _situacaoRepository.Cadastrar(novaSituacao);

                return StatusCode(201);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Atualiza uma situação existente
        /// </summary>
        /// <param name="id">Id da situação que será atualizada</param>
        /// <param name="situacaoAtualizada">Objeto com as novas informações</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "administrador")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, Situaco situacaoAtualizada)
        {
            try
            {
                Situaco situacaoBuscada = _situacaoRepository.BuscarPorId(id);

                if (situacaoBuscada != null)
                {
                    _situacaoRepository.Atualizar(id, situacaoAtualizada);

                    return StatusCode(204);
                }

                return NotFound("Situação não encontrada");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Deleta uma situação existente
        /// </summary>
        /// <param name="id">Id da situação que será deletada</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Situaco situacaoBuscada = _situacaoRepository.BuscarPorId(id);

                if(situacaoBuscada != null)
                {
                    _situacaoRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Situação não encontrada!");
            }
            catch (Exception exception)
            {

                return NotFound(exception);
            }
        }

    }
}
