using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Server;
using NLog;
using NLog.Web;
using PracticeReactApp.Core.Data;
using PracticeReactApp.Core.Data.Entities;

// Early init of NLog to allow startup and exception logging, before host is built
var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    /*for authen by bearer token*/
    //builder.Services.AddSwaggerGen(option => {
    //    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    //    {
    //        In = ParameterLocation.Header,
    //        Name = "Authorization",
    //        Type = SecuritySchemeType.ApiKey
    //    });

    //    option.OperationFilter<SecurityRequirementsOperationFilter>();
    //});

    builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
        .AddIdentityCookies();

    builder.Services.AddAuthorizationBuilder();

    builder.Services.AddDbContext<PracticeDotnetReactContext>(option =>
    {
        option.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly("PracticeReactApp.Migrations"));
    });

    /*for authen by bearer token*/
    //builder.Services.AddIdentityApiEndpoints<User>(option => {
    //    option.User.RequireUniqueEmail = true;
    //    option.Password.RequiredUniqueChars = 0;
    //    option.Password.RequireUppercase = false;
    //    option.Password.RequiredLength = 8;
    //})
    //    .AddEntityFrameworkStores<PracticeReactContext>();

    builder.Services.AddIdentityCore<User>(option =>
    {
        option.User.RequireUniqueEmail = true;
        option.Password.RequiredUniqueChars = 0;
        option.Password.RequireUppercase = false;
        option.Password.RequiredLength = 8;
    })
        .AddRoles<Role>()
        .AddEntityFrameworkStores<PracticeDotnetReactContext>()
        .AddApiEndpoints();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy
            .WithOrigins("https://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    });

    builder.Services.AddHttpClient();
    builder.Services.AddHttpContextAccessor();

    //add service to the container
    Services.ConfigureServices(builder.Services);

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    app.UseDefaultFiles();
    app.UseStaticFiles();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Tells the app to transmit the cookie through HTTPS only.
    app.UseCookiePolicy(
        new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always
        });

    app.MapIdentityApi<User>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}