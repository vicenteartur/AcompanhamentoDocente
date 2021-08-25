using AcompanhamentoDocente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.ViewModel
{
    public class AvaliacaoViewModel
    {
        public int Codigo { get; set; }
        public DateTime dataavaliacao { get; set; }
        public int CodigoColaboradorAvaliador { get; set; }
        public string NomeAvaliador { get; set; }
        public int CodigoACECCA { get; set; }
        public string NomeColaborador { get; set; }
        public string escola { get; set; }
        public string ccurric { get; set; }
        public string ano { get; set; }
        public List<TbCriterioAvaliado> CriterioAvaliado { get; set; }
        public byte Finalizada { get; set; }
    }
}
