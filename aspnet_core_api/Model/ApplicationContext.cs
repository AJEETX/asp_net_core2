using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace aspnet_core_api.Model
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        int Save();
    }
    public class ApplicationContext : DbContext, IDbContext
    {
        public ApplicationContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<Student> Students { get; set; }

        public int Save()
        {
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
