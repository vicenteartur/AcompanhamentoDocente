using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbAtribuicaoColaboradorEscola
    {
        public TbAtribuicaoColaboradorEscola()
        {
            TbAtribuicaoComponenteCurricularAnoColaboradorEscolas = new HashSet<TbAtribuicaoComponenteCurricularAnoColaboradorEscola>();
        }
        [Key]
        public int Codigo { get; set; }
        public int CodigoEscola { get; set; }
        public int CodigoColaborador { get; set; }
        public byte Ativa { get; set; }

        public virtual TbColaborador CodigoColaboradorNavigation { get; set; }
        public virtual TbEscola CodigoEscolaNavigation { get; set; }
        public virtual ICollection<TbAtribuicaoComponenteCurricularAnoColaboradorEscola> TbAtribuicaoComponenteCurricularAnoColaboradorEscolas { get; set; }
    }
}
