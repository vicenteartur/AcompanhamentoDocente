using AcompanhamentoDocente.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AcompanhamentoDocente.ViewModel
{
    public class EscolaViewModel
    {

        [Key]
        public int Codigo { get; set; }

        [Required]
        [StringLength(50)]
        public string Escola { get; set; }

        [Required]
        [StringLength(50)]
        public string Rua { get; set; }

        [Required]
        [StringLength(50)]
        public string Bairro { get; set; }

        public int CodigoCidade { get; set; }

        public int INEP { get; set; }

        public byte Ativa { get; set; }

        public int CodigoEstado { get; set; }

        public int CodigoColaborador { get; set; }
        public string nomeCidade { get; set; }
        public string sigla { get; set; }
        public TbColaborador colaborador { get; set; }

        public List<SelectListItem> cidade { get; set; }
        public List<SelectListItem> estado { get; set; }




    }
}
