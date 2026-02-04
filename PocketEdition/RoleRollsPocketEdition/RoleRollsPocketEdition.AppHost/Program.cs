using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithEnvironment("POSTGRES_DB", "RoleRolls")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "123qwe")
    .WithVolume("rolerolls-postgres-data");

var roleRollsDb = postgres.AddDatabase("RoleRolls");
postgres.AddDatabase("keycloak");

var keycloak = builder.AddContainer("keycloak", "quay.io/keycloak/keycloak:26.0")
    .WithArgs("start-dev")
    .WithEnvironment("KEYCLOAK_ADMIN", "admin")
    .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
    .WithEnvironment("KC_DB", "postgres")
    .WithEnvironment("KC_DB_URL", "jdbc:postgresql://postgres:5432/keycloak")
    .WithEnvironment("KC_DB_USERNAME", "postgres")
    .WithEnvironment("KC_DB_PASSWORD", "123qwe")
    .WithHttpEndpoint(port: 8080, name: "http")
    .WithExternalHttpEndpoints()
    .WithReference(postgres);

var api = builder.AddProject<Projects.RoleRollsPocketEdition>("api")
    .WithReference(roleRollsDb)
    .WithHttpsEndpoint(port: 7125, name: "https")
    .WithEnvironment("Keycloak__Enabled", "true")
    .WithEnvironment("Keycloak__BaseUrl", keycloak.GetEndpoint("http"))
    .WithEnvironment("Keycloak__Realm", "rolerolls")
    .WithEnvironment("Keycloak__Audience", "rolerolls-api")
    .WithEnvironment("Keycloak__RequireHttpsMetadata", "false")
    .WithExternalHttpEndpoints();

builder.AddExecutable("frontend", "npm", "../../../frontend/role-rolls")
    .WithArgs("run", "start", "--", "--host", "0.0.0.0", "--port", "4200")
    .WithHttpEndpoint(port: 4200, name: "http")
    .WithEnvironment("BROWSER", "none")
    .WithExternalHttpEndpoints()
    .WithReference(api);

builder.Build().Run();
