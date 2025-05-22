namespace MVC_DBGym.Models
{
    public class DBPayment
    {
        public int PaymentID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }

        public int MemberID { get; set; }
        public DBMembers Members { get; set; }


    }

}
