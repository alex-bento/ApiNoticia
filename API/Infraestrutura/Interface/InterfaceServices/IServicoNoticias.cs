using Entidades.Entidades;
using Entidades.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interface.InterfaceServices
{
    public interface IServicoNoticias
    {
        Task AdicionaNoticia(Noticia noticia);
        Task AtualizaNoticia(Noticia noticia);
        Task<List<Noticia>> ListarNoticiaAtiva();
        Task<List<NoticiaViewModel>> ListarNoticiaCustomizada();
    }
}
