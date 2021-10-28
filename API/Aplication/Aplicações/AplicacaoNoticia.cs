using Aplication.Interfaces;
using Dominio.Interface.InterfaceServices;
using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using Infraestrutura.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Aplicações
{
    public class AplicacaoNoticia : IAplicacaoNoticia
    {
        INoticia _INoticia;
        IServicoNoticias _IServicoNoticias;

        public AplicacaoNoticia(INoticia INoticia, IServicoNoticias IServicoNoticias)
        {
            _INoticia = INoticia;
            _IServicoNoticias = IServicoNoticias;
        }
        public async Task AdicionaNoticia(Noticia noticia)
        {
            await _IServicoNoticias.AdicionaNoticia(noticia);
        }

        public async Task AtualizaNoticia(Noticia noticia)
        {
            await _IServicoNoticias.AtualizaNoticia(noticia);
        }

        public async Task<List<Noticia>> ListarNoticiaAtiva()
        {
            return await _IServicoNoticias.ListarNoticiaAtiva();
        }

        public async Task<List<NoticiaViewModel>> ListarNoticiaCustomizada()
        {
            return await _IServicoNoticias.ListarNoticiaCustomizada();
        }


        public async Task Adicionar(Noticia Objeto)
        {
            await _INoticia.Adicionar(Objeto);
        }      

        public async Task Atualizar(Noticia Objeto)
        {
            await _INoticia.Atualizar(Objeto);
        }

        public async Task<Noticia> BuscarPorId(int Id)
        {
            return await _INoticia.BuscarPorId(Id);
        }

        public async Task Excluir(Noticia Objeto)
        {
            await _INoticia.Excluir(Objeto);
        }

        public async Task<List<Noticia>> Listar()
        {
            return await _INoticia.Listar();
        }

       
    }
}
