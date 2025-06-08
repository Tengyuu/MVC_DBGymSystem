namespace MVC_DBGym.Models
{
    public class PType
    {
        public int PTypeID { get; set; }
        public string PTypeName { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
