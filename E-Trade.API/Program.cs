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

// Üyelik Sistemi Ekleme
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    // Identitiy için bazý ayarlamalar.
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ETradeDbContext>().AddDefaultTokenProviders();

// Token kontrolü için
builder.Services.AddAuthentication(opt =>
{
    // Elimizdeki Schema
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    
    // Ýki schemayý konuþturma
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
    // Token'dan gelen schema
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    var tokeOptions = builder.Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        // Kontrol edilecek parametreler.
        IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokeOptions.SecurityKey),
        ValidIssuer = tokeOptions.Issuer,
        ValidAudience = tokeOptions.Audience[0],

        // Kontrol edilecek özellikler.
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
    };

});


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
