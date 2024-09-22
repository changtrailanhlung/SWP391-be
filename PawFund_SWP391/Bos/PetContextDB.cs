using Bos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bos
{
    public class PetContextDB : DbContext
    {
        //private readonly IConfiguration _configuration;
        //private readonly string _connectionstring;
        //public PetContextDB(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _connectionstring = _configuration.GetConnectionString("default");
        //}
        //public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL(_connectionstring);
        //}
    }
}
