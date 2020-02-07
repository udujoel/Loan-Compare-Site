using System;

namespace LoanCompareSite.Models
{
    public class LoansDetail
    {
        public int id { get; set; }
        public string name { get; set; }
        public string package { get; set; }
        public long minAmount { get; set; }
        public long maxAmount { get; set; }
        public int count { get; set; }
        public DateTime date { get; set; }
        public double rate { get; set; }
        public string terms { get; set; }
        public string website { get; set; }
        public int duration { get; set; }


    }

}