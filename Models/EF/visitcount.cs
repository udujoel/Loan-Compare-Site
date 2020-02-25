namespace LoanCompareSite.Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("visitcount")]
    public partial class visitcount
    {
        public int id { get; set; }

        public int packageid { get; set; }

        public int visits { get; set; }

        [Required]
        public string username { get; set; }
    }
}
