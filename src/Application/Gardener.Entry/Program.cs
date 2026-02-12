var builder = WebApplication.CreateBuilder(args).Inject();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Error");
}
app.UseBlazorFrameworkFiles();
app.MapFallbackToFile("index.html");
app.Run();