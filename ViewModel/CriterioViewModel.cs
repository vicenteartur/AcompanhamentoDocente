using Microsoft.AspNetCore.Mvc.Rendering;

namespace AcompanhamentoDocente.ViewModel
{
    public class CriterioViewModel
    {

        public int Codigo { get; set; }
        public string Criterio { get; set; }
        public int CodigoClassificacaoCriterio { get; set; }
        public byte Ativa { get; set; }
        public SelectList ClassificacaoCriterio { get; set; }
        public int CodigoCCUrricular { get; set; }
        public MultiSelectList CCurricular { get; set; }
        public int CodigoModalidade { get; set; }
        public SelectList Modalidade { get; set; }
        public string clcriterio { get; set; }

    }
}
