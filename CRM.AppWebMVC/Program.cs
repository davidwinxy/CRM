var builder = WebApplication.CreateBuilder(args); // create un constructor de aplicaciones web

//agrega servicios al contenedor de dependencias
builder.Services.AddControllersWithViews(); //agrega servicios para controladores y vistas

//configura y agrega un cliente http con nombre CRMAPI
builder.Services.AddHttpClient("CRMAPI", c => 
{
    //configura la direccion base del cliente http desde la configuracion
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);
    //puedes configurar otras opciones del htppclient aqui segun sea necesario
});

var app = builder.Build(); //crea una instancia de la aplicaion web

//Configura el pipeline de solicitudes http
if (!app.Environment.IsDevelopment())
{
    //maneja excepsiones en caso de errores y riderige a la accion error en el controlador home
    app.UseExceptionHandler("/Home/Error");
    //El valor htst predeterminado es de 30 dias puedes cambiarlo para escenarios de produccion
    app.UseHsts();
}

app.UseHttpsRedirection(); //redirige las solicitudes http a https
app.UseStaticFiles(); // habilita el uso de archivos estaticos como css javascript imagenes etc

app.UseRouting(); // configura el enrutamiento de solicitudes

app.UseAuthorization(); //habilita la autorizacion para proteger rutas y acciones de controladores

//mapea la ruta predeterminada de controlador y accion
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); //Iniciar la aplicacion web