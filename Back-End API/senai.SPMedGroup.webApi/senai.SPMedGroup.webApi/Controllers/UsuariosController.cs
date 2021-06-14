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
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Um status code 200 - Ok e uma lista de usuários</returns>
        [Authorize(Roles = "1")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_usuarioRepository.Listar());
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Busca um usuário através de seu id
        /// </summary>
        /// <param name="id">Id do usuário que será buscado</param>
        /// <returns>Um status code 200 - Ok com o usuário encontrado</returns>
        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(_usuarioRepository.BuscarPorId(id));
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto com as informações</param>
        /// <returns>Um status code 201 - Created</returns>
        [Authorize(Roles = "1")]
        [HttpPost]
        public IActionResult Post(Usuario novoUsuario)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorEmail(novoUsuario.Email);

                if (usuarioBuscado == null)
                {
                    _usuarioRepository.Cadastrar(novoUsuario);

                    return StatusCode(201);
                }

                return BadRequest("Já existe um usuário cadastrado com esse e-mail!");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">Id do usuário que será atualizado</param>
        /// <param name="usuarioAtualizado">Objeto com as novas informações</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, UsuarioViewModel usuarioAtualizado)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.BuscarPorId(id);

                if (usuarioBuscado == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                Usuario usuario = new Usuario
                {
                    Email           = usuarioAtualizado.Email,
                    Senha           = usuarioAtualizado.Senha,
                    IdTipoUsuario   = usuarioAtualizado.idTipoUsuario
                };

                _usuarioRepository.Atualizar(id, usuario);

                return StatusCode(204);
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

        /// <summary>
        /// Deleta um usuário através de seu id
        /// </summary>
        /// <param name="id">Id do usuário que será deletado</param>
        /// <returns>Um status code 204 - No Content</returns>
        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_usuarioRepository.BuscarPorId(id) != null)
                {
                    _usuarioRepository.Deletar(id);

                    return StatusCode(204);
                }

                return NotFound("Usuário não encontrado");
            }
            catch (Exception exception)
            {

                return BadRequest(exception);
            }
        }

    }
}
