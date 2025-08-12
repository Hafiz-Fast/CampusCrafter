using Student_Freelance_Backend.Repositories;
using Student_Freelance_Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILogginService, LogginService>();
builder.Services.AddScoped<ILogginRepo, LogginRepo>();

builder.Services.AddScoped<ISigninService, SigninService>();
builder.Services.AddScoped<ISigninRepo, SigninRepo>();

builder.Services.AddScoped<IFreelancerService, FreelancerService>();
builder.Services.AddScoped<IFreelancerRepo, FreelancerRepo>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
