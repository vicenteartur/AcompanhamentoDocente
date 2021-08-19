using System;
using System.Collections.Generic;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbModalidade
    {
        public TbModalidade()
        {
            TbAnos = new HashSet<TbAno>();
            TbComponenteCurriculars = new HashSet<TbComponenteCurricular>();
        }

        public int Codigo { get; set; }
        public string Modalidade { get; set; }

        public virtual ICollection<TbAno> TbAnos { get; set; }
        public virtual ICollection<TbComponenteCurricular> TbComponenteCurriculars { get; set; }
    }
}
