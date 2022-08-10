using toysite.Models;

namespace toyshop.Models.Profile
{
    public class Address
    {
        public int Id { get; set; }
        public string Shahr { get; set; }
        public string Ostan { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public User Customer { get; set; }
    }
}