using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.ModelsRelatorio
{
    public class Linhas_Avaliacao
    {
        public int CodigoCC { get; set; }
        public string Componente { get; set; }
        public int CodigoModalidade { get; set; }
        public string Modalidade { get; set; }
        public string SubArea { get; set; }
        public int CodCriterio { get; set; }
        public int Conceito { get; set; }
        public int CodClassCriterio { get; set; }
        public string ClassCriterio { get; set; }
    }
}
