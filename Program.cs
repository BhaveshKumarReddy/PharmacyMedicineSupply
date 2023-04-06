using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PharmacyMedicineSupply.Models;
using PharmacyMedicineSupply.Models.DTO.MedicalRepresentative;
using PharmacyMedicineSupply.Repository;
using PharmacyMedicineSupply.Repository.Classes;
using PharmacyMedicineSupply.Repository.EntityClasses;
using PharmacyMedicineSupply.Repository.EntityInterfaces;
using PharmacySupplyProject.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IDatesScheduleRepository<DatesSchedule> , DatesScheduleRepository>();
builder.Services.AddTransient<IRepresentativeScheduleRepository<RepresentativeSchedule>, RepresentativeScheduleRepository>();
builder.Services.AddTransient<IMedicalRepresentativeRepository<MedicalRepresentativeDTO>, MedicalRepresentativeRepository>();
builder.Services.AddTransient<IMedicineStockRepository<MedicineStock>, MedicineStockRepository>();
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
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

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
