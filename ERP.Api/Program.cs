using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Aggiungi i servizi Swagger
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    //aggiunge la definizione di sicurezza per il Bearer Token
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.OpenApiSecurityScheme
    {
        Description = "Inserisci il token JWT in questo formato: Bearer {tuo_token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.ParameterLocation.Header,
        Type = Microsoft.OpenApi.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    //Applica il requisito a tutti gli endpoint
    c.AddSecurityRequirement(new Microsoft.OpenApi.OpenApiSecurityRequirement
    {
        {
            
            new Microsoft.OpenApi.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.BaseOpenApiReference
                {
                    Type = Microsoft.OpenApi.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

}); 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//register service (Dependency Injection)   
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurazione Auth JWT vulnerabile
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false, //vulnerabilitá (token non scadono mai)
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("QuestaEunaChiaveSegretaMoltoDebole123!"))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Attiva l'interfaccia Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();