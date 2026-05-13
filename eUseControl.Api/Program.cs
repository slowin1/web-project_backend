using EUseControl.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using eUseControl.BussinessLogic;
using eUseControl.BussinessLogic.Core;
using eUseControl.BussinessLogic.Interfaces;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUserService, UserService>();
//подключенние к бд 
builder.Services.AddDbContext<UserContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Убрали проверку if (app.Environment.IsDevelopment())
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();