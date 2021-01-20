using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocietyDataLayer
{
    public class SocietyDataContext : DbContext
    {
        public SocietyDataContext(DbContextOptions<SocietyDataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemMaster>()
                .HasOne<SummaryItemMaster>(s => s.SummaryItemMasterParent)
                .WithMany(g => g.ItemMasterListChild)
                .HasForeignKey(s => s.SummayItemId);
        }

        public DbSet<SocietyMaster> societyMaster { get; set; }
        public DbSet<Building_Master> building_Master { get; set; }
        public DbSet<Flat_Master> flat_Master { get; set; }
        public DbSet<FlatOwner> flatOwner { get; set; }
        public DbSet<SocietyMaintanance> societyMaintanance { get; set; }

        public DbSet<OwnerComplaints> ownerComplaints { get; set; }
        public DbSet<AdminMaster> adminMaster { get; set; }
        public DbSet<ItemMaster> ItemMaster { get; set; }
        public DbSet<SummaryItemMaster> SummaryItemMaster { get; set; }
    }
}
