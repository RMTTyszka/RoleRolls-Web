using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Authentication.Dtos;
using RoleRollsPocketEdition.Configuration;
using RoleRollsPocketEdition.Infrastructure;
using System.Configuration;
using System.Linq;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<RoleRollsDbContext>(options => 
    {
        builder.Configuration.GetConnectionString("RoleRolls");
    }, ServiceLifetime.Transient);
builder.Services.AddServices();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
/*    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
    dataContext.Database.Migrate();*/
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();
