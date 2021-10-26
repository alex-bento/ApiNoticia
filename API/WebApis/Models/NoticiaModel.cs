using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApis.Models
{
    public class NoticiaModel
    {
        public int IdNoticia { get; set; }

        public string Titulo { get; set; }

        public string Informacao { get; set; }
        public string IdUsuario { get; set; }
    }
}