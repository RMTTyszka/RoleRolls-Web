using System.Configuration;
using System.Net;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Authentication.Dtos;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RoleRollsPocketEdition._Application.Campaigns.Handlers;
using RoleRollsPocketEdition._Domain.Global;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Core.Configuration;
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
builder.Services.AddControllers(op =>
    {
        op.SuppressAsyncSuffixInActionNames = false;
    })
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache(op =>
{
    op.SizeLimit = null;
});
builder.Services.AddEntityFrameworkNpgsql()
    .AddDbContext<RoleRollsDbContext>(options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString("RoleRolls"), x => x.MigrationsAssembly(typeof(RoleRollsDbContext).Assembly.ToString()));
    });
builder.Services.AddServices();
builder.Services.AddTransientServices(typeof(Program).Assembly);
builder.Services.AddScopedServices(typeof(Program).Assembly);
builder.Services.AddStartupTasks(typeof(Program).Assembly);

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
    var startupTasks = scope.ServiceProvider.GetServices<IStartupTask>();
    foreach (var task in startupTasks)
    {
        await task.ExecuteAsync();
    }
}

app.UseHttpsRedirection();
app.UseMiddleware<JwtMiddleware>();
app.UseCors(RoleRollsPolicyOrigins);
app.MapHub<SceneHub>("/sceneHub")
    .RequireCors(RoleRollsPolicyOrigins)
    .AllowAnonymous();
app.MapControllers();
app.Run();
