var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors((options) =>
{
    options.AddPolicy("DevOps", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("http://localhost:4200")
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials();
    });
    options.AddPolicy("ProdOps", (corsBuilder) =>
    {
        corsBuilder.WithOrigins("https://myProductSite.com")
         .AllowAnyMethod()
         .AllowAnyHeader()
         .AllowCredentials();
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevOps");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdOps");
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
