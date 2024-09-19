using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Domain.ModelViews;
using MinimalApi.Domain.Services;
using MinimalApi.Domain.Validator;
using MinimalApi.Domain.DTOs;
using MinimalApi.Infra.Db;
using MinimalApi.Domain.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var key = builder.Configuration.GetSection("Jwt").ToString();

builder.Services.AddAuthentication(option => {
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option => {
    option.TokenValidationParameters = new TokenValidationParameters{
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
    };
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<iAdminService, AdminService>();
builder.Services.AddScoped<iVehicleService, VehicleService>();

builder.Services.AddDbContext<DbContexto>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("mysql") ?? "", new MySqlServerVersion(new Version(8, 0, 26)));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


#region Home
app.MapGet("/", () => Results.Json(new Home())).WithTags("Home");
#endregion

#region Admin
string GenJwtToken(Admin admin){
    if(string.IsNullOrEmpty(key)) return string.Empty;

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    var claims = new List<Claim>(){
        new Claim("Email", admin.Email),
        new Claim("Profile", admin.Profile.ToString())
    };
    

       var token = new JwtSecurityToken(
        claims: claims,
        signingCredentials: credentials, 
        expires: DateTime.UtcNow.AddDays(1) 
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

app.MapPost("/admins/login", ([FromBody] LoginDTO loginDTO, iAdminService adminService) =>
{
    var adm = adminService.Login(loginDTO);;
    if (adm != null)
    {
        string token = GenJwtToken(adm);
        return Results.Ok(new LoggedAdminModelView{
            Email = adm.Email,
            Profile = adm.Profile,
            Token = token
        });
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Admin");

app.MapPost("/admins", ([FromBody] AdminDTO adminDTO, iAdminService adminService) =>
{
    if (AdminDTOValidator.Validate(adminDTO).Count != 0)
    {
        return Results.BadRequest(new ValidationError { Messages = AdminDTOValidator.Validate(adminDTO) });
    }

    var admin = new Admin
    {
        Name = adminDTO.Name,
        Email = adminDTO.Email,
        Password = adminDTO.Password,
        Profile = adminDTO.Profile.ToString() ?? Profile.User.ToString()
    };
    adminService.Create(admin);

    return Results.Created($"/admins/{admin.Id}", admin);

}).RequireAuthorization().WithTags("Admin");

app.MapGet("/admins", ([FromQuery] int? page, iAdminService adminService) =>
{
    var pageParam = page;
    var admins = adminService.All(pageParam).Select(a => new AdminModelView
    {
        Id = a.Id,
        Email = a.Email,
        Profile = a.Profile
    });

    if (admins.Count() > 0)
    {
        return Results.Ok(admins);
    }
    return Results.NotFound();

}).RequireAuthorization().WithTags("Admin");

app.MapGet("/admins/{id}", ([FromRoute] int id, iAdminService adminService) =>
{
    var admin = adminService.FindById(id);
    if (admin == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(admin);

}).RequireAuthorization().WithTags("Admin");
#endregion

#region Vehicle
app.MapPost("/vehicle", ([FromBody] VehicleDTO vehicleDTO, iVehicleService vehicleService) =>
{
    if (VehicleDTOValidator.Validate(vehicleDTO).Count != 0)
    {
        return Results.BadRequest(new ValidationError { Messages = VehicleDTOValidator.Validate(vehicleDTO) });
    }

    var vehicle = new Vehicle
    {
        Name = vehicleDTO.Name,
        Marca = vehicleDTO.Marca,
        Ano = vehicleDTO.Ano
    };
    vehicleService.Create(vehicle);

    return Results.Created($"/vehicle/{vehicle.Id}", vehicle);

}).RequireAuthorization().WithTags("Vehicles");

app.MapGet("/vehicle", ([FromQuery] int? page, iVehicleService vehicleService) =>
{
    var vehicle = vehicleService.ReturnAll(page);

    return Results.Ok(vehicle);

}).WithTags("Vehicles");

app.MapGet("/vehicle/{id}", ([FromRoute] int id, [FromBody] VehicleDTO vehicleDTO, iVehicleService vehicleService) =>
{
    var vehicle = vehicleService.FindById(id);

    if (vehicle == null)
        return Results.NotFound();

    vehicle.Name = vehicleDTO.Name;
    vehicle.Marca = vehicleDTO.Marca;
    vehicle.Ano = vehicleDTO.Ano;

    vehicleService.Update(vehicle);

    return Results.Ok(vehicle);

}).WithTags("Vehicles");

app.MapPut("/vehicle/{id}", ([FromRoute] int id, iVehicleService vehicleService) =>
{
    var vehicle = vehicleService.FindById(id);

    if (vehicle == null)
        return Results.NotFound();

    return Results.Ok(vehicle);

}).RequireAuthorization().WithTags("Vehicles");

app.MapDelete("/vehicle/{id}", ([FromRoute] int id, iVehicleService vehicleService) =>
{
    var vehicle = vehicleService.FindById(id);
    if (vehicle == null)
        return Results.NotFound();
    vehicleService.Delete(vehicle);
    return Results.NoContent();

}).RequireAuthorization().WithTags("Vehicles");

#endregion

#region Dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();
    app.UseAuthorization();
}
#endregion

app.Run();

