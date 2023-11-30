namespace Domain
{
    public class Apartment
    {
        public Guid Id { get; set; }
        public string Block { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public int Floor { get; set; }
        public double Percentage { get; set; }
        public double Area { get; set; }
        public Building Building { get; set; }
        public Guid BuildingId { get; set; }
       // public ICollection<Receipt> Receipts { get; set; }
        public Person Person { get; set; }
        public Guid? PersonId { get; set; }

        
    }
}