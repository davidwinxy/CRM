var builder = WebApplication.CreateBuilder(args); //crea un constructor de aplicaciones web

//agrega servicios al contenedor de dependencias
builder.Services.AddControllersWithViews(); //agrega servicios para controladores y vistas

//configura y agrega un cliente http con compre CRMAPI
builder.Services.AddHttpClient("CRMAPI", c =>
{
    //configura la direcion base del cliente http desde la configuracion
    c.BaseAddress = new Uri(builder.Configuration["UrlsAPI:CRM"]);
    //pudes confirgurar otras opciones del cliente http aqui
});

var app = builder.Build(); //crea un ainstacia de la aplicacion web

//configura el pipleline de solicitudes de http
if (app.Environment.IsDevelopment())
{
    //maneja exepciones en caso de errores y redirige a la accion error en el controlador home
    app.UseExceptionHandler("/Home/Error");
    //el valor hsts predeterminado es de 30 dias pudes cambiarlo para esenarios de produccion
    app.UseHsts();
}

app.UseHttpsRedirection(); //redirige las solicitudes htto a https
app.UseStaticFiles(); //habilita el uso de archivos estaticos como en css javascript imagenes etc

app.UseRouting(); //configura el enrutamiento de solicitudes

app.UseAuthorization(); //habilita la autorizacion para proteger rutas y acciones de controladores

//mapea la ruta predeterminada de controladores y accion
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); //inicia la aplicacion web

