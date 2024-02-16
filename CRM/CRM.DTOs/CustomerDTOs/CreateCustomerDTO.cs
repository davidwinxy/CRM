using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DTOs.CustomerDTOs
{
    public class CreateCustomerDTO
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "el campo nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "el campo nombre no puede tener mas de 50 caracteres")]
        public string Name { get; set; }


        [Display(Name = "apellido")]
        [Required(ErrorMessage = "el campo apellido es obligatorio")]
        [MaxLength(50, ErrorMessage = "el campo apellido ")]
        public string LastName { get; set; }


        [Display(Name = "Drireccion")]
        [MaxLength(255, ErrorMessage = "el campo Direccion no puede tener mas de 255 caracteres")]
        public string? Address { get; set; }
    }
}


