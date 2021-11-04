using AcompanhamentoDocente.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface ICargo
    {

        Task<List<TbCargo>> ListaCargo();
        Task<TbCargo> Detalhes(int id);
        Task Inserir(TbCargo cargo);
        Task Atualizar(TbCargo cargo);
        Task Deletar(TbCargo cargo);

        Task<TbColaborador> MontarAdmin(int id);
        bool TbCargoExists(int id);

    }
}
