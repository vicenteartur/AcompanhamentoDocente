using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbEscola
    {
        public TbEscola()
        {
            TbAtribuicaoColaboradorEscolas = new HashSet<TbAtribuicaoColaboradorEscola>();
        }

        public int Codigo { get; set; }
        public string Escola { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public int CodigoCidade { get; set; }
        public short Inep { get; set; }
        public byte Ativa { get; set; }

        public virtual TbCidade CodigoCidadeNavigation { get; set; }
        public virtual ICollection<TbAtribuicaoColaboradorEscola> TbAtribuicaoColaboradorEscolas { get; set; }
    }
}
