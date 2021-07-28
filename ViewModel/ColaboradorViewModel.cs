using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.ViewModel
{
    public class ColaboradorViewModel
    {

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int CodigoCargo { get; set; }
        public string Cargo { get; set; }
        public int NiveldeAcesso { get; set; }

    }
}
