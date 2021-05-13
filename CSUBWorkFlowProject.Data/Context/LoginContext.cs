using CSUBWorkFlowProject.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSUBWorkFlowProject.Data.Context
{
    public class LoginContext : DbContext
    {
        public LoginContext() : base()
        {
        }

        public DbSet<User> logins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Initial Catalog=CSUBWorkFlowProjectDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasData(
            new User { Username = "auser", Password = "testuser", isManager = false},
            new User { Username = "amanager", Password = "testmanager", isManager = true }) ;
        }
    }
}
