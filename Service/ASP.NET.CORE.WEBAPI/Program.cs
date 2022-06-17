using BusinessProviders.Business;
using DataProviders.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Containers
builder.Services.AddSingleton<ISignUpProvider, SignUpProvider>();
builder.Services.AddSingleton<ISignUpDataProvider, SignUpDataProvider>();


// Read values from appsettings.json

//var connectionString = builder.Configuration["ConnectionStrings:Defaultconnection"];


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
