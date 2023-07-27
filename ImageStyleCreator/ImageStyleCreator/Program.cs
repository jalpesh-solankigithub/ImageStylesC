using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ImageStyleCreator;
using ImageStyleCreator.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ImageStyleCreatorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ImageStyleCreatorContext") ?? throw new InvalidOperationException("Connection string 'ImageStyleCreatorContext' not found.")));

// Add services to the container.
builder.Services.AddTransient<IImageRepository, ImageRepository>();

// Register the PhotoService as a singleton with the connection string parameter
string azureBlobConnectionString = builder.Configuration.GetConnectionString("AzureBlobStorage");
builder.Services.AddSingleton<IImageService>(new ImageService(azureBlobConnectionString));
var containerName = "jalpeshcontainer";
builder.Services.AddSingleton<ImageProcessor>(new ImageProcessor(azureBlobConnectionString, containerName));

//builder.Services.AddCors();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
	builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsapp"); 
app.UseAuthorization();

app.MapControllers();

app.Run();
