using E_Trade.API.Filters;
using E_Trade.Service.Configurations;
using E_Trade.Core.Repositories;
using E_Trade.Core.Services;
using E_Trade.Core.UnitOfWorks;
using E_Trade.Repository;
using E_Trade.Repository.Repositories;
using E_Trade.Repository.UnitOfWorks;
using E_Trade.Service.Mapping;
using E_Trade.Service.Services;
using E_Trade.Service.Validations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using E_Trade.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));


builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute()))
    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI Container
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<ETradeDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PSqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ETradeDbContext)).GetName().Name);
    });
});

// �yelik Sistemi Ekleme
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    // Identitiy i�in baz� ayarlamalar.
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ETradeDbContext>().AddDefaultTokenProviders();

// Token kontrol� i�in
builder.Services.AddAuthentication(opt =>
{
    // Elimizdeki Schema
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    
    // �ki schemay� konu�turma
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
    // Token'dan gelen schema
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    if (tokenOptions.Audience == null)
    {
        throw new Exception("Audience bo�");
    }
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        // Kontrol edilecek parametreler.
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],

        // Kontrol edilecek �zellikler.
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };

});


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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

await app.RunAsync();