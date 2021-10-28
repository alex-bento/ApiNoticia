using Dominio.Interface.Genericos;
using Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Interface
{
    public interface INoticia : IGenericos<Noticia>
    {
        // Coloquei uma expressão
        Task<List<Noticia>> ListarNoticias(Expression<Func<Noticia, bool>> exNoticia);

        Task<List<Noticia>> ListarNoticiasCustomizado();
    }
}
