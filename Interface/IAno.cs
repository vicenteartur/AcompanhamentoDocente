using AcompanhamentoDocente.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Interface
{
    interface IAno
    {

        Task<List<TbAno>> Index();
        Task<TbAno> Details(int? id);
        void Create(TbAno tbAno);
        void Edit(int id,TbAno tbAno);
        void Delete(TbAno ano);
        bool TbAnoExists(int id);
        Task<TbColaborador> MontarAdmin(int id);

    }
}
