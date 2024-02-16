using CRM.API.Endpoints;
using CRM.API.Models.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;


//crea un nuevo constructor de la aplicacion web
var builder = WebApplication.CreateBuilder(args);


//agrega servicios para habilitar la generacion de documentos api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configura y agrega un contexto de base  de datos para entity framework
builder.Services.AddDbContext<CRMContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("conn"))
);

//agrega una instacia de la clase customerDAL como un sevicio para la inyeccion de dependencias
builder.Services.AddScoped<CustomerDAL>();

//construye la aplicacion web
var app = builder.Build();


//agrega los puntos finales relacionados con los clientes a la aplicacion

app.AddCustomerEndpoint();

//verifica si la aplicacion se esta ejecutando en un entorno de desarrollo
if (app.Environment.IsDevelopment())
{

    //habilita el uso de swagger para la documentacion de la api
    app.UseSwagger();
    app.UseSwaggerUI();
}


//agrega middleware para redirigir las solicitudes http a https
app.UseHttpsRedirection();
//ejecutar la aplicacion
app.Run();

