using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI
{
    public class ProjectSeeder
    {
        private readonly ProjectDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public ProjectSeeder(ProjectDbContext context,
            IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
                if (!_context.Users.Any())
                {
                    var admin = CreateAdmin();
                    _context.Users.Add(admin);
                    _context.SaveChanges();
                }
                if (!_context.Deliveries.Any())
                {
                    var deliveries = GetDeliveries();
                    _context.Deliveries.AddRange(deliveries);
                    _context.SaveChanges();
                }

            }
        }

        private User CreateAdmin()
        {
            string examplePassword = "1234QWE";
            var admin = new User()
            {
                CreationTime = DateTime.Now,
                Email = "admin@example.com",
                DateOfBirth = new DateTime(1999, 01, 01),
                Name = "Admin",
                Surname = "",
                Phone = "",
                RoleId = 3,


            };
            admin.Password = _passwordHasher.HashPassword(admin, examplePassword);
            return admin;
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                Name = "Manager"
            },
                new Role()
                {
                    Name = "Admin"
                },
            };

            return roles;
        }
        private IEnumerable<Delivery> GetDeliveries()
        {
            var deliveries = new List<Delivery>()
            {
                new Delivery()
                {
                   Type=Enums.DeliveryType.ToShop,
                   Price=0
                },
                new Delivery()
                {
                    Type=Enums.DeliveryType.ToHouse,
                    Price=(decimal)16.99
                },
                new Delivery()
                {
                    Type=Enums.DeliveryType.ToParcelLocker,
                    Price=(decimal)7.99
                },
            };

            return deliveries;
        }
    }
}
