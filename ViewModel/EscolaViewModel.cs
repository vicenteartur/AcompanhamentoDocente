using AcompanhamentoDocente.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AcompanhamentoDocente.ViewModel
{
    public class EscolaViewModel
    {
        //dbContext db = new dbContext();
        
        //public EscolaViewModel()
        //{
        //    estado = carregaestados();
        //    cidade = carregacidades();
        //    //colaborador = BuscaColaborador(id);

        //}

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
        
        public List<SelectListItem> cidade  { get; set; }
        public List<SelectListItem> estado { get; set; }

        //public List<SelectListItem> carregaestados()
        //{
        //    var lista = new List<SelectListItem>();
        //    var estados = db.TbEstados.ToList();

        //    try
        //    {
        //        foreach (var item in estados)
        //        {
        //            var option = new SelectListItem()
        //            {
        //                Text = item.Sigla,
        //                Value = item.Codigo.ToString(),
                        
        //            };

        //            lista.Add(option);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //    return lista;
        //}

        //public List<SelectListItem> upcarregaestados( int codigo)
        //{
        //    var lista = new List<SelectListItem>();
        //    var estados = db.tbEstadoes.ToList();

        //    try
        //    {
        //        foreach (var item in estados)
        //        {
        //            var option = new SelectListItem()
        //            {
        //                Text = item.Sigla,
        //                Value = item.Codigo.ToString(),
        //                Selected = (item.Codigo == Codigo)
        //            };

        //            lista.Add(option);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //    return lista;
        //}


        //public List<SelectListItem> upcidade(int codigo, int codigoEstado)
        //{
        //    var lista = new List<SelectListItem>();
        //    var cidades = (from c in db.tbCidades where c.CodigoEstado == codigoEstado select c).ToList<tbCidade>();

        //    try
        //    {
        //        foreach (var item in cidades)
        //        {
        //            var option = new SelectListItem()
        //            {
        //                Text = item.Cidade,
        //                Value = item.Codigo.ToString(),
        //                Selected = (item.Codigo == Codigo)
        //            };

        //            lista.Add(option);

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //    return lista;
        //}

        //public List<SelectListItem> carregacidades()
        //{
        //    var lista = new List<SelectListItem>();
        //    return lista;
        //}



        //public TbColaborador BuscaColaborador(int id)
        //{
        //    try
        //    {
        //        var perfil = new TbColaborador();
        //        perfil = (from c in db.TbColaboradors where (c.Codigo == id) select c).First<TbColaborador>();
        //        return perfil;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


            
        //}


    }
}
