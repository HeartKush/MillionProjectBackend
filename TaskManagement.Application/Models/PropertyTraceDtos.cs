using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.Models
{
    public class PropertyTraceListItemDto
    {
        public string? IdPropertyTrace { get; set; }
        public System.DateTime DateSale { get; set; }
        public string? Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public string? IdProperty { get; set; }
    }

    public class CreatePropertyTraceRequest
    {
        [Required(ErrorMessage = "La fecha de venta es obligatoria")]
        public System.DateTime DateSale { get; set; }

        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "El valor es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El valor debe ser mayor o igual a 0")]
        public decimal Value { get; set; }

        [Required(ErrorMessage = "El impuesto es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El impuesto debe ser mayor o igual a 0")]
        public decimal Tax { get; set; }

        [Required(ErrorMessage = "El IdProperty es obligatorio")]
        public string? IdProperty { get; set; }
    }
}




