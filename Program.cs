using ApiStarPare.Data;
using ApiStarPare.Repositorys;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(5094); // HTTP
//    options.ListenAnyIP(7060, listenOptions => listenOptions.UseHttps()); // HTTPS
//});


// Add services to the container.

var useSqlServer = builder.Configuration.GetValue<bool>("UseSqlServer");


if (useSqlServer)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))));
}


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());    
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "StarPareAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


var app = builder.Build();

//aplica as migrações do banco de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>();

        // Aplica as migrations, mas ignora se o banco já estiver atualizado
        dbContext.Database.Migrate();
    }
    catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
    {
        Console.WriteLine($"Aviso: O banco de dados já está atualizado. {ex.Message}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
