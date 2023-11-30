namespace Domain
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}