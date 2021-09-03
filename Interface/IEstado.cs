using AcompanhamentoDocente.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IEstado
    {
        Task<List<TbEstado>> ListaEstado();
        Task<TbEstado> Detalhes(int id);
        Task Inserir(TbEstado estado);
        Task Atualizar(TbEstado estado);
        Task Deletar(TbEstado estado);

        Task<TbColaborador> MontarAdmin(int id);
        bool TbEstadoExists(int id);

    }
}
