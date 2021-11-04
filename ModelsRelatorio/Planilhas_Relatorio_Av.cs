using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcompanhamentoDocente.Models;

namespace AcompanhamentoDocente.ModelsRelatorio
{
    public class Planilhas_Relatorio_Av
    {
        public int CodigoAvaliacao { get; set; }
        public string Avaliado { get; set; }
        public string Avaliador { get; set; }
        public DateTime dataavaliacao { get; set; }
        public string ccurricular { get; set; }
        public string modalidade { get; set; }
        public virtual ICollection<TbCriterioAvaliado> criterioavaliados { get; set; }
    }
}
