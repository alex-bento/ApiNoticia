using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Genericos
{
    public interface IGereicaAplicacao<T> where T : class
    {
        Task Adicionar(T Objeto);
        Task Atualizar(T Objeto);
        Task Excluir(T Objeto);
        Task<T> BuscarPorId(T Id);
        Task<List<T>> Listar();
    }
}
