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

            var line = dados.OrderBy(c => c.Classificacao).First();

            worksheet.Cell(currentRow, currentCol).Value = "Escola";
            worksheet.Cell(currentRow, currentCol + 1).Value = line.Escola;
            currentRow++;
            worksheet.Cell(currentRow, currentCol).Value = "Componente";
            worksheet.Cell(currentRow, currentCol + 1).Value = line.ccurricular;
            linha_inicio = currentRow+1;
            
            currentRow = currentRow+4;
            
            worksheet.Cell(currentRow, currentCol).Value = "Cod Critério";
            worksheet.Cell(currentRow, currentCol+1).Value = "Critério";
            currentRow++;

            
            
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

            currentRow = linha_inicio;
            currentCol = currentCol + 2;

            foreach (var item in dados)
            {
                var amostra = dados.First();
                var col = dados.Where(av => av.CodigoAvaliacao == amostra.CodigoAvaliacao).ToList();
                
                worksheet.Cell(currentRow, currentCol).Value = amostra.Avaliado;
                currentRow++;
                worksheet.Cell(currentRow, currentCol).Value = amostra.Avaliador;
                currentRow++;
                worksheet.Cell(currentRow, currentCol).Value = amostra.dataavaliacao;
                currentRow++;
                worksheet.Cell(currentRow, currentCol).Value = amostra.anoturma;
                currentRow++;
                

                foreach (var itemcol in tabelacriterio)
                {

                    var critav = col.Where(c => c.CodigoCriterio == itemcol.Codigo).First();

                    if (critav != null)
                    {
                        worksheet.Cell(currentRow, currentCol).Value = critav.Conceito;
                        currentRow++;
                    }
                    else
                    {
                        worksheet.Cell(currentRow, currentCol).Value = "-";
                        currentRow++;
                    }

                }

                currentCol = currentCol++;
                currentRow = linha_inicio;

            }
            
            

            var linha_final = currentRow;
            var coluna_final = currentCol + 1;

            IXLRange range = worksheet.Range(worksheet.Cell(linha_inicio, coluna_inicio).Address, worksheet.Cell(linha_final, coluna_final).Address);

            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;

            currentRow = currentRow + 2;
            currentCol = 1;

           

            linha_inicio = currentRow;
            coluna_final = currentCol + 3;
            range = worksheet.Range(worksheet.Cell(linha_inicio, coluna_inicio).Address, worksheet.Cell(linha_final, coluna_final).Address);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            currentRow++;


           

            range = worksheet.Range(worksheet.Cell(linha_inicio, coluna_inicio).Address, worksheet.Cell(currentRow - 1, coluna_final).Address);

            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            range = worksheet.Range(worksheet.Cell(linha_inicio + 1, coluna_inicio).Address, worksheet.Cell(currentRow - 1, coluna_final).Address);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            range = worksheet.Range(worksheet.Cell(linha_inicio + 1, coluna_inicio + 2).Address, worksheet.Cell(currentRow - 1, coluna_final).Address);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            for (int i = 1; i < 100; i++)
            {
                worksheet.Column(i).AdjustToContents();
            }



            await using var memory = new MemoryStream();
            workbook.SaveAs(memory);

            return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "avaliacao.xlsx");


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
