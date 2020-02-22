namespace LoanCompareSite.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class LoanComparerModel : DbContext
    {
        public LoanComparerModel()
            : base("name=LoanComparerCFDBModel")
        {
        }

        public virtual DbSet<loandetail> loandetails { get; set; }
        public virtual DbSet<subscription> subscriptions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<loandetail>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<loandetail>()
                .Property(e => e.package)
                .IsUnicode(false);

            modelBuilder.Entity<loandetail>()
                .Property(e => e.terms)
                .IsUnicode(false);

            modelBuilder.Entity<loandetail>()
                .Property(e => e.website)
                .IsUnicode(false);
        }
    }
}
