using Microsoft.EntityFrameworkCore;
using RepositoryPatternWithUOW.EF;
using RepositoryPatternWithUOW.Core.Repositories;
using RepositoryPatternWithUOW.EF.ClassServices;
using RepositoryPatternWithUOW.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(ConnectionString,
        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)    
    ));

// builder.Services.AddScoped(typeof(IBaseRepository<>) , typeof(BaseRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection() ;

app.UseAuthorization();

app.MapControllers();

app.Run();
