using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbColaborador
    {
        public TbColaborador()
        {
            TbAtribuicaoColaboradorEscolas = new HashSet<TbAtribuicaoColaboradorEscola>();
            TbAvaliacaos = new HashSet<TbAvaliacao>();
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? CodigoCargo { get; set; }
        public byte Ativo { get; set; }

        public virtual TbCargo CodigoCargoNavigation { get; set; }
        public virtual ICollection<TbAtribuicaoColaboradorEscola> TbAtribuicaoColaboradorEscolas { get; set; }
        public virtual ICollection<TbAvaliacao> TbAvaliacaos { get; set; }
    }
}
