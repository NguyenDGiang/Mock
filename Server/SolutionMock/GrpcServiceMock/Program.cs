using GrpcServiceMock.Models;
using GrpcServiceMock.Repositories;
using GrpcServiceMock.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddGrpc();
builder.Services.AddDbContext<NTQTRAININGContext>(item => item.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddCors();
var app = builder.Build();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors(options =>
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<CategoryService>();
app.MapGrpcService<ProductService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
