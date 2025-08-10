
using Microsoft.EntityFrameworkCore;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ITIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
            // Builder => it's refrences to appsettings and i say to him go to section called ConnectionString and get the value of cs key
            builder.Services.AddScoped<IDepartment, DepartmentRepo>();

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
