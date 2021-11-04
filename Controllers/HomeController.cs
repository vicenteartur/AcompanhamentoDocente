using AcompanhamentoDocente.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Services;
using System;
using ClosedXML.Excel;
using AcompanhamentoDocente.ModelsRelatorio;
using System.Collections.Generic;
using System.IO;

namespace AcompanhamentoDocente.Controllers
{
    public class HomeController : Controller
    {
        
        
        private readonly IHome _acesso;

        public HomeController()
        {
            
            _acesso = new HomeService();
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> DashBoard(int id)
        {
            var col = await _acesso.MontarAdmin(id);
            ViewData["admin"] = col;
            var lista = await _acesso.ListaEscolasAtivas(id);
            return View(lista);
        }

        public async Task<IActionResult> GerenciarEscola(int id, int esc, int? ano)
        {
            var col = await _acesso.MontarAdmin(id);
            ViewData["admin"] = col;
            var escola = await _acesso.MontarEscola(esc, id);
            ViewData["escola"] = escola;
            

            if (ano == null) 
            {
                DateTime datatual = DateTime.Now;
                var anoatual = datatual.Year;

            var rel = await _acesso.RelatorioGeral(esc,anoatual);
                ViewData["ano"] = anoatual;
                return View(rel);
            }
            else
            {
            var rel = await _acesso.RelatorioGeral(esc,(int)ano);
                ViewData["ano"] = ano;
                return View(rel);
            }
            
            
        }

        public async Task<IActionResult> DetalhesSubArea(int id, int esc, string sub, int? ano)
        {

            var col = await _acesso.MontarAdmin(id);
            ViewData["admin"] = col;
            var escola = await _acesso.MontarEscola(esc, id);
            ViewData["escola"] = escola;
            ViewData["subarea"] = sub;


            if (ano == null)
            {
                DateTime datatual = DateTime.Now;
                var anoatual = datatual.Year;

                var rel = await _acesso.RelatorioSubArea(esc, sub, anoatual);
                ViewData["ano"] = anoatual;
                
                return View(rel);

            }
            else
            {

                var rel = await _acesso.RelatorioSubArea(esc, sub, (int)ano);
                ViewData["ano"] = ano;
                return View(rel);
            }
        }
            public async Task<IActionResult> DetalhesDisciplina(int id, int esc, int ccc, int? ano)
        {

            var col = await _acesso.MontarAdmin(id);
            ViewData["admin"] = col;
            var escola = await _acesso.MontarEscola(esc, id);
            ViewData["escola"] = escola;
            ViewData["ccc"] = ccc;
            


            if (ano == null)
            {
                DateTime datatual = DateTime.Now;
                var anoatual = datatual.Year;
                var rel = await _acesso.RelatorioDisciplina(esc, ccc, (int)anoatual);
                ViewData["ano"] = anoatual;
                return View(rel);
            }
            else
            {
                var rel = await _acesso.RelatorioDisciplina(esc, ccc, (int)ano);
                ViewData["ano"] = ano;
                return View(rel);
            }

            
            
        }

        public async Task<IActionResult> AvaliacaoImpressa(int id, int esc, string ccc, int ano)
        {
            var dados = await _acesso.RelatorioXLSXDisciplina (esc, ccc,ano);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Relatório Geral");
            var currentRow = 1;
            var currentCol = 1;

            var linha_inicio = currentRow;
            var coluna_inicio = currentCol;
            var linha_format = currentRow;
            var coluna_format = currentCol;


            var line = dados.OrderBy(c => c.Classificacao).First();

            worksheet.Cell(currentRow, currentCol).Value = "Escola";
            
            worksheet.Cell(currentRow, currentCol + 1).Value = line.Escola;
            currentRow++;
            worksheet.Cell(currentRow, currentCol).Value = "Componente";
            worksheet.Cell(currentRow, currentCol + 1).Value = line.ccurricular;
            string componentecurr = line.ccurricular;
            var linha_final = currentRow;
            var coluna_final = currentCol + 1;

            IXLRange range = worksheet.Range(worksheet.Cell(linha_format, coluna_format).Address, worksheet.Cell(linha_final, coluna_final).Address);

            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            currentRow = currentRow+3;
            linha_format = currentRow;
            coluna_format = currentCol+1;

            linha_inicio = currentRow;
            worksheet.Cell(currentRow, currentCol + 1).Value = "Professor";
            currentRow++;
            worksheet.Cell(currentRow, currentCol + 1).Value = "Avaliador";
            currentRow++;
            worksheet.Cell(currentRow, currentCol + 1).Value = "Data";
            currentRow++;
            worksheet.Cell(currentRow, currentCol + 1).Value = "Ano";
            
            linha_final = currentRow;
            
            range = worksheet.Range(worksheet.Cell(linha_format, coluna_format).Address, worksheet.Cell(linha_final, coluna_format).Address);

            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            currentRow++;
            linha_format = currentRow;
            coluna_format = currentCol;
            worksheet.Cell(currentRow, currentCol).Value = "Cod Critério";
            worksheet.Cell(currentRow, currentCol+1).Value = "Critério";
                       
            linha_final = currentRow;
            coluna_final = currentCol + 1;

            range = worksheet.Range(worksheet.Cell(linha_format, coluna_format).Address, worksheet.Cell(linha_final, coluna_final).Address);

            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


            currentRow++;

            linha_format = currentRow;
            coluna_format = currentCol;

            var tabelacriterio = new List<TbCriterioAvaliacao>();
            
            foreach (var item in dados)
            {
               
                if (!tabelacriterio.Any(c => c.Codigo == item.CodigoCriterio))
                {
                    worksheet.Cell(currentRow, currentCol).Value = item.CodigoCriterio;
                    worksheet.Cell(currentRow, currentCol + 1).Value = item.Criterio;
                    currentRow++;
                    var crit = new TbCriterioAvaliacao
                    {
                        Codigo = item.CodigoCriterio,
                        Criterio = item.Criterio
                    };

                    tabelacriterio.Add(crit);
                }
                
                

                

            }

            range = worksheet.Range(worksheet.Cell(linha_format, currentCol).Address, worksheet.Cell(currentRow - 1, currentCol+1).Address);

            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            currentRow = linha_inicio;
            currentCol = currentCol + 2;
            var listaauxiliar = new List<linha_plan_relatorio_xls>();
            var col = new List<linha_plan_relatorio_xls>();



            foreach (var item in dados)
            {
                if(!listaauxiliar.Any(av => av.CodigoAvaliacao == item.CodigoAvaliacao))
                {
                    listaauxiliar.Add(item);
                    worksheet.Cell(currentRow, currentCol).Value = item.Avaliado;
                    currentRow++;
                    worksheet.Cell(currentRow, currentCol).Value = item.Avaliador;
                    currentRow++;
                    worksheet.Cell(currentRow, currentCol).Value = item.dataavaliacao;
                    currentRow++;
                    worksheet.Cell(currentRow, currentCol).Value = item.anoturma;
                    currentRow++;
                    worksheet.Cell(currentRow, currentCol).Value = "Atende? (0/1)";
                    

                        range = worksheet.Range(worksheet.Cell(linha_inicio, currentCol).Address, worksheet.Cell(currentRow, currentCol).Address);

                        range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                        range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    
                    currentRow++;
                        
                        linha_format = currentRow;

                    col = dados.Where(av => av.CodigoAvaliacao == item.CodigoAvaliacao).ToList();

                    foreach (var itemcol in tabelacriterio)
                    {

                        

                        if (col.Any(l => l.CodigoCriterio == itemcol.Codigo))
                        {
                            var conceito = col.Where(c => c.CodigoCriterio == itemcol.Codigo).First();
                            worksheet.Cell(currentRow, currentCol).Value = conceito.Conceito;
                            currentRow++;
                        }
                        else
                        {
                            worksheet.Cell(currentRow, currentCol).Value = "-";
                            currentRow++;
                        }

                    }

                    range = worksheet.Range(worksheet.Cell(linha_format, currentCol).Address, worksheet.Cell(currentRow-1, currentCol).Address);

                    range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    currentCol++;

                    currentRow = linha_inicio;

                }

                

            }


            

            
            range = worksheet.Range(worksheet.Cell(1, 1).Address, worksheet.Cell(linha_final, coluna_final).Address);
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            

            for (int i = 1; i < coluna_final+1 ; i++)
            {
                worksheet.Column(i).AdjustToContents();
            }



            await using var memory = new MemoryStream();
            workbook.SaveAs(memory);

            return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "avaliacoes"+componentecurr+ano.ToString()+".xlsx");


        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Colaborador (int? id)
        //{
        //    return RedirectToAction("Index", "Colaborador", new { id = id }); ;
        //}

    }
}
