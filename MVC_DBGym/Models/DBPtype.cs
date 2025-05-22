namespace MVC_DBGym.Models
{
    public class DBPtype
    {
        public int PTypeID { get; set; }
        public string PTypeName { get; set; }

        public ICollection<DBPaymentType> PaymentTypes { get; set; }
    }
}
