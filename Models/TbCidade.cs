using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbCidade
    {
        public TbCidade()
        {
            TbEscolas = new HashSet<TbEscola>();
        }

        public int Codigo { get; set; }
        public string Cidade { get; set; }
        public int CodigoEstado { get; set; }

        public virtual TbEstado CodigoEstadoNavigation { get; set; }
        public virtual ICollection<TbEscola> TbEscolas { get; set; }
    }
}
