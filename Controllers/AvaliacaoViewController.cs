using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.Services;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.IO;

namespace AcompanhamentoDocente.Controllers
{
    public class AvaliacaoViewController : Controller
    {

        private readonly IAvaliacaoViewModel _avaliacao;

        public AvaliacaoViewController()
        {
            _avaliacao = new AvaliacaoViewModelService();
        }

        // GET: AvaliacaoViewController
        public async Task<ActionResult> Index(int? id, int? esc)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);

            return View(await _avaliacao.ListaAvaliacoes((int)id, (int)esc));
        }

        public async Task<ActionResult> AvFinalizadas(int? id, int? esc)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);
            var lista = await _avaliacao.ListaAvaliacoesFinalizadas((int)esc);
            return View(lista);
        }

        // GET: AvaliacaoViewController/Details/5
        public async Task<ActionResult> Details(int? id, int? esc, int? aval, int? atrib)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);

            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // GET: AvaliacaoViewController/Create
        public async Task<ActionResult> Create(int? id, int? esc, int? atrib)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (atrib == null)
            {
                return NotFound();
            }


            var existe = new TbAvaliacao();
            existe = await _avaliacao.AvaliacaoExisteAberta((int)atrib);

            if ( existe != null)
            {
                return RedirectToAction("Edit", new { id = id, esc = esc, atrib = atrib, aval = existe.Codigo });
            }

            else
            {

                    var avaliacao = new AvaliacaoViewModel
                    {
                        CodigoColaboradorAvaliador = (int)id,
                        CodigoACECCA = (int)atrib
                    };

                    var aval = await _avaliacao.Inserir(avaliacao);

                    return RedirectToAction("Edit", new { id = id, esc = esc, atrib = atrib, aval = aval.Codigo });
            }
            
        }

        // GET: AvaliacaoViewController/Edit/5
        public async Task<ActionResult> Edit(int? id, int? esc, int? atrib, int? aval)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }

            if (atrib == null)
            {
                return NotFound();
            }

            if (aval == null)
            {
                return NotFound();
            }

            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);
            ViewData["atrib"] = aval;

            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // POST: AvaliacaoViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, int? esc, int? atrib, int? aval, [Bind("Codigo,CodigoColaboradorAvaliador,CodigoACECCA,dataavaliacao")] AvaliacaoViewModel avaliacao)
        {
            var admin = await _avaliacao.MontarAdmin((int)id);

            if (ModelState.IsValid)
            {
                try
                {
                    var ataval = new TbAvaliacao
                    {
                        Codigo = avaliacao.Codigo,
                        Datarealizacao = avaliacao.dataavaliacao,
                        CodigoAtribuicaoComponenteCurricularAnoColaboradorEscola = avaliacao.CodigoACECCA,
                        CodigoColaboradorAvaliador = avaliacao.CodigoColaboradorAvaliador,
                        Finalizada = 1
                    };

                    await _avaliacao.Atualizar(ataval);

                }
                catch (Exception)
                {
                    if (!_avaliacao.AvaliacaoExists(avaliacao.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = id, esc = esc });
            }
            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // GET: AvaliacaoViewController/Delete/5
        public async Task<ActionResult> Delete(int? id, int? esc, int? atrib, int? aval)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }

            if (atrib == null)
            {
                return NotFound();
            }

            if (aval == null)
            {
                return NotFound();
            }

            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);
            ViewData["atrib"] = aval;

            return View(await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval));
        }

        // POST: AvaliacaoViewController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id, int? esc, int? Codigo)
        {

            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }

            if (Codigo == null)
            {
                return NotFound();
            }

            try
            {
                await _avaliacao.Deletar((int)Codigo);
                return RedirectToAction("Index", new { id = id, esc = esc });
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> AtualizaCriAv(int? id)
        {
            if (id != null)
            {
                TbCriterioAvaliado critavl = await _avaliacao.MontarCritAv((int)id);
                return PartialView(critavl);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public void AtualizaCriAv([Bind("Codigo,CodigoAvaliacao,CodigoCriterioAvaliacao,Conceito,Comentario")] TbCriterioAvaliado critavaliado)
        {

            _avaliacao.AtualizaCritAv(critavaliado);

        }

        public async Task<ActionResult> Avaliar(int? id, int? esc)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (esc == null)
            {
                return NotFound();
            }


            return RedirectToAction("Index", "AtribCColEscView", new { id = id, esc = esc });

        }

        public async Task<ActionResult> ListaAbertaAvaliacaoAtribuicao(int? id, int? esc, int? atrib)
        {
            ViewData["admin"] = await _avaliacao.MontarAdmin((int)id);
            ViewData["escola"] = await _avaliacao.MontarEscola((int)esc);
            ViewData["atrib"] = (int)atrib;

            return View(await _avaliacao.ListaAvaliacoesAtribuicao((int)id, (int)esc,(int)atrib));
        }

        public async Task<IActionResult> AvaliacaoImpressa(int? id, int? esc, int? aval, int? atrib)
        {
            var dados = await _avaliacao.Detalhes((int)id, (int)atrib, (int)aval);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Avaliacao");
            var currentRow = 1;
            var currentCol = 1;

            var linha_inicio = currentRow;
            var coluna_inicio = currentCol;

            

            worksheet.Cell(currentRow, currentCol).Value = "Avaliador";
            worksheet.Cell(currentRow, currentCol+1).Value = dados.NomeAvaliador;
            currentRow++;
            worksheet.Cell(currentRow, currentCol).Value = "Colaborador Avaliado";
            worksheet.Cell(currentRow, currentCol + 1).Value = dados.NomeColaborador;
            currentRow++;
            worksheet.Cell(currentRow, currentCol).Value = "Data de Criação";
            worksheet.Cell(currentRow, currentCol + 1).Value = dados.dataavaliacao.ToShortDateString();
            currentRow++;
            worksheet.Cell(currentRow, currentCol).Value = "Componente";
            worksheet.Cell(currentRow, currentCol + 1).Value = dados.ccurric;
            currentRow++;
            worksheet.Cell(currentRow, currentCol).Value = "Turma";
            worksheet.Cell(currentRow, currentCol + 1).Value = dados.ano;

            var linha_final = currentRow;
            var coluna_final = currentCol + 1;

            IXLRange range = worksheet.Range(worksheet.Cell(linha_inicio,coluna_inicio).Address, worksheet.Cell(linha_final,coluna_final).Address);

            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;

            currentRow = currentRow+2;
            currentCol = 1;

            worksheet.Cell(currentRow, currentCol).Value = "Classificaçao";
            worksheet.Cell(currentRow, currentCol+1).Value = "Critério";
            worksheet.Cell(currentRow, currentCol+2).Value = "Atende?";
            worksheet.Cell(currentRow, currentCol + 3).Value = "Comentário";
            
            linha_inicio = currentRow;
            coluna_final = currentCol + 3;
            range = worksheet.Range(worksheet.Cell(linha_inicio, coluna_inicio).Address, worksheet.Cell(linha_final, coluna_final).Address);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            currentRow++;
            

            foreach (var item in dados.CriterioAvaliado)
            {
                worksheet.Cell(currentRow, currentCol).Value = item.CodigoCriterioAvaliacaoNavigation.CodigoClassificacaoCriterioNavigation.Classificacao;
                worksheet.Cell(currentRow, currentCol + 1).Value = item.CodigoCriterioAvaliacaoNavigation.Criterio;
                if(item.Conceito == 1) 
                { 
                    worksheet.Cell(currentRow, currentCol + 2).Value = "SIM";
                }
                else
                {
                    worksheet.Cell(currentRow, currentCol + 2).Value = "NÃO";
                }
                worksheet.Cell(currentRow, currentCol + 3).Value = item.Comentario;
                currentRow++;
            }

            range = worksheet.Range(worksheet.Cell(linha_inicio, coluna_inicio).Address, worksheet.Cell(currentRow-1, coluna_final).Address);
            
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            range = worksheet.Range(worksheet.Cell(linha_inicio+1, coluna_inicio).Address, worksheet.Cell(currentRow-1, coluna_final).Address);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Justify;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            range = worksheet.Range(worksheet.Cell(linha_inicio + 1, coluna_inicio+2).Address, worksheet.Cell(currentRow - 1, coluna_final).Address);
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            for (int i = 1; i < 100; i++)
            {
                worksheet.Column(i).AdjustToContents();
            }

            
            
            await using var memory = new MemoryStream();
            workbook.SaveAs(memory);

            return File(memory.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "avaliacao.xlsx");

            
        }

    }
}
