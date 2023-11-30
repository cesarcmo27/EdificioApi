namespace Application.Apartment
{
    public class ApartmentDTO
    {
        public Guid Id { get; set; }
        public string Block { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int Floor { get; set; }
        public double Percentage { get; set; }
        public double Area { get; set; }

        public Guid BuildingId { get; set; }
    }
}