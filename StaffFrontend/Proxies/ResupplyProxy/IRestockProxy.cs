using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.ResupplyProxy
{
    public interface IRestockProxy
    {

        Task<List<Restock>> GetRestocks();

        Task<Restock> GetRestock(int restockId);

        Task UpdateRestock(Restock restock);

        Task DeleteRestock(int restockId);

        Task CreateRestock(Restock restock);
    }
}
