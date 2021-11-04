using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbAno
    {
        public TbAno()
        {
            TbAtribuicaoComponenteCurricularAnoColaboradorEscolas = new HashSet<TbAtribuicaoComponenteCurricularAnoColaboradorEscola>();
        }

        public int Codigo { get; set; }
        public string Ano { get; set; }
        public string Turma { get; set; }
        public int CodigoModalidade { get; set; }
        public string Periodo { get; set; }

        public virtual TbModalidade CodigoModalidadeNavigation { get; set; }
        public virtual ICollection<TbAtribuicaoComponenteCurricularAnoColaboradorEscola> TbAtribuicaoComponenteCurricularAnoColaboradorEscolas { get; set; }
    }
}
