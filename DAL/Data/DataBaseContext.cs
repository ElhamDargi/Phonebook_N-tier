using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class DataBaseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.; Initial Catalog=DBPhonebook; Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

        }
        public DbSet<Entities.Contact> Contacts { get; set; } 
    }
}
