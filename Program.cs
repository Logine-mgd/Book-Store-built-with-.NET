
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project1.Mappings;
using Project1.Models;
using Project1.UnitofWork;
using System.Text;

namespace Project1
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
            builder.Services.AddDbContext<BookStoreContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
            builder.Services.AddScoped<unitofwork>();
            builder.Services.AddAutoMapper(typeof(Mapconfig));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();
            builder.Services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
    .AddJwtBearer(
    op =>
    {
        op.SaveToken = true;
        string key = "Secret key Logine Magdy Secret Key";
        var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        op.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = secertkey,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Book Store",
                    Version = "v1",
                    Description = "Book Store is a backend system designed to manage the operations of a book store.\n This API supports two primary types of users: Admins and Customers, providing tailored functionalities for each role."
                }
                );
                c.EnableAnnotations();
            });


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
