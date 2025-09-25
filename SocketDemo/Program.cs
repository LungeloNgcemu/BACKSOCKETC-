using SocketDemo.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173", "https://socketfront-dh2g.onrender.com")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: UseRouting must come before UseCors
app.UseRouting();

// CORS must be after UseRouting and before UseEndpoints
app.UseCors("CorsPolicy");

app.UseAuthorization();

// UseEndpoints is not needed in minimal APIs, use MapHub directly
app.MapHub<ChatHub>("/chathub");
app.MapControllers();

app.Run();