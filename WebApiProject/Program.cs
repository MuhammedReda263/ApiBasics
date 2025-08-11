
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApiProject.Models;
using WebApiProject.Repository;

namespace WebApiProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(option=> option.SuppressModelStateInvalidFilter = true); // to of default modelState Validation
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddSwaggerGen(); // we replect by setting ehich is dow to can send authraize in header
            #region SwaggerSettings
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // add Bearer Token settings
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token.\n\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
            });
            #endregion
            builder.Services.AddDbContext<ITIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
            // Builder => it's refrences to appsettings and i say to him go to section called ConnectionString and get the value of cs key
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<ITIContext>();

            builder.Services.AddScoped<IDepartment, DepartmentRepo>();

            builder.Services.AddAuthentication(options =>
            {    //Check JWT in header
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // when you check if it's valid or not use JWT
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // when it's invalid show the default of this schema and it eill retuen unauthoraize
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; // other schemes work with jwt
            }
            ).AddJwtBearer (option => // Verified Jey
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false; // Not HTTPS
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:IssuerIp"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:AudienceIp"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"]))
                };
            })
                ;

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("All",
                    policy => policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });


            var app = builder.Build();

            app.UseCors("All");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
