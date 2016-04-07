namespace NetwiseTask
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OrderModel : DbContext
    {
        public OrderModel()
            : base("name=OrderModel")
        {
        }

        public virtual DbSet<orderDetail> orderDetails { get; set; }
        public virtual DbSet<order> orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<orderDetail>()
                .Property(e => e.prod_name)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.surname)
                .IsFixedLength();

            modelBuilder.Entity<order>()
                .Property(e => e.amount)
                .HasPrecision(18, 0);
        }
    }
}
