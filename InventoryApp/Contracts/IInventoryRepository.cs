using SampleDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleDapper.Contracts
{
    public interface IInventoryRepository
    {
        public Task<IEnumerable<Inventory>> GetInventory();

        public Task<int> AddNewItem(Inventory inventory);

        public Task DeleteInventoryById(int id);

        public Task UpdateInventory(int id, Inventory inventory);
    }
}
