using Microsoft.EntityFrameworkCore;
using senai.SPMedGroup.webApi.Contexts;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        SpMedContext ctx = new SpMedContext();

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">Id do usuário que será atualizado</param>
        /// <param name="usuarioAtualizado">Objeto usuarioAtualizado com as novas informações</param>
        public void Atualizar(int id, Usuario usuarioAtualizado)
        {
            Usuario usuarioBuscado = BuscarPorId(id);

            if (usuarioAtualizado.Email != null)
            {
                if (BuscarPorEmail(usuarioAtualizado.Email) == null)
                {
                    usuarioBuscado.Email = usuarioAtualizado.Email;
                }
            }

            if (usuarioAtualizado.Senha != null)
            {
                usuarioBuscado.Senha = usuarioAtualizado.Senha;
            }

            if (usuarioAtualizado.IdTipoUsuario != null)
            {
                if (ctx.TiposUsuarios.Find(usuarioAtualizado.IdTipoUsuario) != null)
                {
                    usuarioBuscado.IdTipoUsuario = usuarioAtualizado.IdTipoUsuario;
                }
            }

            ctx.Usuarios.Update(usuarioBuscado);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Busca um usuário através de seu -e-mail
        /// </summary>
        /// <param name="email">E-mail do usuário que será buscado</param>
        /// <returns>U usuário buscado</returns>
        public Usuario BuscarPorEmail(string email)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.Email == email);
        }

        /// <summary>
        /// Busca um usuário através de seu ID
        /// </summary>
        /// <param name="id">ID do usuário que será buscado</param>
        /// <returns>Um usuário encontrado</returns>
        public Usuario BuscarPorId(int id)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto com as informações para cadastro</param>
        public void Cadastrar(Usuario novoUsuario)
        {
            ctx.Usuarios.Add(novoUsuario);

            ctx.SaveChanges();
        }

        /// <summary>
        /// Deleta um usuário através de seu id
        /// </summary>
        /// <param name="id">Id do usuário que será deletado</param>
        public void Deletar(int id)
        {
            ctx.Usuarios.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Uma lista de usuários</returns>
        public List<Usuario> Listar()
        {
            return ctx.Usuarios.Select(u => new Usuario
            {
                IdUsuario = u.IdUsuario,
                IdTipoUsuario = u.IdTipoUsuario,
                IdTipoUsuarioNavigation = u.IdTipoUsuarioNavigation,
                Email = u.Email,
                Senha = u.Senha
            }).ToList();
        }

        /// <summary>
        /// Faz o login do usuário de acordo com o e-mail e senha
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Um usuário encontrado</returns>
        public Usuario Logar(string email, string senha)
        {
            return ctx.Usuarios.Include(u => u.IdTipoUsuarioNavigation).FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

    }
}
