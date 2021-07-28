using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbCargo
    {
        public TbCargo()
        {
            TbColaboradors = new HashSet<TbColaborador>();
        }

        public int Codigo { get; set; }
        public string Cargo { get; set; }
        public int NiveldeAcesso { get; set; }

        public virtual ICollection<TbColaborador> TbColaboradors { get; set; }
    }
}
