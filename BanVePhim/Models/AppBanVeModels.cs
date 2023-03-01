using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BanVePhim.Models
{
    public partial class AppBanVeModels : DbContext
    {
        public AppBanVeModels()
            : base("name=AppBanVeModels")
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<Ghe> Ghes { get; set; }
        public virtual DbSet<HangGhe> HangGhes { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ghe>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.Ghe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HangGhe>()
                .Property(e => e.TenHangGhe)
                .IsFixedLength();

            modelBuilder.Entity<HangGhe>()
                .HasMany(e => e.Ghes)
                .WithRequired(e => e.HangGhe)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);
        }
    }
}
