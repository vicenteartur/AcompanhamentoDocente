using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.ModelsRelatorio
{
    public class linha_plan_relatorio_xls
    {
        public int CodigoAvaliacao { get; set; }
        public DateTime dataavaliacao { get; set; }
        public string Avaliado { get; set; }
        public int codigoAvaliador { get; set; }
        public string Avaliador { get; set; }
        public string Escola { get; set; }
        public string anoturma { get; set; }
        public string modalidade { get; set; }
        public string ccurricular { get; set; }
        public int CodigoCriterio { get; set; }
        public string Criterio { get; set; }
        public string Classificacao{ get; set; }
        public int Conceito {get; set; }
        




    }
}
