using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;




namespace AcompanhamentoDocente.Interface
{
    interface IEscolaViewModel
    {

        Task<TbColaborador> MontarColaborador(int CodigoColaborador);
        Task<EscolaViewModel> MontarEscola(int CodigoEscola, int CodigoColaborador);
        Task<List<EscolaViewModel>> ListaEscolasAtivas(int CodigoColaborador);
        Task<List<EscolaViewModel>> ListaEscolasInativas(int CodigoColaborador);
        Task<List<SelectListItem>> ListaCidades(int CodigoEstado, int CodigoCidade);
        Task<List<SelectListItem>> ListaEstados(int CodigoEstado);
        Task InserirEscola(EscolaViewModel Escola);
        Task AtualizarEscola(EscolaViewModel Escola);
        Task RemoverEscola(EscolaViewModel Escola);
        Task ApagarEscola(EscolaViewModel Escola);
        void Salvar();

    }
}
