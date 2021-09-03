using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AcompanhamentoDocente.ViewModel
{
    public class AtribCCColEscViewModel
    {

        public int Codigo { get; set; }
        public int CodigoAtribuicaoColaboradorEscola { get; set; }
        public int CodigoAno { get; set; }
        public int CodigoCC { get; set; }
        public int CodigoColaborador { get; set; }
        public int CodigoModalidade { get; set; }
        public string Modalidade { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int CodigoCargo { get; set; }
        public string Cargo { get; set; }
        public int NiveldeAcesso { get; set; }
        public byte Ativo { get; set; }
        public List<SelectListItem> ano { get; set; }
        public string Ano { get; set; }
        public List<SelectListItem> CCurricular { get; set; }
        public string CompCurr { get; set; }
        public int CodigoEscola { get; set; }
        public string NomeEscola { get; set; }
        public int CodigoAdministrador { get; set; }
        public string NomeAdministrador { get; set; }
        public int CodigoCargoAdministrador { get; set; }
        public string CargoAdministrador { get; set; }
        public int NiveldeAcessoAdministrador { get; set; }

    }
}
