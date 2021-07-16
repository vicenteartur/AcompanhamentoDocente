using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbComponenteCurricular
    {
        public TbComponenteCurricular()
        {
            TbAtribuicaoComponenteCurricularAnoColaboradorEscolas = new HashSet<TbAtribuicaoComponenteCurricularAnoColaboradorEscola>();
        }

        public int Codigo { get; set; }
        public string ComponenteCurricular { get; set; }
        public string SubArea { get; set; }
        public byte Ativa { get; set; }

        public virtual ICollection<TbAtribuicaoComponenteCurricularAnoColaboradorEscola> TbAtribuicaoComponenteCurricularAnoColaboradorEscolas { get; set; }
    }
}
