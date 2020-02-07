namespace LoanCompareSite.Models.viewModels
{
    public class RepaymentDetail
    {
        public long total { get; set; }
        public long balance { get; set; }
        public int monthno { get; set; }
        public long amountToPay { get; set; }
        public int payPercent { get; set; }

    }
}