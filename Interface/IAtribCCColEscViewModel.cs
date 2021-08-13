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
        SelectList ListaAno();
        SelectList ListaCCurricular();
        Task<List<ColaboradorViewModel>> ListaProfessores(int escola);
        Task<List<AtribCCColEscViewModel>> ListaAtribuicao();
        Task<AtribCCColEscViewModel> Detalhes(int id);
        Task Inserir(AtribCCColEscViewModel atribuicao);
        Task Atualizar(AtribCCColEscViewModel atribuicao);
        Task Deletar(AtribCCColEscViewModel atribuicao);

    }
}
