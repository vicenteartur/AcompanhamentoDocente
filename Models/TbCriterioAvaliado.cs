using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbCriterioAvaliado
    {
        public int Codigo { get; set; }
        public int CodigoCriterioAvaliacao { get; set; }
        public byte Conceito { get; set; }
        public string Comentario { get; set; }
        public int CodigoAvaliacao { get; set; }

        public virtual TbAvaliacao CodigoAvaliacaoNavigation { get; set; }
        public virtual TbCriterioAvaliacao CodigoCriterioAvaliacaoNavigation { get; set; }
    }
}
