using Microsoft.EntityFrameworkCore;
using project_management_mcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
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
            // self relation employee id with manager id
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.EmployeeParent)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.ManagerId);

            // many to one employee with department id
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Employees);

            // many to one employee with job id
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Job)
                .WithMany(x => x.Employees);

            modelBuilder.Entity<Job>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Jobs);

            // one to one employee with account
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(e => e.Employee)
                .HasForeignKey<Account>(x => x.Id);

            // many to many account with role
            modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.AccountId, ar.RoleId });
            modelBuilder.Entity<AccountRole>()
                .HasOne(x => x.Account)
                .WithMany(x => x.AccountRoles)
                .HasForeignKey(x => x.AccountId);
            modelBuilder.Entity<AccountRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.AccountRoles)
                .HasForeignKey(x => x.RoleId);

            // many to many employee with activity
            modelBuilder
                .Entity<EmployeeActivity>()
                .HasKey(x => new { x.EmployeeId, x.ActivityId });
            modelBuilder
                .Entity<EmployeeActivity>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.EmployeeActivities)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey(x => x.EmployeeId);
            modelBuilder
                .Entity<EmployeeActivity>()
                .HasOne(x => x.Activity)
                .WithMany(x => x.EmployeeActivities)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey(x => x.ActivityId);

            // one to many activity with activity history
            modelBuilder
                .Entity<ActivityHistory>()
                .HasOne(x => x.Activity)
                .WithMany(x => x.ActivityHistories);

            // one to many project with activity
            modelBuilder
                .Entity<Activity>()
                .HasOne(x => x.Project)
                .WithMany(x => x.Activities);

            // one to many employee manager id with project
            modelBuilder
                .Entity<Project>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.Projects)
                .HasForeignKey(x => x.ManagerId);
        }
    }
}
