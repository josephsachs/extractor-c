using extractor_c.Services;
using extractor_c.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<OpenAIService>();
builder.Services.AddSingleton<PdfService>();
builder.Services.AddSingleton<FileHandlerService>();
builder.Services.Configure<OpenAIConfigData>(
    builder.Configuration.GetSection("OpenAI"));

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.MapControllers();

app.Run();
