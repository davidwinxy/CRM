using System.ComponentModel.DataAnnotations;
namespace CRM.DTOs.CustomerDTOs
{
    public class CreateCustomerDTO
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "el campo nombres es obligatorio")]
        [MaxLength(50, ErrorMessage ="el campo nombre no puede tener mas de 50 caracteres")]
        public string Name {  get; set; }

        [Display(Name = "APellido")]
        [Required(ErrorMessage ="el campo apellido es obligatorio")]
        [MaxLength(50, ErrorMessage ="el campo apellido no puede tener mas de 50 caracteres")]
        public string LastName { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(255, ErrorMessage = "el campo direccion no puede tener mas de 255 caracteres")]
        public string? Address { get; set; }
    }
}

