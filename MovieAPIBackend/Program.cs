using MovieAPIBackend.Cache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => 
  { c.SwaggerDoc("v1", 
    new Microsoft.OpenApi.Models.OpenApiInfo 
    { Title = "TMDB Api - V1", 
      Version = "v1" 
    }); 
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "comments.xml");
    c.IncludeXmlComments(filePath);
});
builder.Services.AddSingleton<TMDBCache, TMDBCache>();
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
