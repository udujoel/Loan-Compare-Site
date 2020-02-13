namespace LoanCompareSite.Models.EF
{
    using System.Data.Entity;

    public partial class LoanComparerDBModel : DbContext
    {
        public LoanComparerDBModel()
            : base("name=LoanComparerDBModel")
        {
        }

        public virtual DbSet<loandetail> loandetails { get; set; }

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
