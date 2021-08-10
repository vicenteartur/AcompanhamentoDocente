using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.ViewModel
{
    public class AtribuicaoViewModel
    {
        public int CodigoAno { get; set; }
        public string Ano { get; set; }
        public string Turma { get; set; }
        public string Modalidade { get; set; }
        public string Periodo { get; set; }
        public int CodigoCC { get; set; }
        public string ComponenteCurricular { get; set; }
        public string SubArea { get; set; }
        public byte Ativa { get; set; }
        public int CodigoEscola { get; set; }
        public string NomeEscola { get; set; }
        public int CodigoAdministrador { get; set; }
        public string NomeAdministrador { get; set; }
        public int CodigoCargoAdministrador { get; set; }
        public string CargoAdministrador { get; set; }
        public int NiveldeAcessoAdministrador { get; set; }

    }
}
