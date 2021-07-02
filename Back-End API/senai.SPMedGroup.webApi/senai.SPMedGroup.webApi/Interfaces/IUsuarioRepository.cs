using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface IUsuarioRepository
    {
        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>Uma lista de usuários</returns>
        List<Usuario> Listar();

        /// <summary>
        /// Busca um usuário através de seu ID
        /// </summary>
        /// <param name="id">ID do usuário que será buscado</param>
        /// <returns>Um usuário encontrado</returns>
        Usuario BuscarPorId(int id);

        /// <summary>
        /// Busca um usuário através de seu -e-mail
        /// </summary>
        /// <param name="email">E-mail do usuário que será buscado</param>
        /// <returns>U usuário buscado</returns>
        Usuario BuscarPorEmail(string email);

        /// <summary>
        /// Busca o nome do usuário
        /// </summary>
        /// <param name="id">Id do usuário</param>
        /// <returns>O nome do usuário</returns>
        string BuscarNomeUsuario(int id);

        /// <summary>
        /// Faz o login do usuário de acordo com o e-mail e senha
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Um usuário encontrado</returns>
        Usuario Logar(string email, string senha);

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto com as informações para cadastro</param>
        void Cadastrar(Usuario novoUsuario);

        /// <summary>
        /// Atualiza um usuário existente
        /// </summary>
        /// <param name="id">Id do usuário que será atualizado</param>
        /// <param name="usuarioAtualizado">Objeto usuarioAtualizado com as novas informações</param>
        void Atualizar(int id, Usuario usuarioAtualizado);

        /// <summary>
        /// Deleta um usuário através de seu id
        /// </summary>
        /// <param name="id">Id do usuário que será deletado</param>
        void Deletar(int id);
    }
}
