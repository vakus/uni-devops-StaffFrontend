using StaffFrontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffFrontend.Proxies.RestockProxy
{
    public class RestockProxyLocal : IRestockProxy
    {
        private List<Restock> restocks;

        public RestockProxyLocal()
        {
            restocks = new List<Restock>
            {
                new Restock(){restockId="1", supplierId="1", sProductId="1", quantity=4, price=4.99m, approved=false, date=DateTime.MinValue}
            };
        }

        public RestockProxyLocal(List<Restock> restocks)
        {
            this.restocks = restocks;
        }

        public Task CreateRestock(Restock restock)
        {
            return Task.Run(() =>
            {
                restocks.Add(restock);
            });
        }

        public Task DeleteRestock(int restockId)
        {
            return Task.Run(() =>
            {
                restocks.RemoveAll(s => s.restockId == restockId.ToString());
            });
        }

        public Task<List<Restock>> GetRestocks()
        {
            return Task.FromResult(restocks);
        }

        public Task<Restock> GetRestock(int restockId)
        {
            return Task.FromResult(restocks.FirstOrDefault(s => s.restockId == restockId.ToString()));
        }

        public Task UpdateRestock(Restock restock)
        {
            return Task.Run(() =>
            {
                if (restocks.Where(r => r.restockId == restock.restockId).Count() != 0)
                {
                    restocks.RemoveAll(s => s.restockId == restock.restockId);
                    restocks.Add(restock);
                }
            });
        }
    }
}
