using Autofac.Extensions.DependencyInjection;
using Autofac;
using EFCore.API.Configure;
using EFCore.API.Entities;
using EFCore.API.Middlewares;
using System.Reflection;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Serilog;
using EFCore.API.Logging;

Log.Logger = new LoggerConfiguration()
    .CreateBootstrapLogger();

try
{

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    #region CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
        {
            builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin();
        });
    });
    #endregion

    #region compression
    builder.Services.AddResponseCompression(options =>
    {
        options.EnableForHttps = true;
        options.Providers.Add<BrotliCompressionProvider>();
        options.Providers.Add<GzipCompressionProvider>();
    });
    builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.Fastest;
    });

    builder.Services.Configure<GzipCompressionProviderOptions>(options =>
    {
        options.Level = CompressionLevel.SmallestSize;
    });
    #endregion

    #region Serilog
    builder.Host.UseSerilog(SeriLogger.Configure);
    #endregion Serilog

    builder.Services.AddDbContext<AccommodationContext>();

    #region Data Access Dependency Injection

    builder.Services.AddTransient<ExceptionHandlerMiddleware>();

    #endregion

    #region AutoFac
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModuleRegister()));
    #endregion

    #region Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwagger(builder.Configuration);
    #endregion Swagger

    builder.Services.AddHttpClient();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

    var app = builder.Build();

    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCors(MyAllowSpecificOrigins);

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

