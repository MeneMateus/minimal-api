using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Domain.Entities;
using MinimalApi.Domain.Interfaces;
using MinimalApi.Domain.ModelViews;
using MinimalApi.Domain.Services;
using MinimalApi.Domain.Validator;
using MinimalApi.DTOs;
using MinimalApi.Infra.Db;

var builder = WebApplication.CreateBuilder(args);


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
app.MapPost("/admin/login", ([FromBody] LoginDTO loginDTO, iAdminService adminService) =>
{
    if (adminService.Login(loginDTO) != null)
    {
        return Results.Ok();
    }
    else
    {
        return Results.Unauthorized();
    }
}).WithTags("Admin");
#endregion

#region Vehicle
app.MapPost("/vehicle", ([FromBody] VehicleDTO vehicleDTO, iVehicleService vehicleService) =>
{
    if(VehicleDTOValidator.Validate(vehicleDTO).Count != 0)
    {
        return Results.BadRequest(new ValidationError { Messages = VehicleDTOValidator.Validate(vehicleDTO)});
    }

    var vehicle = new Vehicle
    {
        Name = vehicleDTO.Name,
        Marca = vehicleDTO.Marca,
        Ano = vehicleDTO.Ano
    };
    vehicleService.Create(vehicle);

    return Results.Created($"/vehicle/{vehicle.Id}", vehicle);

}).WithTags("Vehicles");

app.MapGet("/vehicle", ([FromQuery] int? page, iVehicleService vehicleService) =>
{
    var vehicle = vehicleService.ReturnAll(page = page);

    return Results.Ok(vehicle);

}).WithTags("Vehicles");

app.MapGet("/vehicle/{id}", ([FromRoute] int id,[FromBody] VehicleDTO vehicleDTO, iVehicleService vehicleService) =>
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

}).WithTags("Vehicles");

app.MapDelete("/vehicle/{id}", ([FromRoute] int id, iVehicleService vehicleService) =>
{
    var vehicle = vehicleService.FindById(id);
    if (vehicle == null)
        return Results.NotFound();
    vehicleService.Delete(vehicle);
    return Results.NoContent();

}).WithTags("Vehicles");

#endregion

#region Dev
if (app.Environment.IsDevelopment())
{
    // Enable Swagger only in development
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion

app.Run();

