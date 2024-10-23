using FinManagerGf.Shared;
using FinManagerGf.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplication();
builder.Services.AddRepository(GetAppSettingsFromProperties(builder.Configuration));

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:5173")
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ClientPermission");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();



Dictionary<string, string> GetAppSettingsFromProperties(IConfiguration configuration)
{
    var settings = new Dictionary<string, string>();

    var fields = typeof(AppSettingsPropertyNames).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);

    foreach (var field in fields)
    {
        var fieldValue = field.GetValue(null)?.ToString();

        if (!string.IsNullOrEmpty(fieldValue))
        {
            settings[fieldValue] = configuration.GetValue<string>(fieldValue)!;
        }
    }

    settings.Remove(AppSettingsPropertyNames.DefaultConnection);
    settings.Add(AppSettingsPropertyNames.DefaultConnection, configuration.GetConnectionString(AppSettingsPropertyNames.DefaultConnection)!);
    return settings;
}
