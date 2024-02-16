using System.ComponentModel.DataAnnotations;
namespace CRM.DTOs.CustomerDTOs
{
    public class EditCustomerDTO
    {
        public EditCustomerDTO(GetIdResultCustomerDTO getIdResultCustomerDTO)
        {
            Id = getIdResultCustomerDTO.Id;
            Name = getIdResultCustomerDTO.Name;
            LastName = getIdResultCustomerDTO.LastName;
            Address = getIdResultCustomerDTO.Address;
        }
        public EditCustomerDTO()
        {
            Name = string.Empty;
            LastName = string.Empty;
        }
        [Required(ErrorMessage ="EL campo ID es obligatorio.")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="el campo nombre es obligatorio")]
        [MaxLength(50, ErrorMessage ="el nombre no puede tener mas de 50 caracteres")]
        public string Name { get; set; }

        [Display(Name ="Apellido")]
        [Required(ErrorMessage ="el campo apellido es obligatorio")]
        [MaxLength(50, ErrorMessage ="el campo apellido no puede tener mas de 50 caracteres")]
        public string LastName { get; set; }

        [Display(Name ="Direccion")]
        [MaxLength(255, ErrorMessage ="el campo direccion no puede tener mas de 255 caracteres")]
        public string? Address { get; set; }
    }
}