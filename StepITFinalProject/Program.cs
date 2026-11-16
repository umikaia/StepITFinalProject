using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using StepITFinalProject.Data;
using StepITFinalProject.Services.Implementations;
using StepITFinalProject.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Baza>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(Program)
);
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
