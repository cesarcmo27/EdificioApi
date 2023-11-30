namespace Domain
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public ICollection<Apartment> Apartments { get; set; }
    }
}