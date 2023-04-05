using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.Classes;
using PharmacyMedicineSupply.Repository.EntityClasses;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IMedicineStockReposiroty<MedicineStock>, MedicineStockReposiroty>();
builder.Services.AddTransient<IManagerRepository, ManagerRepository>();
builder.Services.AddTransient<IMedicineDemandRepository<MedicineDemand>, MedicineDemandRepository>();
builder.Services.AddTransient<IPharmacyRepository<Pharmacy>, PharmacyRepository>();
builder.Services.AddTransient<IPharmacyMedSupplyRepository<PharmacyMedSupply>, PharmacyMedSupplyRepository>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();


builder.Services.AddDbContext<PharmacySupplyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthorizationMicroservice v1"));
}

app.UseCors(options => options.AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
