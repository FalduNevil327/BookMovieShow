namespace BookMovieShow.Areas.User.Model
{

    public class PaymentModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PromoCode { get; set; }
        public string CardDetails { get; set; }
        public string CardName { get; set; }
        public string CardExpiration { get; set; }
        public string CardCVV { get; set; }
        public bool QuickPay { get; set; }
        public SeatSelectionModel SeatSelection { get; set; }
        public bool PaymentSuccess { get; set; }
        public string TransactionId { get; set; }
        public string Message { get; set; }

    }
}
