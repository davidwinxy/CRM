//importa el espacio de nombres necesarios para DBcontext
using Microsoft.EntityFrameworkCore;
using CRM.API.Models.EN;

//define la clase CRMcontext que hereda de DBContext
namespace CRM.API.Models.DAL
{
    public class CRMContext : DbContext
    {
        //constructor que toma DBcontextOptios como parametro para configurar la conexion a la base de datos
        public CRMContext(DbContextOptions<CRMContext> options) : base(options) 
        {
        }

        //define un DBset llamado "Customers" que representa una tabla de clientes en la base de datos
        public DbSet<Customer> Customers { get; set; }
    }
}
