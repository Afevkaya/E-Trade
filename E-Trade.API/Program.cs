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
using E_Trade.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOption"));

// Fluent Validation ile ilgili 
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
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IBasketService, BasketService>();


builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<ETradeDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PSqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ETradeDbContext)).GetName().Name);
    });
});

// Üyelik Sistemi Servis Ekleme
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    // Identitiy için bazý ayarlamalar.
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.User.AllowedUserNameCharacters = "abcçdefgðhýijklmnoöpqrstuüvwxyzABCÇDEFGÐHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789-._@/ ";
}).AddEntityFrameworkStores<ETradeDbContext>().AddDefaultTokenProviders();

// Token Servisill, token kontrolü için
builder.Services.AddAuthentication(opt =>
{
    // Elimizdeki Schema
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    
    // Ýki schemayý konuþturma
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
    // Token'dan gelen schema
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokenOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        // Kontrol edilecek parametreler.
        // Jwt den gelen parametreler alttaki parametreler ile kýyaslanacak.
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience[0],

        // Alttaki özellikler kontrol edilsin mi edilmesin mi?
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

app.UseCustomException();   // Custom Exception middleware

app.UseAuthentication();    // Doðrulama

app.UseAuthorization();     // Yetkilendirme

app.MapControllers();

await app.RunAsync();