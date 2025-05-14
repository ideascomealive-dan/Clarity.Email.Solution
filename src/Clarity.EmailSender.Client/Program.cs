var builder = WebApplication.CreateBuilder(args);

// Register HttpClient for calling the Web API
var emailApiBaseUrl = builder.Configuration["EmailApi:BaseUrl"];

builder.Services.AddHttpClient("EmailApi", client =>
{
    client.BaseAddress = new Uri(emailApiBaseUrl!); // API base
});

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
