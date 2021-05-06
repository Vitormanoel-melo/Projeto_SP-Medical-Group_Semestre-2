using senai.SPMedGroup.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Interfaces
{
    interface ITipoUsuarioRepository
    {
        /// <summary>
        /// Lista todos os tipos de usuário
        /// </summary>
        /// <returns>Uma lista de tipos de usuário</returns>
        List<TiposUsuario> Listar();

        /// <summary>
        /// Busca um tipo de usuário através de seu id
        /// </summary>
        /// <param name="id">Id do tipo de usuário que será buscado</param>
        /// <returns>Um tipo de usuário encontrado</returns>
        TiposUsuario BuscarPorId(int id);

        /// <summary>
        /// Busca um tipo de usuário pelo nome
        /// </summary>
        /// <param name="nome">Nome do tipo de usuário que será buscado</param>
        /// <returns>Um tipo de usuário encotrado</returns>
        TiposUsuario BuscarPorNome(string nome);

        /// <summary>
        /// Cadastra um tipo de usuário
        /// </summary>
        /// <param name="novoTipo">Objeto novoTipo com as informações para cadastro</param>
        void Cadastrar(TiposUsuario novoTipo);

        /// <summary>
        /// Atualiza um tipo de usuário existente
        /// </summary>
        /// <param name="id">Id do tipo de usuário que será atualizado</param>
        /// <param name="tipoUsuarioAtualizado">Objeto com as novas informações</param>
        void Atualizar(int id, TiposUsuario tipoUsuarioAtualizado);

        /// <summary>
        /// Deleta um tipo de usuário existente
        /// </summary>
        /// <param name="id">Id do tipo de usuário que será deletado</param>
        void Deletar(int id);
    }
}
