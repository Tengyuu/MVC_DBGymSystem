namespace MVC_DBGym.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }

        public int MemberID { get; set; }
        public Member Member { get; set; }


    }

}
