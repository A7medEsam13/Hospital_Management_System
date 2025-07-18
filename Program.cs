
using Hospital_Management_System.Models;
using Hospital_Management_System.Services;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Hospital_Management_System.Repository;


namespace Hospital_Management_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // registering swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            // registering automapper services
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));


            // registering the application db context with sql server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

            // registering the injection of services
            builder.Services.AddScoped<IPatientServices, PatientServices>();
            builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();
            builder.Services.AddScoped<IDoctorServices, DoctorServices>();
            builder.Services.AddScoped<IStaffServices, StaffServices>();
            builder.Services.AddScoped<IRoleServices, RoleServices>();

            builder.Services.AddScoped<IRoleRepository, RoleRepository>();

            builder.Services.AddIdentity<ApplicationUser, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(); 
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
