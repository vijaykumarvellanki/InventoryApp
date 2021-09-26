using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SampleDapper.Contracts;
using SampleDapper.Controllers;
using SampleDapper.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventorySystem.Tests
{
    [TestClass]
    public class InventoryControllerTests
    {
        private MockRepository _mockRepository;
        private Mock<IInventoryRepository> _mockInventoryRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict);
            _mockInventoryRepository = _mockRepository.Create<IInventoryRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockRepository.VerifyAll();
        }

        private InventoryController CreateInventoryController()
        {
            return new InventoryController(_mockInventoryRepository.Object);
        }

        [TestMethod]
        public async Task GetInventory_WhenMethodCalled_ReturnsInventories()
        {
            List<Inventory> expectedResult = new List<Inventory>()
            {
                new Inventory()
                {
                    Id=1,
                    Name="xyz",
                    Description="sample",
                    Price=1234,
                    Location="Warehouse",
                    Quantity=10
                },
                new Inventory()
                {
                    Id=2,
                    Name="abc",
                    Description="sample1",
                    Price=2345,
                    Location="Store",
                    Quantity=20
                }
            };

            _mockInventoryRepository
              .Setup(inventoryRepository => inventoryRepository.GetInventory())
              .ReturnsAsync(expectedResult);

            var inventoryController = CreateInventoryController();
            var actualResult = await inventoryController.GetInventory() as OkObjectResult;

            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult.Value, typeof(List<Inventory>));
            Assert.AreEqual(actualResult.Value, expectedResult);
        }

        [TestMethod]
        public async Task AddInventory_WhenMethodCalled_ReturnsId()
        {
            Inventory request = new Inventory()
            {
                Name="xyz",
                Description="sample",
                Price=1234,
                Location="Warehouse",
                Quantity=10
               
            };

            _mockInventoryRepository
              .Setup(inventoryRepository => inventoryRepository.AddNewItem(It.IsAny<Inventory>()))
              .ReturnsAsync(1);

            var inventoryController = CreateInventoryController();
            var actualResult = await inventoryController.AddInventory(request) as OkObjectResult;

            Assert.IsNotNull(actualResult);
            Assert.IsInstanceOfType(actualResult.Value, typeof(int));
            Assert.AreEqual(actualResult.Value, 1);
        }

        [TestMethod]
        public async Task UpdateInventory_WhenMethodCalled_ReturnsNull()
        {
            Inventory request = new Inventory()
            {
                Name = "xyz",
                Description = "sample",
                Price = 1234,
                Location = "Warehouse",
                Quantity = 10
            };

            _mockInventoryRepository
              .Setup(inventoryRepository => inventoryRepository.UpdateInventory(It.IsAny<int>(), It.IsAny<Inventory>()));

            var inventoryController = CreateInventoryController();
            var actualResult = await inventoryController.UpdateInventory(1, request) as NoContentResult;

            Assert.AreEqual(actualResult, null);
        }

        [TestMethod]
        public async Task DeleteInventory_WhenMethodCalled_ReturnsNull()
        {
            _mockInventoryRepository
              .Setup(inventoryRepository => inventoryRepository.DeleteInventoryById(It.IsAny<int>()));

            var inventoryController = CreateInventoryController();
            var actualResult = await inventoryController.DeleteInventory(1) as NoContentResult;

            Assert.AreEqual(actualResult, null);
        }
    }
}
