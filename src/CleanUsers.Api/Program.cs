using Asp.Versioning;
using CleanUsers.Api.Middlewares;
using CleanUsers.Application;
using CleanUsers.Infrastructure;
using CleanUsers.Infrastructure.Common.Persistence;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Host.UseSerilog(logger: new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger());

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(option =>
    {
        option.EnableAnnotations();
    });
    builder.Services.AddApiVersioning(option =>
    {
        option.DefaultApiVersion = new ApiVersion(1.0);
        option.AssumeDefaultVersionWhenUnspecified = true;
        option.ReportApiVersions = true;
        option.ApiVersionReader = new MediaTypeApiVersionReader("api-version");
    });
    builder.Services.AddHttpLogging(option =>
    {
        option.LoggingFields = HttpLoggingFields.Request | HttpLoggingFields.Response;
        option.RequestBodyLogLimit = 4096;
        option.ResponseBodyLogLimit = 4096;
    });
    builder.Services.AddProblemDetails();

    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration.GetConnectionString("Database")!);

    builder.Services.AddFluentValidationRulesToSwagger();

    builder.Services.AddScoped<ExceptionHandlingMiddleware>();
}

var app = builder.Build();
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.MapControllers();

    if (app.Environment.IsDevelopment())
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                DatabaseInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }
    }
    app.Run();
}
