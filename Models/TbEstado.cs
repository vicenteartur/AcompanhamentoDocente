using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbEstado
    {
        public TbEstado()
        {
            TbCidades = new HashSet<TbCidade>();
        }

        public int Codigo { get; set; }
        public string Estado { get; set; }
        public string Sigla { get; set; }

        public virtual ICollection<TbCidade> TbCidades { get; set; }
    }
}
