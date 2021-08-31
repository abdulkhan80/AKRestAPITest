using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKTest.Data
{
    public class EntrantRepository : IEntrantRepository
    {
        public async Task<IQueryable<EntrantEntity>> GetEntrantRepository()
        {
            return new List<EntrantEntity>()
            {
                new EntrantEntity{ id=1, firstName="Entrant 1",lastName="result 1"},
                new EntrantEntity{ id=2, firstName="Entrant 2",lastName="result 2"},
                new EntrantEntity{ id=3, firstName="Entrant 3",lastName="result 3"},
                new EntrantEntity{ id=4, firstName="Entrant 4",lastName="result 4"},
                new EntrantEntity{ id=5, firstName="Entrant 5",lastName="result 55"},
            }.AsQueryable();
        }
    }
}
