using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using VNWalks.API.Data;
using VNWalks.API.Mappings;
using VNWalks.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "VNWalks",
        Version = "v1",
    });

    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                },

                Scheme = "Oauth2",

                Name = JwtBearerDefaults.AuthenticationScheme,
                In = ParameterLocation.Header
            },

            new List<string>()
        }
    });
}
);


//Add Dependency Injecion and Register the database context
builder.Services.AddDbContext<VNWalksDbContext>(options =>
    //config to use SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("VNWalksConnectionString"))
);

builder.Services.AddDbContext<VNWalksAuthDbContext>(options =>
    //config to use SQL Server
    options.UseSqlServer(builder.Configuration.GetConnectionString("VNWalksAuthConnectionString"))
);

// Dua vao repository va SQlRepository  
builder.Services.AddScoped<IRegionRepositoty, SQLRegionRepository>();
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
builder.Services.AddScoped<ITokenRepository, SQLTokenRepository>();
builder.Services.AddScoped<IImageRepository, SQLImageRepository>();



//Dua Auto Mapping vao Ung Dung
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//Them thuoc tinh nhan dang (Identity) vao Program
builder.Services.AddIdentityCore<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("VNWalks")
    .AddEntityFrameworkStores<VNWalksAuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredUniqueChars = 1;
});


// Them Authentication vao Program
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
        //khi ma thong bao dc tao, chta se xac thuc ma thong bao dua tren nhung tham so nao
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        }
    );





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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images",
    // https://localhost:1234/Images
});

app.Run();
