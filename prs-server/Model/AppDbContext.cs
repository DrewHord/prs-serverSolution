using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prs_server.Model {
    public class AppDbContext: DbContext {

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<RequestLine> RequestLines { get; set; }



        public AppDbContext() { }

        


        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<User>(e => {
                e.HasIndex(p => p.Username).IsUnique(true);
            });

            builder.Entity<Vendor>(e => {
                e.HasIndex(p => p.Code).IsUnique(true);
            });

            builder.Entity<Product>(e => {
                e.HasIndex(p => p.PartNbr).IsUnique(true);
            });
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            if (!builder.IsConfigured) {
                builder.UseSqlServer("server=localhost\\sqlexpress;database=PrsDb;trusted_connection=true");
            }
        }

    }
}
