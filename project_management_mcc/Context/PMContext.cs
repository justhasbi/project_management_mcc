using Microsoft.EntityFrameworkCore;
using project_management_mcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Context
{
    public class PMContext : DbContext
    {
        public PMContext(DbContextOptions<PMContext> options) : base(options)
        {

        }

        // DbSet here
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountRole> AccountRoles { get; set; }

        public DbSet<Activity> Activities { get; set; }
        
        public DbSet<ActivityHistory> ActivityHistories { get; set; }
        
        public DbSet<Department> Departments { get; set; }
        
        public DbSet<Employee> Employees { get; set; }
        
        public DbSet<EmployeeActivity> EmployeeActivities { get; set; }
        
        public DbSet<Job> Jobs { get; set; }
        
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<Role> Roles { get; set; }

        // define relationship here
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
