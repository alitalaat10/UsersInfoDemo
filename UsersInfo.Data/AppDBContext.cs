using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersInfo.Core;

namespace UsersInfo.Data
{
    public class AppDBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=TAL3AT\SQL22;TrustServerCertificate=True;User Id=sa;Password=159236478;DataBase=UserDemo;");
        }

        public DbSet<User> Users { get; set; }
    }
}
