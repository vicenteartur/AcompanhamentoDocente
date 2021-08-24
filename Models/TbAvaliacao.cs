using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbAvaliacao
    {
        public TbAvaliacao()
        {
            TbCriterioAvaliados = new HashSet<TbCriterioAvaliado>();
        }

        public int Codigo { get; set; }
        public int CodigoColaboradorAvaliador { get; set; }
        public int CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola { get; set; }
        public byte Finalizada { get; set; }

        public virtual TbAtribuicaoComponenteCurricularAnoColaboradorEscola CodigoAtribuicaoComponenteCurricularAnoColaboradorEscolaNavigation { get; set; }
        public virtual TbColaborador CodigoColaboradorAvaliadorNavigation { get; set; }
        public virtual ICollection<TbCriterioAvaliado> TbCriterioAvaliados { get; set; }
    }
}
