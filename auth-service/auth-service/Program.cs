
using auth_service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

var conString = Context.GetConnectionString();
builder.Services.AddDbContext<Context>();

var app = builder.Build();

Context.UpdateDatabase(app.Services);

app.UseCors(policy =>
{
    policy.AllowAnyHeader();
    policy.AllowAnyMethod();
    policy.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Welcome");

app.MapGet("/auth", (HttpContext context) =>
{
    var dbContext = Context.GetDatabase(app.Services);

        Console.WriteLine($"DATABASE BAÐLANILIYOR connection string : {Context.GetConnectionString()}");
        try
        {
            var obj = dbContext.Sessions.Where(session => session.SessionKey == "merabalar").FirstOrDefault();
            if (obj != null)
                return obj.SessionKey;
            Console.WriteLine($"Ekleme iþlemi yapýlýyor");
            dbContext.Sessions.Add(new auth_service.Models.Session());
            dbContext.SaveChanges();
            return "Yeni Ekleme Yapýldý";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return "Internal Error";
        }
        

});

app.Run();