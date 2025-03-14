using Autofac.Extensions.DependencyInjection;
using Autofac;
using EFCore.API.Configure;
using EFCore.API.Data;
using EFCore.API.Logging;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.IO.Compression;
using System.Reflection;
using EFCore.API.Middlewares;
using System.Text.Json.Serialization;
using EFCore.API.Helpers;


Log.Logger = new LoggerConfiguration()
    .CreateBootstrapLogger();

try
{

    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    ); 

    #region Ef DbContext
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<AccommodationDBContext>(options => options.UseSqlServer(connectionString));
    #endregion Ef DbContext

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

    #region Dependency Injection

    builder.Services.AddTransient<ExceptionHandlerMiddleware>();

    builder.Services.AddSingleton<IHelperFunctions, HelperFunctions>();

    #endregion Dependency Injection

    #region Serilog
    builder.Host.UseSerilog(SeriLogger.Configure);
    #endregion Serilog

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

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception)
{

	throw;
}

