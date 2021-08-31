using AKTest.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AKTest.Business.Services
{
    public interface IEntrantService
    {
        Task<List<Entrants>> GetEntrantAll();
        Task<Entrants> GetEntrantById(int? id);
        Task PostEntrant(Entrants entrants);
        Task DeleteEntrant(int? id);
    }
}
