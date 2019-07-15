namespace Domain
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderHeader> OrderHeader { get; set; }

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
        }
    }
}
