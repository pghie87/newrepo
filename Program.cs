using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace EmsalEWayBillSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            // Register application services
            builder.Services.AddScoped<IEWayBillService, EWayBillService>();
            builder.Services.AddScoped<IEWayBillAPIClient, EWayBillAPIClient>();
            builder.Services.AddScoped<IEWayBillValidator, EWayBillValidator>();
            builder.Services.AddScoped<IEWayBillMapper, EWayBillMapper>();
            
            // Add DbContext
            builder.Services.AddDbContext<ApplicationDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline
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