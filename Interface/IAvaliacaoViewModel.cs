using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IAvaliacaoViewModel
    {
        Task<List<AvaliacaoViewModel>> ListaAvaliacoes(int id, int esc);
        Task<AvaliacaoViewModel> Detalhes(int adm, int atrib, int aval);
        Task<AvaliacaoViewModel> Inserir(AvaliacaoViewModel avaliacao);
        Task AtualizaCritAv(TbCriterioAvaliado avaliacao);
        Task Atualizar(TbAvaliacao avaliacao);
        Task<TbCriterioAvaliado> MontarCritAv(int avaliacao);
        Task Deletar(AvaliacaoViewModel avaliacao);
        Task<TbColaborador> MontarAdmin(int id);
        Task<TbEscola> MontarEscola(int id);
        bool AvaliacaoExists(int id);
        Task<List<AvaliacaoViewModel>> ListaAvaliacoesFinalizadas(int esc);

    }
}
