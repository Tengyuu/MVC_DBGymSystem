namespace MVC_DBGym.Models
{
    public class Ptype
    {
        public int PTypeID { get; set; }
        public string PTypeName { get; set; }

        public ICollection<PaymentType> PaymentType { get; set; }
    }
}
