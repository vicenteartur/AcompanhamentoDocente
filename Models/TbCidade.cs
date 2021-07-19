using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable disable

namespace AcompanhamentoDocente.Models
{
    public partial class TbCidade
    {
        public TbCidade()
        {
            TbEscolas = new HashSet<TbEscola>();
        }
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public int Codigo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public string Cidade { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
        public int CodigoEstado { get; set; }
        [JsonIgnore(Condition =JsonIgnoreCondition.Always)]
        public virtual TbEstado CodigoEstadoNavigation { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public virtual ICollection<TbEscola> TbEscolas { get; set; }
    }

 }
