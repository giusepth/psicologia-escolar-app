using PsicologiaEscolar.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<TicketRepository>();
builder.Services.AddDistributedMemoryCache(); // Almacenamiento en memoria para sesiones
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tiempo de expiración de sesión
    options.Cookie.HttpOnly = true;                 // Seguridad: acceso solo por HTTP
    options.Cookie.IsEssential = true;              // Necesario para funcionamiento
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession(); // Habilita el uso de sesiones

app.UseAuthorization();

app.UseDefaultFiles();    // Busca index.html en wwwroot
app.UseStaticFiles();     // Sirve archivos estáticos de la carpeta wwwroot

app.MapControllers();

app.Run();
