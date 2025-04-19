using extractor_c.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<OpenAIService>();
builder.Services.AddSingleton<PdfService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
