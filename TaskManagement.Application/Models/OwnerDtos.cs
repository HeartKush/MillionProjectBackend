using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Models
{
    public class OwnerListItemDto
    {
        public string? IdOwner { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class OwnerDetailDto
    {
        public string? IdOwner { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Photo { get; set; }
        public System.DateTime? Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateOwnerRequest
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string? Name { get; set; }

        [StringLength(200, ErrorMessage = "La dirección no puede exceder 200 caracteres")]
        public string? Address { get; set; }

        [StringLength(500, ErrorMessage = "La URL de la foto no puede exceder 500 caracteres")]
        public string? Photo { get; set; }

        public System.DateTime? Birthday { get; set; }
    }
}




