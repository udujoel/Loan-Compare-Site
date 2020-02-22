namespace LoanCompareSite.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("subscription")]
    public partial class subscription
    {
        public int id { get; set; }

        [Required]
        public string userid { get; set; }

        public DateTime? startdate { get; set; }

        public DateTime? enddate { get; set; }
    }
}
