namespace GestionSubcription.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ApplicationId { get; set; }
        public Application Application { get; set; }

        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
    }

}
