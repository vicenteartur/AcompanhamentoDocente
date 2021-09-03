using AcompanhamentoDocente.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface ICidade
    {
        Task<List<TbCidade>> ListaCidade();
        Task<TbCidade> Detalhes(int id);
        Task Inserir(TbCidade cidade);
        Task Atualizar(TbCidade cidade);
        Task Deletar(TbCidade cidade);

        Task<TbColaborador> MontarAdmin(int id);
        SelectList ListaEstado();
        SelectList ListaEstadoUp(TbCidade cidade);
        bool TbCidadeExists(int id);

    }
}