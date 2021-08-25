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
        Task<List<AvaliacaoViewModel>> ListaAvaliacoes(int esc);
        Task<AvaliacaoViewModel> Detalhes(int adm, int atrib, int aval);
        Task Inserir(AvaliacaoViewModel avaliacao);
        Task Atualizar(TbCriterioAvaliado avaliacao);
        Task Deletar(AvaliacaoViewModel avaliacao);
        Task<TbColaborador> MontarAdmin(int id);
        Task<TbEscola> MontarEscola(int id);
        bool AvaliacaoExists(int id);

    }
}
