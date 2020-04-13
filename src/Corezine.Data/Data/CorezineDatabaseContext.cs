using Corezine.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Corezine.Domain.Data
{
    public class CorezineDatabaseContext : IdentityDbContext<AppUser, AppRole, Int32>
    {

        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public CorezineDatabaseContext(DbContextOptions<CorezineDatabaseContext> options) : base(options)
        {

        }
    }
}
