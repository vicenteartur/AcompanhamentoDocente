using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ModelsRelatorio;
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
        Task<List<GraficoViewModel>> RelatorioGeral(int CodigoEscola, int ano);
        Task<List<GraficoViewModel>> RelatorioSubArea(int CodigoEscola, string sub, int ano);
        Task<List<GraficoViewModel>> RelatorioDisciplina(int CodigoEscola, int ccc, int ano);
        Task<List<Planilhas_Relatorio_Av>> RelatorioXLSXDisciplina(int CodigoEscola, int ccc, int ano);
    }
}
