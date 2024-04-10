using AutoMapper;
using BL.Automapper;
using BL.Services;
using BL.Services.Interfaces;
using DAL;
using Microsoft.EntityFrameworkCore;
using WebAPI.Automapper;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ExpenseTrackerDbContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddTransient<ExpenseTrackerDbContext, ExpenseTrackerDbContext>();
            builder.Services.AddAutoMapper(typeof(DalBlMappingProfile),typeof(DtoBlMappingProfile));
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
