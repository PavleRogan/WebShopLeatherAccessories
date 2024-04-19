using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Infrastructure.Persistence;

namespace WebShop.Infrastructure.Seeders
{
    internal class DataSeeder(WebShopDbContext dbContext) : IDataSeeder
    {
        
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Products.Any())
                {
                    var products = GetProducts();
                    dbContext.Products.AddRange(products);
                    await dbContext.SaveChangesAsync();
                }
                if (!dbContext.Users.Any())
                {
                    var users = GetUsers();
                    dbContext.Users.AddRange(users);
                    await dbContext.SaveChangesAsync();
                }
                
                if (!dbContext.Admins.Any())
                {
                    var admins = GetAdmins();
                    dbContext.Admins.AddRange(admins);
                    await dbContext.SaveChangesAsync();
                }
              
               
            }

        }

        Guid productId1 = new Guid("B02D6E2E-7C2C-4EB1-963C-1A62042B93FA");
        Guid productId2 = new Guid("AC8F513A-331E-40FE-B047-23C45031F939");
        Guid orderId1 = new Guid("eae7b2a9-a90b-4796-ac8b-14a00c6b92c5");
        Guid orderId2 = new Guid("3728bf70-3f1f-4e5f-becc-483e0e8a2049");
        Guid userId1 = new Guid("2728bf70-3f1f-4e5f-becc-483e0e8a2042");
        Guid userId2 = new Guid("1728bf70-3f1f-4e5f-becc-483e0e8a2041");
        Guid orderItemId1 = new Guid("1228bf70-3f1f-4e5f-becc-483e0e8a2041");
        Guid orderItemId2 = new Guid("1238bf70-3f1f-4e5f-becc-483e0e8a2041");

       
        private IEnumerable<Admin> GetAdmins()
        {
            return new List<Admin>
            {
                new Admin
                {
                    AdminId = Guid.NewGuid(),
                    Username = "admin",
                    Password = "admin" 
                }
            };
        }

        private IEnumerable<Product> GetProducts()
        {
            var maleWallet = new Product
            {
                ProductId = productId1,
                Name = "Men's Leather Wallet",
                Description = "Classic men's leather wallet with multiple card slots and a bill compartment.",
                Category = "Wallets",
                Gender = "Male",
                Price = 49.99m,
                StockQuantity = 100 
            };

            var femaleWallet = new Product
            {
                ProductId = productId2,
                Name = "Women's Leather Wallet",
                Description = "Elegant women's leather wallet with compartments for cards, cash, and coins.",
                Category = "Wallets",
                Gender = "Female",
                Price = 59.99m,
                StockQuantity = 80 
            };

            return new List<Product> { maleWallet, femaleWallet };
        }
    

        private IEnumerable<User> GetUsers()
        {
            List<User> users = [new()

            {
                UserId = userId1,
                Name = "John Doe",
                Email = "john@example.com",
                Password = "password123",
                Address = new()
                {
                    City = "New York",
                    StreetAndNumber = "123 Main St",
                    PostalCode = "10001"
                },
                ContactNumber = "123-456-7890",
                Orders = new List<Order>
                    {
                       new Order
                        {
                            OrderId = orderId1,
                            OrderDate = DateTime.Now.AddDays(-7),
                            UserId = userId1,
                            Processed = true,

                            OrderItems = new List<OrderItem>
                            {
                            new OrderItem
                                    {
                                        OrderItemId = orderItemId1,
                                        ProductId = productId1,
                                        OrderId = orderId1,
                                        Quantity = 2 // Example quantity
                                    }
                            }
                        }
                    }
            },new (){
                UserId = userId2,
                Name = "Jane Smith",
                Email = "jane@example.com",
                Password = "password456",
                Address = new Address
                {
                    City = "Los Angeles",
                    StreetAndNumber = "456 Oak Ave",
                    PostalCode = "90001"
                },
                ContactNumber = "987-654-3210",
                Orders = new List<Order>
                    {
                       new Order
                        {
                            OrderId = orderId2,
                            OrderDate = DateTime.Now.AddDays(-7),
                            UserId = userId2,
                            Processed = false,

                            OrderItems = new List<OrderItem>
                            {
                            new OrderItem
                                    {
                                        OrderItemId = orderItemId2,
                                        ProductId = productId2,
                                        OrderId = orderId2,
                                        Quantity = 1 // Example quantity
                                    }
                            }
                        }
                    }
            }

                ];

            return users;
        }
    }
}
