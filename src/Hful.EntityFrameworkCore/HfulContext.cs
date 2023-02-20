using Hful.Domain.Iam;
using Hful.EntityFrameworkCore.Extensions;

using Microsoft.EntityFrameworkCore;

namespace Hful.EntityFrameworkCore
{
    public class HfulContext : DbContext
    {
        public HfulContext(DbContextOptions<HfulContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUser();
        }
    }
}
