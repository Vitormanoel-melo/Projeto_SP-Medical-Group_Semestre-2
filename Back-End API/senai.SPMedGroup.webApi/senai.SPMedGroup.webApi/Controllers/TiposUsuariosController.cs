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
    public class TiposUsuariosController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }

        public TiposUsuariosController()
        {
            _tipoUsuarioRepository = new TipoUsuarioRepository();
        }

        /// <summary>
        /// Lista todos os tipos de usuário
        /// </summary>
        /// <returns>Uma lista de tipos de usuário</returns>
        [Authorize(Roles = "administrador")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_tipoUsuarioRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca um tipo de usuário através de seu ID
        /// </summary>
        /// <param name="id">Id do tipo de usuário que será buscado</param>
        /// <returns>Um status code 200 - Ok com o tipo de usuário encontrado</returns>
        [Authorize(Roles = "administrador")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_tipoUsuarioRepository.BuscarPorId(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra um tipo de usuário
        /// </summary>
        /// <param name="novoTipoUsuario">Objeto novoTipoUsuario com as informações</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "administrador")]
        [HttpPost]
        public IActionResult Post(TiposUsuario novoTipoUsuario)
        {
            try
            {
                if (_tipoUsuarioRepository.BuscarPorNome(novoTipoUsuario.NomeTipo) == null)
                {
                    _tipoUsuarioRepository.Cadastrar(novoTipoUsuario);

                    return StatusCode(201);
                }

                return BadRequest("Este tipo de usuário já está cadastrado!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Atualiza um tipo de usuário existente
        /// </summary>
        /// <param name="id">Id do tipo de usuário que será atualizado</param>
        /// <param name="tipoAtualizado">Objeto tipoAtualizado com as novas informações</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "administrador")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, TiposUsuario tipoAtualizado)
        {
            try
            {
                TiposUsuario tipoUsuarioBuscado = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoUsuarioBuscado == null)
                {
                    return NotFound("Tipo de usuário não encontrado!");
                }

                if (tipoUsuarioBuscado.NomeTipo == tipoAtualizado.NomeTipo)
                {
                    return BadRequest("O tipo que está sendo atualizado já contém essas informações!");
                }

                _tipoUsuarioRepository.Atualizar(id, tipoAtualizado);

                return StatusCode(204);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Deleta um tipo de usuário existente
        /// </summary>
        /// <param name="id">Id do tipo de usuário que será deletado</param>
        /// <returns>Um status code 204 - NoContent</returns>
        [Authorize(Roles = "administrador")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_tipoUsuarioRepository.BuscarPorId(id) != null)
                {
                    _tipoUsuarioRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Tipo de usuário não encontrado!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

    }
}
