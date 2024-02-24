using Microsoft.EntityFrameworkCore;
using Persistent;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();




// Configure the HTTP request pipeline or middlewares 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

// automatically cleans-up after it
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

// This is equivalent to the dotnet CLI(dotnet ef database update) operation to create the database tables
// based on the migration
try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync(); // dotnet ef database update from cli
    await Seed.SeedData(context);
}
catch(Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
