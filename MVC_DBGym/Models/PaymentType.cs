namespace MVC_DBGym.Models
{
    public class PaymentType
    {
        public int PaymentID { get; set; }  // FK to Payment
        public int PTypeID { get; set; }    // FK to PType

        public Payment Payment { get; set; }
        public Ptype PType { get; set; }
    }
}
