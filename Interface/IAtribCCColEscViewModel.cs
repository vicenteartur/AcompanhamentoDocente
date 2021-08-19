using AcompanhamentoDocente.Models;
using AcompanhamentoDocente.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IAtribCCColEscViewModel
    {

        Task<TbColaborador> MontarAdmin(int id);
        Task<TbEscola> MontarEsc(int id);
        Task<List<SelectListItem>> ListaAno(int codano, int mod);
        Task<List<SelectListItem>> ListaCCurricular(int codcc, int mod);
        Task<List<ColaboradorViewModel>> ListaProfessores(int escola);
        Task<List<AtribCCColEscViewModel>> ListaAtribuicao(int id, int esc);
        Task<AtribCCColEscViewModel> Detalhes(int id);
        Task Inserir(AtribCCColEscViewModel atribuicao);
        Task Atualizar(AtribCCColEscViewModel atribuicao);
        Task Deletar(AtribCCColEscViewModel atribuicao);
        bool TbAtribExists(int codat);
        Task<TbAtribuicaoColaboradorEscola> BuscaAtrib(int id, int esc);
        SelectList ListaModalidade();
        SelectList ListaModalidadeUp(AtribCCColEscViewModel atribuicao);
    }
}
