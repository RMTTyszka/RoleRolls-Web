using System.Configuration;
using System.Net;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Authentication.Dtos;
using RoleRollsPocketEdition.Configuration;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoleRollsPocketEdition.Campaigns.Application.Handlers;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.NotificationUpdate;
using RoleRollsPocketEdition.Infrastructure;

var RoleRollsPolicyOrigins = "rolerolls";
var assembly = typeof(RoleRollsDbContext).Assembly;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: RoleRollsPolicyOrigins,
                      policy =>
                      {
                          policy
                              .WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                      });
});
builder.Services.AddControllers(op => op.SuppressAsyncSuffixInActionNames = false);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<RoleRollsDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("RoleRolls"), x => x.MigrationsAssembly(typeof(RoleRollsDbContext).Assembly.ToString()));
    }, ServiceLifetime.Transient);
builder.Services.AddServices();
builder.Services.AddImplementationsUsingInterfaceB(typeof(Program).Assembly);
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddMassTransit(configurador =>
{
    configurador.AddConsumer<CampaignUpdatedHandler>();
    configurador.AddConsumer<DefenseTemplateUpdatedHandler>();
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
builder.Services.AddSignalR();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<RoleRollsDbContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseCors(RoleRollsPolicyOrigins);
app.MapHub<SceneHub>("/sceneHub")
    .RequireCors(RoleRollsPolicyOrigins)
    .AllowAnonymous();
app.MapControllers();

app.Run();
