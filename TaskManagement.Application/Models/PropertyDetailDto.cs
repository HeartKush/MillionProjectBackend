namespace TaskManagement.Application.Models
{
    public class PropertyDetailDto
    {
        public string? IdProperty { get; set; }
        public string? IdOwner { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? CodeInternal { get; set; }
        public int Year { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool Featured { get; set; }
    }
}


