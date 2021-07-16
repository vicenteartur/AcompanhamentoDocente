using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbClassificacaoCriterio
    {
        public TbClassificacaoCriterio()
        {
            TbCriterioAvaliacaos = new HashSet<TbCriterioAvaliacao>();
        }

        public int Codigo { get; set; }
        public string Classificacao { get; set; }

        public virtual ICollection<TbCriterioAvaliacao> TbCriterioAvaliacaos { get; set; }
    }
}
