using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbAtribuicaoComponenteCurricularAnoColaboradorEscola
    {
        public TbAtribuicaoComponenteCurricularAnoColaboradorEscola()
        {
            TbAvaliacaos = new HashSet<TbAvaliacao>();
        }

        public int Codigo { get; set; }
        public int CodigoAtribuicaoColaboradorEscola { get; set; }
        public int CodigoComponenteCurricular { get; set; }
        public int CodigoAno { get; set; }
        public byte Ativa { get; set; }

        public virtual TbAno CodigoAnoNavigation { get; set; }
        public virtual TbAtribuicaoColaboradorEscola CodigoAtribuicaoColaboradorEscolaNavigation { get; set; }
        public virtual TbComponenteCurricular CodigoComponenteCurricularNavigation { get; set; }
        public virtual ICollection<TbAvaliacao> TbAvaliacaos { get; set; }
    }
}
