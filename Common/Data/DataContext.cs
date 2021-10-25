using Common.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_ManagementHouseRentals.Data.Entities;

namespace Web_ManagementHouseRentals.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Property> Properties { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Proposal> Proposals { get; set; }

        public DbSet<Extra> Extras { get; set; }

        public DbSet<EnergyCertificate> EnergyCertificates { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<SizeType> SizeTypes { get; set; }

        public DbSet<ZipCode> ZipCodes { get; set; }

        public DbSet<Property_Photo> Property_Photos { get; set; }

        public DbSet<ProposalState> ProposalStates { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //Habilitar a regra de apagar em cascata (Cascade Delete Rule)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
