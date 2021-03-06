using Entidades.Entidades;
using Infraestrutura.Configurações;
using Infraestrutura.Interface;
using Infraestrutura.Repositorio.Genericos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositorio
{
    public class RpositorioNoticia : RepositorioGnerico<Noticia>, INoticia
    {
        private readonly DbContextOptions<Contexto> _optionsBuilder;
        public RpositorioNoticia()
        {
            _optionsBuilder = new DbContextOptions<Contexto>();
        }
        public async Task<List<Noticia>> ListarNoticias(Expression<Func<Noticia, bool>> exNoticia)
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                return await banco.Noticia.Where(exNoticia).AsNoTracking().ToListAsync();
            }
        }

        public async Task<List<Noticia>> ListarNoticiasCustomizado()
        {
            using (var banco = new Contexto(_optionsBuilder))
            {
                var listaNotcia = await (from noticia in banco.Noticia
                                         join usuario in banco.ApplicationUser
                                         on noticia.UserId equals usuario.Id
                                         select new Noticia
                                         {
                                             Id = noticia.Id,
                                             Informacao = noticia.Informacao,
                                             Titulo = noticia.Titulo,
                                             DataCadastro = noticia.DataCadastro,
                                             ApplicationUser = usuario

                                         }).AsNoTracking().ToListAsync();

                return listaNotcia;
            }
        }
    }
}
