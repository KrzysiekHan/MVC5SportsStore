namespace Domain.DbModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain.Entities;

    public partial class ModelContext : DbContext
    {
        public ModelContext()
            : base("name=ModelContext")
        {
        }

        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderHeader> OrderHeader { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.UnitPriceDiscount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderDetail>()
                .Property(e => e.LineTotal)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderHeader>()
                .Property(e => e.TotalDue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderHeader>()
                .HasOptional(e => e.OrderDetail)
                .WithRequired(e => e.OrderHeader);

            modelBuilder.Entity<Products>()
                .Property(e => e.Price)
                .HasPrecision(16, 2);

            modelBuilder.Entity<Products>()
                .Property(e => e.ImageMimeType)
                .IsUnicode(false);
        }
    }
}
