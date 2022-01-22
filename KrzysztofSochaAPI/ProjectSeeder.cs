using KrzysztofSochaAPI.Context;
using KrzysztofSochaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI
{
    public class ProjectSeeder
    {
        private readonly ProjectDbContext _context;
        public ProjectSeeder(ProjectDbContext context)
        {
            _context = context;
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

                
            }
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
    }
}
