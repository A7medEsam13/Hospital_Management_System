using LoggerService;
using Hospital_Management_System.Models;
using Hospital_Management_System.Repository;
using Hospital_Management_System.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;


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
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

                // ? Add JWT Bearer authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter 'Bearer' followed by your token in the text box below.\r\n\r\nExample: \"Bearer abc123\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });



            // registering automapper services
            builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));


            // registering the application db context with sql server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            ///summary : 
            //// registering the injection of services
            #region injection of services

            builder.Services.AddScoped<IPatientServices, PatientServices>();
            builder.Services.AddScoped<IPatientRepository, PatientRepository>();
            builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();
            builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            builder.Services.AddScoped<IDoctorServices, DoctorServices>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IStaffServices, StaffServices>();
            builder.Services.AddScoped<IStuffRepository, StuffRepository>();
            builder.Services.AddScoped<IEmergencyContactRepository, EmergencyContactRepository>();
            builder.Services.AddScoped<IEmergencyContactServices, EmergencyContactServices>();
            builder.Services.AddScoped<IDiagnosisServices, DiagnosisServices>();
            builder.Services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
            builder.Services.AddScoped<IDiagnosisPatientRepository, DiagnosisPatientRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            #endregion

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Register the serilog.
           Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Information()
           .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

            

            




            builder.Services.AddAuthentication(options =>
            {
                // check jwt token header 
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                //[authorize]
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // unauthorize
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => // verified key
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:aud"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:securityKey"]))
                };
            });

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
