
using API.Errors;
using API.helpers;
using API.Middleware;
using API.Services;
using AutoMapper;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(options =>
  options.UseSqlite(builder.Configuration.GetConnectionString("FullStackConnection")));
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
builder.Services.AddTransient(typeof(IPaginationServices<,>),typeof(PaginationService<,>));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<Core.Interfaces.IUnitOfWork, UnitOfWork> ();
builder.Services.Configure<ApiBehaviorOptions>(options=>{
  options.InvalidModelStateResponseFactory=actionContext=>{
    var errors=actionContext.ModelState.Where(e=>e.Value.Errors.Count>0)
    .SelectMany(x=>x.Value.Errors)
    .Select(x=>x.ErrorMessage).ToArray();

    var errorReponse=new ApiValidationReponse{
      Errors=errors
    };

    return new BadRequestObjectResult(errorReponse);
  };
});

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


app.UseCors("AllowAll");
// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var logger=services.GetRequiredService<ILogger<Program>>();
try{
  await context.Database.MigrateAsync();
  await StoreContextSeed.SeedAsync(context);
}
catch(Exception e){
  logger.LogError(e,"An error occured during migrations");
}

app.Run();
