using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Interface
{
    public interface IUsuario
    {
        Task<bool> AdicionarUsuario(string email, string senha, int idade, string celular);

        // Verificar se existe o usuario
        Task<bool> ExisteUsuario(string email, string senha);
    }
}
