using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IHome
    {

        Task<TbColaborador> MontarAdmin(int id);
        Task<List<EscolaViewModel>> ListaEscolasAtivas(int CodigoColaborador);
        Task<EscolaViewModel> MontarEscola(int CodigoEscola, int CodigoColaborador);
        Task<List<GraficoViewModel>> RelatorioGeral(int CodigoEscola);
        Task<List<GraficoViewModel>> RelatorioSubArea(int CodigoEscola, string sub);
        Task<List<GraficoViewModel>> RelatorioDisciplina(int CodigoEscola, int ccc);
    }
}
