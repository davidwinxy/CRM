using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CRM.DTOs.CustomerDTOs
{
    public class SearchQueryCustomerDTO
    {
        [Display(Name = "nombre")]
        public string? Name_Like { get; set; }

        [Display(Name = "pagina")]
        public string? LastName_Like { get; set; }
        [Display(Name = "pagina")]
        public int skip { get; set; }
        [Display(Name = "CantReg x Pagina")]
        public int Take { get; set; }
        ///<summary>
        ///1 = no se cuenta los resultados de la busqueda
        ///2 = cuenta los resultado de la busqueda
        /// </summary>
        public byte SendRowCount { get; set; }
    }
}
