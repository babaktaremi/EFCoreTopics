using EFCoreTopics.Database.Data;
using EFCoreTopics.Hubs.Stream;
using MessagePack;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AdventureWorksLContext>();

builder.Services.AddCors(c => c.AddPolicy("DefaultPolicy",policyBuilder =>policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() ));
builder.Services.AddSignalR(c =>
{
    c.ClientTimeoutInterval=TimeSpan.FromSeconds(45);
    c.EnableDetailedErrors = true;
    c.KeepAliveInterval=TimeSpan.FromSeconds(15);
}); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("DefaultPolicy");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<StreamingHub>("StreamingHub");
});
//BenchmarkRunner.Run<StreamingBenchmark>();
app.Run();
