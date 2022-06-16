using Conduit.Data;
using Microsoft.EntityFrameworkCore;
using Conduit.Middlewares;
using Conduit.Data.Repositories;
using Conduit.Domain.Repositories;
using Conduit.Domain.Services;
using Conduit.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
builder.Services.AddScoped<IUserRepository, UsersRepository>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

// Learn more about configuring Swagger/OpenAPI at //https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
IConfiguration configuration = builder.Configuration;


builder.Services.AddDbContext<Conduit.Data.AppContext>(options =>
{
    //options.UseSqlServer("Data Source=DESKTOP-ICHCNJM\\SQLEXPRESS;Initial Catalog = ConduitData;Integrated Security=True");
    options.UseSqlServer(configuration.GetSection("ConnectionStrings").GetSection("MyConn").Value);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Authentication Middleware
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();