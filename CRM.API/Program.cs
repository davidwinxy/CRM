//Importa los espacios de nombres necesarios para el proyecto.
using CRM.API.Endpoints;
using CRM.API.Models.DAL;
using Microsoft.EntityFrameworkCore;

//crear un nuevo constructor de la aplicacion web
var builder = WebApplication.CreateBuilder(args);

//Agrega Servicios para habilitar la generacion de un documento de api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configura y agrega un contexto de base de datos para entity framework core
builder.Services.AddDbContext<CRMContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conn"))
);

//Agrega una isntancia de la clase customerDAL como un sevicio para la inyeccion de dependencias
builder.Services.AddScoped<CustomerDAL>();

//construye la aplicacion web
var app = builder.Build();

//agrega los puntos finales relacionados con los clientes a la aplicaion
app.AddCustomerEndpoints();

//verifica si la aplicacion se esta ejecutando en un entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    //habilita el uso de swager para la documentacion de la api
    app.UseSwagger();
    app.UseSwaggerUI();
}

//agrega middleware para redirigir las solicitudes thhp a https
app.UseHttpsRedirection();

//ejecuta la aplicacion
app.Run();
