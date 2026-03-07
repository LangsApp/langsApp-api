using LangApp.API;
using LangApp.API.Auth;
using LangApp.BLL.Words.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Add_API_DI(builder.Configuration);

builder.Services.AddAutoMapper(typeof(WordsProfile));

builder.Services.Add_Identity_Configuration();

builder.Services.Add_JWT_Configuration(builder.Configuration);


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    await IdentityBootstrapper.EnsureSuperAdminAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
