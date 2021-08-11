using senai.SPMedGroup.webApi.Contexts;
using senai.SPMedGroup.webApi.Domains;
using senai.SPMedGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.SPMedGroup.webApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        SpMedContext ctx = new SpMedContext();

        public void Atualizar(int id, TiposUsuario tipoUsuarioAtualizado)
        {
            TiposUsuario tipoBuscado = BuscarPorId(id);

            if (tipoUsuarioAtualizado.NomeTipo != null)
            {
                TiposUsuario tipoBuscadoNome = BuscarPorNome(tipoUsuarioAtualizado.NomeTipo);

                if (tipoBuscadoNome == null)
                {
                    tipoBuscado.NomeTipo = tipoUsuarioAtualizado.NomeTipo;
                }
            }

            ctx.TiposUsuarios.Update(tipoBuscado);

            ctx.SaveChanges();
        }

        public TiposUsuario BuscarPorId(int id)
        {
            return ctx.TiposUsuarios.FirstOrDefault(te => te.IdTipoUsuario == id);
        }

        public TiposUsuario BuscarPorNome(string nome)
        {
            return ctx.TiposUsuarios.FirstOrDefault(te => te.NomeTipo == nome);
        }

        public void Cadastrar(TiposUsuario novoTipo)
        {
            ctx.TiposUsuarios.Add(novoTipo);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.TiposUsuarios.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public List<TiposUsuario> Listar()
        {
            return ctx.TiposUsuarios.ToList();
        }

    }
}
