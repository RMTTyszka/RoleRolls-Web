using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Authentication.Dtos;
using RoleRollsPocketEdition.Configuration;
using RoleRollsPocketEdition.Infrastructure;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoleRollsPocketEdition.Campaigns.Application.Handlers;

var RoleRollsPolicyOrigins = "rolerolls";


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: RoleRollsPolicyOrigins,
                      policy =>
                      {
                          policy
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowAnyOrigin();
                      });
});
builder.Services.AddControllers(op => op.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<RoleRollsDbContext>(options => 
    {
        builder.Configuration.GetConnectionString("RoleRolls");
    }, ServiceLifetime.Transient);
builder.Services.AddServices();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddMassTransit(configurador =>
{
    configurador.AddConsumer<CampaignUpdatedHandler>();
    configurador.UsingInMemory((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.UseMessageRetry(r =>
        {
            r.Interval(5, TimeSpan.FromSeconds(5));
        });
    });
    /*configurador.AddEntityFrameworkOutbox<RoleRollsDbContext>(o =>
    {
        o.UsePostgres();
        o.UseBusOutbox();
    });
    configurador.AddTransactionalEnlistmentBus();*/
});

var app = builder.Build();

/*// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<RoleRollsDbContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseCors(RoleRollsPolicyOrigins);
app.MapControllers();

app.Run();
