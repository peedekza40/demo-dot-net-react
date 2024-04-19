using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Server.Data;
using PracticeReactApp.Server.Models.Entities;

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

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();

builder.Services.AddAuthorizationBuilder();

builder.Services.AddDbContext<PracticeReactContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityCore<User>(option =>
{
    option.User.RequireUniqueEmail = true;
    option.Password.RequiredUniqueChars = 0;
    option.Password.RequireUppercase = false;
    option.Password.RequiredLength = 8;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PracticeReactContext>()
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

/*for authen by bearer token*/
//builder.Services.AddIdentityApiEndpoints<User>(option => {
//    option.User.RequireUniqueEmail = true;
//    option.Password.RequiredUniqueChars = 0;
//    option.Password.RequireUppercase = false;
//    option.Password.RequiredLength = 8;
//})
//    .AddEntityFrameworkStores<PracticeReactContext>();

//add service to the container

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