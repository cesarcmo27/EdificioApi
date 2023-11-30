namespace Domain
{
    public class Category
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
      
        public int Status { get; set; }

        public Group Group { get; set; }

    }
}