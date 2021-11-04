using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface ICriterioViewModel
    {

        Task<List<CriterioViewModel>> ListaCriterios(int ccur);
        Task<CriterioViewModel> Detalhes(int id);
        Task Inserir(List<CriterioViewModel> criterio);
        Task Atualizar(List<CriterioViewModel> criterio);
        Task Deletar(CriterioViewModel criterio);
        SelectList Classificacao();
        SelectList ClassificacaoUp(int classificacao);
        Task<TbColaborador> MontarAdmin(int id);
        Task<List<TbComponenteCurricular>> ListaCompCur();
        MultiSelectList CompCurri();
        MultiSelectList UpCompCurri(int crit);
        bool TbCriterioExists(int id);
        Task<TbComponenteCurricular> Comp(int ccurr);

    }
}
