using CognitiveServices.OCR.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITextRecogniationService, TextRecogniationService>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<AzureCognitiveServicesConfig>(builder.Configuration.GetSection(typeof(AzureCognitiveServicesConfig).Name));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
