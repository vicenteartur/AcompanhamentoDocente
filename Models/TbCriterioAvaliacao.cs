using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbCriterioAvaliacao
    {
        public TbCriterioAvaliacao()
        {
            TbCriterioAvaliados = new HashSet<TbCriterioAvaliado>();
        }

        public int Codigo { get; set; }
        public string Criterio { get; set; }
        public int CodigoClassificacaoCriterio { get; set; }
        public byte Ativa { get; set; }

        public virtual TbClassificacaoCriterio CodigoClassificacaoCriterioNavigation { get; set; }
        public virtual ICollection<TbCriterioAvaliado> TbCriterioAvaliados { get; set; }
    }
}
