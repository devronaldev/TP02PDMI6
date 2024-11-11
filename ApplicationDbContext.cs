using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP02PDMI6.Models;

namespace TP02PDMI6
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TaskItem> TasksItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseMySql("Server=localhost;Database=TP02PDMI6;User=root;Password=;", ServerVersion.AutoDetect("Server=localhost;Database=TP02PDMI6;User=root;Password=;"));
        }
    }
}
