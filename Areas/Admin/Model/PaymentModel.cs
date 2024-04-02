namespace BookMovieShow.Areas.Admin.Model
{
    public class PaymentModel
    {
        public int? PaymentID { get; set; }
        public int? UserID { get; set; }
        public int? ShowTimeID { get; set; }
        public int? TransactionID { get; set; }
        public decimal? Amount { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
