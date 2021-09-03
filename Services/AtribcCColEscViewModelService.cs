using AcompanhamentoDocente.Interface;
using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Services
{
    public class AtribcCColEscViewModelService : IAtribCCColEscViewModel
    {

        private dbContext db = new dbContext();

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors.Include(c => c.CodigoCargoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public async Task<TbEscola> MontarEsc(int id)
        {
            var tbescola = await db.TbEscolas
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbescola;
        }
        public async Task<TbAtribuicaoColaboradorEscola> BuscaAtrib(int id, int esc)
        {
            var atribuicao = await db.TbAtribuicaoColaboradorEscolas
                .FirstAsync(m => m.CodigoColaborador == id && m.CodigoEscola == esc);

            return atribuicao;
        }

        public async Task<List<ColaboradorViewModel>> ListaProfessores(int escola)
        {
            var colaborador = await (from cg in db.TbCargos
                                     join c in db.TbColaboradors on cg.Codigo equals c.CodigoCargo
                                     join at in db.TbAtribuicaoColaboradorEscolas on c.Codigo equals at.CodigoColaborador
                                     join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                                     where cg.NiveldeAcesso == 0 && at.CodigoEscola == escola && c.Ativo == 1
                                     orderby c.Nome
                                     select new ColaboradorViewModel
                                     {
                                         Codigo = c.Codigo,
                                         Nome = c.Nome,
                                         Email = c.Email,
                                         Ativo = c.Ativo,
                                         NiveldeAcesso = cg.NiveldeAcesso,
                                         Cargo = cg.Cargo
                                     }).ToListAsync();
            return colaborador;
        }

        public async Task<List<SelectListItem>> ListaAno(int codano, int mod)
        {
            var ano = await db.TbAnos.Include(a => a.CodigoModalidadeNavigation).Where(a => a.CodigoModalidade == mod).OrderBy(a => a.Codigo).ToListAsync();

            if (codano != 0)
            {
                var lista = new List<SelectListItem>();

                try
                {
                    foreach (var item in ano)
                    {

                        string opcao = $"{item.Ano + item.Turma + " - " + item.CodigoModalidadeNavigation.Modalidade + " - " + item.Periodo}";
                        var option = new SelectListItem()
                        {
                            Text = opcao,
                            Value = item.Codigo.ToString(),
                            Selected = (item.Codigo == codano)
                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;

            }

            else
            {
                var lano = await db.TbAnos.Include(a => a.CodigoModalidadeNavigation).Where(a => a.CodigoModalidade == mod).OrderBy(a => a.Codigo).ToListAsync();
                var lista = new List<SelectListItem>();


                try
                {

                    foreach (var item in lano)
                    {
                        string opcao = $"{item.Ano + item.Turma + " - " + item.CodigoModalidadeNavigation.Modalidade + " - " + item.Periodo}";
                        var option = new SelectListItem()
                        {
                            Text = opcao,
                            Value = item.Codigo.ToString(),

                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;
            }
        }

        public async Task<List<SelectListItem>> ListaCCurricular(int codcc, int mod)
        {
            var ano = await db.TbComponenteCurriculars.Include(m => m.CodigoModalidadeNavigation).Where(m => m.CodigoModalidade == mod).OrderBy(a => a.Codigo).ToListAsync();

            if (codcc != 0)
            {
                var lista = new List<SelectListItem>();

                try
                {
                    foreach (var item in ano)
                    {


                        var option = new SelectListItem()
                        {
                            Text = item.ComponenteCurricular,
                            Value = item.Codigo.ToString(),
                            Selected = (item.Codigo == codcc)
                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;

            }

            else
            {
                var lcodcc = await db.TbComponenteCurriculars.Include(m => m.CodigoModalidadeNavigation).Where(m => m.CodigoModalidade == mod).OrderBy(a => a.Codigo).ToListAsync();
                var lista = new List<SelectListItem>();


                try
                {

                    foreach (var item in lcodcc)
                    {

                        var option = new SelectListItem()
                        {
                            Text = item.ComponenteCurricular,
                            Value = item.Codigo.ToString(),

                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;
            }
        }

        public async Task Atualizar(AtribCCColEscViewModel atribuicao)
        {
            var atrib = new TbAtribuicaoComponenteCurricularAnoColaboradorEscola()
            {
                Codigo = atribuicao.Codigo,
                CodigoAtribuicaoColaboradorEscola = atribuicao.CodigoAtribuicaoColaboradorEscola,
                CodigoComponenteCurricular = atribuicao.CodigoCC,
                CodigoAno = atribuicao.CodigoAno
            };

            db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.Update(atrib);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(AtribCCColEscViewModel atribuicao)
        {
            var atrib = new TbAtribuicaoComponenteCurricularAnoColaboradorEscola()
            {
                Codigo = atribuicao.Codigo,
                CodigoAtribuicaoColaboradorEscola = atribuicao.CodigoAtribuicaoColaboradorEscola,
                CodigoComponenteCurricular = atribuicao.CodigoCC,
                CodigoAno = atribuicao.CodigoAno
            };

            db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.Remove(atrib);
            await db.SaveChangesAsync();
        }

        public async Task<AtribCCColEscViewModel> Detalhes(int id)
        {
            var atribfinal = await (from atc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                                    join at in db.TbAtribuicaoColaboradorEscolas on atc.CodigoAtribuicaoColaboradorEscola equals at.Codigo
                                    join c in db.TbColaboradors.Include(cg => cg.CodigoCargoNavigation) on at.CodigoColaborador equals c.Codigo
                                    join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                                    join cc in db.TbComponenteCurriculars on atc.CodigoComponenteCurricular equals cc.Codigo
                                    join ano in db.TbAnos on atc.CodigoAno equals ano.Codigo
                                    join m in db.TbModalidades on ano.CodigoModalidade equals m.Codigo


                                    where atc.Codigo == id

                                    select new AtribCCColEscViewModel
                                    {
                                        Codigo = atc.Codigo,
                                        CodigoAtribuicaoColaboradorEscola = at.Codigo,
                                        CodigoAno = ano.Codigo,
                                        CodigoCC = cc.Codigo,
                                        CodigoColaborador = c.Codigo,
                                        Nome = c.Nome,
                                        CodigoCargo = c.CodigoCargoNavigation.Codigo,
                                        Email = c.Email,
                                        Cargo = c.CodigoCargoNavigation.Cargo,
                                        NiveldeAcesso = c.CodigoCargoNavigation.NiveldeAcesso,
                                        CodigoEscola = e.Codigo,
                                        NomeEscola = e.Escola,
                                        CompCurr = cc.ComponenteCurricular,
                                        CodigoModalidade = m.Codigo,
                                        Modalidade = m.Modalidade,
                                        Ano = $"{ano.Ano + ano.Turma + " - " + m.Modalidade + " - " + ano.Periodo}"

                                    }).FirstAsync();

            return atribfinal;
        }

        public async Task Inserir(AtribCCColEscViewModel atribuicao)
        {
            var atrib = new TbAtribuicaoComponenteCurricularAnoColaboradorEscola()
            {
                Codigo = atribuicao.Codigo,
                CodigoAtribuicaoColaboradorEscola = atribuicao.CodigoAtribuicaoColaboradorEscola,
                CodigoComponenteCurricular = atribuicao.CodigoCC,
                CodigoAno = atribuicao.CodigoAno,
                Ativa = 1
            };

            await db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.AddAsync(atrib);
            await db.SaveChangesAsync();
        }

        public async Task<List<AtribCCColEscViewModel>> ListaAtribuicao(int id, int esc)
        {
            var atribfinal = await (from atc in db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas
                                    join at in db.TbAtribuicaoColaboradorEscolas on atc.CodigoAtribuicaoColaboradorEscola equals at.Codigo
                                    join c in db.TbColaboradors.Include(cg => cg.CodigoCargoNavigation) on at.CodigoColaborador equals c.Codigo
                                    join e in db.TbEscolas on at.CodigoEscola equals e.Codigo
                                    join cc in db.TbComponenteCurriculars on atc.CodigoComponenteCurricular equals cc.Codigo
                                    join ano in db.TbAnos on atc.CodigoAno equals ano.Codigo
                                    join m in db.TbModalidades on ano.CodigoModalidade equals m.Codigo

                                    where at.CodigoEscola == esc && c.CodigoCargoNavigation.NiveldeAcesso < 1

                                    select new AtribCCColEscViewModel
                                    {
                                        Codigo = atc.Codigo,
                                        CodigoAtribuicaoColaboradorEscola = at.Codigo,
                                        CodigoAno = ano.Codigo,
                                        CodigoCC = cc.Codigo,
                                        CodigoColaborador = c.Codigo,
                                        Nome = c.Nome,
                                        CodigoCargo = c.CodigoCargoNavigation.Codigo,
                                        Cargo = c.CodigoCargoNavigation.Cargo,
                                        NiveldeAcesso = c.CodigoCargoNavigation.NiveldeAcesso,
                                        Ano = $"{ano.Ano + ano.Turma + " - " + m.Modalidade + " - " + ano.Periodo}",
                                        CompCurr = cc.ComponenteCurricular,
                                        CodigoEscola = e.Codigo,
                                        NomeEscola = e.Escola
                                    }).ToListAsync();

            return atribfinal;
        }

        public bool TbAtribExists(int codat)
        {
            return db.TbAtribuicaoComponenteCurricularAnoColaboradorEscolas.Any(e => e.Codigo == codat);
        }

        public SelectList ListaModalidade()
        {
            var lista = new SelectList(db.TbModalidades, "Codigo", "Modalidade");

            return lista;
        }

        public SelectList ListaModalidadeUp(AtribCCColEscViewModel atribuicao)
        {
            var lista = new SelectList(db.TbModalidades, "Codigo", "Modalidade", atribuicao.CodigoModalidade);
            return lista;
        }
    }
}
