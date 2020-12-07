using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ResupplyProxy
{
    public class RestockProxyLocal : IRestockProxy
    {
        public Task CreateRestock(Restock resupply)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRestock(int resupplyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Restock>> GetRestocks()
        {
            throw new NotImplementedException();
        }

        public Task<Restock> GetRestock(int resupplyId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRestock(Restock resupply)
        {
            throw new NotImplementedException();
        }
    }
}
