using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebShop.Domain.Entities;
using WebShop.Infrastructure.Persistence;

namespace WebShop.Infrastructure.Seeders
{
    internal class DataSeeder(WebShopDbContext dbContext) : IDataSeeder
    {
         Tuple<string, string>  hashedPasswordAndSalt = HashPassword("password");
        Tuple<string, string> hashedPasswordAndSalt2 = HashPassword("password");
        Tuple<string, string> hashedAdminPasswordAndSalt = HashPassword("admin");



        private readonly static int iterations = 1000;

        private static Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, iterations);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }


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
                    Password = hashedAdminPasswordAndSalt.Item1,
                    Salt = hashedAdminPasswordAndSalt.Item2
                }
            };
        }

        private IEnumerable<Product> GetProducts()
        {
            var maleWallet = new Product
            {
                ProductId = productId1,
                Name = "Men's Classic Leather Wallet",
                Description = "Classic men's leather wallet with multiple card slots and a bill compartment.",
                Category = "Wallets",
                Gender = "Male",
                Price = 4200,
                StockQuantity = 100,
                ImageUrl = "https://images.ctfassets.net/30h767egv35o/7hE16yy6KAHOr110VfxE75/8551932e6cc6ca0fbe46a96cea64cdcb/IMG-1575.jpg"
            };

            var femaleWallet = new Product
            {
                ProductId = productId2,
                Name = "Women's Leather Wallet",
                Description = "Elegant women's leather wallet with compartments for cards, cash, and coins.",
                Category = "Wallets",
                Gender = "Female",
                Price = 59.99m,
                StockQuantity = 80,
                ImageUrl = "https://images.ctfassets.net/30h767egv35o/4u6c59jHjf9xz582fu6WHk/c17cd696f0189628293b961ee341b8d0/2.jpg"
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
                Password = hashedPasswordAndSalt2.Item1,
                Salt = hashedPasswordAndSalt2.Item2,
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
                                        Quantity = 2, // Example quantity
                                       Price = 4200,
                                        Name =  "wallet"
                                    }
                            }
                        }
                    }
            },new (){
                UserId = userId2,
                Name = "Jane Smith",
                Email = "jane@example.com",
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
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
                                        Quantity = 1, // Example quantity
                                        Price = 50,
                                        Name =  "wallet woman"

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
