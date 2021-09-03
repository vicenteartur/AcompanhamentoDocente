#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbCriterioComponenteCurricular
    {
        public int Codigo { get; set; }
        public int CodigoCriterioAvaliacao { get; set; }
        public int CodigoComponenteCurricular { get; set; }
        public int Ativa { get; set; }

        public virtual TbComponenteCurricular CodigoComponenteCurricularNavigation { get; set; }
        public virtual TbCriterioAvaliacao CodigoCriterioAvaliacaoNavigation { get; set; }
    }
}
