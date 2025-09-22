namespace TaskManagement.Application.Models
{
    public class CreatePropertyRequest
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public string? CodeInternal { get; set; }
        public int Year { get; set; }
        public string? IdOwner { get; set; }
        public string? ImageUrl { get; set; }
        public bool ImageEnabled { get; set; } = true;
        public bool Featured { get; set; } = false;
    }
}


