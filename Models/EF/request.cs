namespace LoanCompareSite.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("request")]
    public partial class request
    {
        public int id { get; set; }

        [Required]
        public string username { get; set; }

        public long amountreq { get; set; }

        public long durationreq { get; set; }
    }
}
