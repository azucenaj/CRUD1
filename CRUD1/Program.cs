global using CRUD1.Data;
global using Microsoft.EntityFrameworkCore;
global using Newtonsoft.Json.Serialization;
global using CRUD1.Model;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions => { sqlOptions.EnableRetryOnFailure(); });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c=>
c.AddPolicy("AllowOrigin",options=>options.AllowAnyOrigin()
                                            .AllowAnyMethod()
                                                .AllowAnyHeader()
                                                   ));
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
                                = new DefaultContractResolver());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(options => options.WithOrigins("http://localhost:7071").AllowAnyOrigin().
                                                                            AllowAnyMethod().
                                                                                AllowAnyHeader());
    app.UseCors("AllowOrigin");
    

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
