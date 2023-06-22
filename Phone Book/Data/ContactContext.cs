using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Phone_Book.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phone_Book.Data
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["SqlServer"].ConnectionString);
        }

    }
}
