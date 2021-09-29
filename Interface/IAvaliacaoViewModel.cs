using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using System.Collections.Generic;
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
        Task Deletar(int avaliacao);
        Task<TbColaborador> MontarAdmin(int id);
        Task<TbEscola> MontarEscola(int id);
        bool AvaliacaoExists(int id);
        Task<List<AvaliacaoViewModel>> ListaAvaliacoesFinalizadas(int esc);
        Task<List<AvaliacaoViewModel>> ListaAvaliacoesAtribuicao(int id, int esc, int atrib);

    }
}
