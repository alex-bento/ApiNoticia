using Dominio.Interface.InterfaceServices;
using Entidades.Entidades;
using Infraestrutura.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Servico
{
    public class ServicoNoticia : IServicoNoticias
    {
        private readonly INoticia _INoticia;
        public ServicoNoticia(INoticia INoticia)
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

        public async Task<List<Noticia>> ListarNoticiaAtiva(Noticia noticia)
        {
            return await _INoticia.ListarNoticias(n => n.Ativo);
        }
    }
}
