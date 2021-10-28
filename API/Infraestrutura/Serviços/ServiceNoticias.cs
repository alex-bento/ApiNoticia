using Dominio.Interface.InterfaceServices;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using Infraestrutura.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Servicos
{
    class ServiceNoticias : IServicoNoticias
    {
        private readonly INoticia _INoticia;

        public ServiceNoticias(INoticia INoticia)
        {
            _INoticia = INoticia;
        }
        public async Task AdicionaNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidarPropriedaeString(noticia.Titulo, "Titulo");
            var validarInformacao = noticia.ValidarPropriedaeString(noticia.Informacao, "Informacao");

            if (validarTitulo && validarInformacao)
            {
                noticia.DataAlteeracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _INoticia.Adicionar(noticia);
            }
        }

        public async Task AtualizaNoticia(Noticia noticia)
        {
            var validarTitulo = noticia.ValidarPropriedaeString(noticia.Titulo, "Titulo");
            var validarInformacao = noticia.ValidarPropriedaeString(noticia.Informacao, "Informacao");

            if (validarTitulo && validarInformacao)
            {
                noticia.DataAlteeracao = DateTime.Now;
                noticia.DataCadastro = DateTime.Now;
                noticia.Ativo = true;
                await _INoticia.Atualizar(noticia);
            }
        }

        public async Task<List<Noticia>> ListarNoticiaAtiva()
        {
            return await _INoticia.ListarNoticias(n => n.Ativo);
        }

        public async Task<List<NoticiaViewModel>> ListarNoticiaCustomizada()
        {
            var listarNoticiaCustomizada = await _INoticia.ListarNoticiasCustomizado();

            var retorno = (
                    from noticia in listarNoticiaCustomizada
                    select new NoticiaViewModel
                    {
                        Id = noticia.Id,
                        Titulo = noticia.Titulo,
                        Informacao = noticia.Informacao,
                        DataCadastro = string.Concat(noticia.DataCadastro.Day, "/", noticia.DataCadastro.Month, "/", noticia.DataCadastro.Year),
                        Usuario = SepararEmail(noticia.ApplicationUser.Email)
                    }
                ).ToList();

            return retorno;
        }

        private string SepararEmail(string email)
        {
            var stringEmail = email.Split('@');

            return stringEmail[0].ToString();
        }
    }
}
