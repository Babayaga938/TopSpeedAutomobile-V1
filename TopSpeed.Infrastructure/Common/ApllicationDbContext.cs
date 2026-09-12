using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TopSpeed.Domain.Models;

namespace TopSpeed.Infrastructure.Common
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Brand> Brand { get; set; }

        public DbSet<VechicleType> VechicleType { get; set; }

       public DbSet<Post> Post { get; set; }
    }
}
