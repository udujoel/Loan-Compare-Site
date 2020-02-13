namespace LoanCompareSite.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("loandetail")]
    public partial class loandetail
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [StringLength(100)]
        public string package { get; set; }

        public long minAmount { get; set; }

        public long maxAmount { get; set; }

        public int? count { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }

        public float rate { get; set; }

        [StringLength(1000)]
        public string terms { get; set; }

        [Required]
        [StringLength(250)]
        public string website { get; set; }

        public int duration { get; set; }
    }
}
