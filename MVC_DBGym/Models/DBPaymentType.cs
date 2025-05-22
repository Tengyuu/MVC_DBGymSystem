namespace MVC_DBGym.Models
{
    public class DBPaymentType
    {
        public int PaymentID { get; set; }  // FK to Payment
        public int PTypeID { get; set; }    // FK to PType

        public DBPayment Payment { get; set; }
        public DBPtype PType { get; set; }
    }
}
