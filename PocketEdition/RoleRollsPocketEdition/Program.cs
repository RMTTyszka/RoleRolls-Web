using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Core.Authentication.Dtos;
using RoleRollsPocketEdition.Core.Configuration;
using RoleRollsPocketEdition.Core.MIddlewares;
using RoleRollsPocketEdition.Core.NotificationUpdate;
using RoleRollsPocketEdition.Infrastructure;
using Serilog;

var RoleRollsPolicyOrigins = "rolerolls";
var assembly = typeof(RoleRollsDbContext).Assembly;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: RoleRollsPolicyOrigins,
                      policy =>
                      {
                          policy
                              .WithOrigins("http://localhost:4200", "http://localhost:4201")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                      });
});

builder.Services.AddControllers(op =>
    {
        op.SuppressAsyncSuffixInActionNames = false;
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
    })
    .AddMvcOptions(options =>
    {
        options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            _ => "The field is required.");
    });

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
/*builder.Services.AddOptions<SqlTransportOptions>().Configure(options =>
{
    options.Host = "localhost";
    options.Database = "RoleRolls";
    options.Schema = "transport";
    options.Role = "postgres";
    options.Username = "postgres";
    options.Password = "123qwe";
    options.AdminUsername = "postgres";
    options.AdminPassword = "123qwe";
});*/

/*builder.Services.AddMassTransit(configurador =>
{
    configurador.AddConsumer<CampaignUpdatedHandler>();
    configurador.AddConsumer<DefenseTemplateUpdatedHandler>();
    configurador.UsingPostgres((context, cfg) =>
    {
        cfg.ConfigureEndpoints(context);
        cfg.UseMessageRetry(r =>
        {
            r.Interval(5, TimeSpan.FromSeconds(5));
        });
    });
});

builder.Services.AddPostgresMigrationHostedService();*/
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
app.UseMiddleware<SerilogBadRequestLoggingMiddleware>();
app.UseCors(RoleRollsPolicyOrigins);
app.MapHub<SceneHub>("/sceneHub")
    .RequireCors(RoleRollsPolicyOrigins)
    .AllowAnonymous();
app.MapControllers();
app.Run();