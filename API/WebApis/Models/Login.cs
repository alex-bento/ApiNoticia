using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApis.Models
{
    public class Login
    {
        // Propriedeade para fazer o Login

        public string email { get; set; }
        public string senha { get; set; }
        public int idade { get; set; }
        public string celular { get; set; }
    }
}