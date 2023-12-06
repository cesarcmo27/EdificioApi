

using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

//add services to container
builder.Services.AddCors(options => options.AddPolicy(name: "MiEdificio",
policy =>
{
    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}

));

builder.Services.AddHealthChecks();
builder.Services.AddControllers(opt => {
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<Create>();
});

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
// configure http pipeline
var app = builder.Build();

app.MapHealthChecks("/health");

app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv6 v1"));
}
app.UseCors("MiEdificio");
app.UseHttpsRedirection();

//  se remueve porque esta implicitmanete app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    Console.WriteLine("Iniciando la creacio  de bd");
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var rolManager = services.GetRequiredService<RoleManager<AppRol>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context,userManager,rolManager);
}
catch (Exception ex)
{
    var looger = services.GetRequiredService<ILogger<Program>>();
    looger.LogError(ex, "Error en la migracion");
}

await app.RunAsync();
