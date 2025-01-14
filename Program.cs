using repositorys;

var builder = WebApplication.CreateBuilder(args);

//--------------------------------------------------------------------------------------------------
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IPresupuestosRepository, PresupuestosRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Habilitar servicios de sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de la sesión
    options.Cookie.HttpOnly = true; // Solo accesible desde HTTP, no JavaScript
    options.Cookie.IsEssential = true; // Necesario incluso si el usuario no acepta cookies
});

//Inyeccion de CadenaDeConexion
string CadenaDeConexion = builder.Configuration.GetConnectionString("SqliteConexion")!.ToString();
builder.Services.AddSingleton(CadenaDeConexion);

// Registrar IHttpContextAccessor
builder.Services.AddHttpContextAccessor(); // agrego para poder acceder a la variable HttpContext desde layout
//--------------------------------------------------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Usar sesiones ---------------------------------------------------------------
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Logeo}/{action=Index}/{id?}");

app.Run();
