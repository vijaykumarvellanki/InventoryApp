using Dapper;
using SampleDapper.Context;
using SampleDapper.Contracts;
using SampleDapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace SampleDapper.Repository
{
    public class InventoryRepository: IInventoryRepository
    {
        private readonly DapperContext _context;

        public InventoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventory>> GetInventory()
        {
            var query = "select * from Inventory";

            using(var connection = _context.CreateConnection())
            {
                var inventories = await connection.QueryAsync<Inventory>(query);
                return inventories.ToList();
            }
        }

        public async Task<int> AddNewItem(Inventory inventory)
        {
            var query = "insert into Inventory (Name, Description, Price, Quantity, Location) values (@Name, @Address, @Country)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", inventory.Name, DbType.String);
            parameters.Add("Description", inventory.Description, DbType.String);
            parameters.Add("Price", inventory.Price, DbType.Decimal);
            parameters.Add("Quantity", inventory.Quantity, DbType.Int32);
            parameters.Add("Location", inventory.Location, DbType.String);

            using (var connection = _context.CreateConnection())
            {
              int id = await connection.ExecuteAsync(query, parameters);
              return id;
            }
        }

        public async Task DeleteInventoryById(int id)
        {
            var query = "DELETE FROM Inentory WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }

        public async Task UpdateInventory(int id, Inventory inventory)
        {
            var query = "UPDATE Inventory SET Name = @Name, Description=@Description, Price=@Price, Quantit@Quantity, Location=@Location WHERE Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", inventory.Name, DbType.String);
            parameters.Add("Description", inventory.Description, DbType.String);
            parameters.Add("Price", inventory.Price, DbType.Decimal);
            parameters.Add("Quantity", inventory.Quantity, DbType.Int32);
            parameters.Add("Location", inventory.Location, DbType.String);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
