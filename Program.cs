using Joonasw.AspNetCore.SecurityHeaders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddCsp(nonceByteAmount: 32);
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
// Content Security Policy
app.UseCsp(csp =>
{
    // If nothing is mentioned for a resource class, allow from this domain
    csp.ByDefaultAllow
        .FromSelf();
    
    // https://ajax.googleapis.com https://unpkg.com https://cdn.jsdelivr.net https://cdn.freshstatus.io
    csp.AllowScripts
        .FromSelf()
        .From("https://cdn.freshstatus.io")
        .From("https://unpkg.com")
        .From("https://ajax.googleapis.com")
        .From("https://cdn.jsdelivr.net")
        .AddNonce();

    https://fonts.googleapis.com https://unpkg.com https://cdnjs.cloudflare.com https://cdn.jsdelivr.net
    csp.AllowStyles
        .FromSelf()
        .From("https://fonts.googleapis.com")
        .From("https://unpkg.com")
        .From("https://cdnjs.cloudflare.com")
        .From("https://cdn.jsdelivr.net")
        .AddNonce();
    
    csp.AllowFonts
        .FromSelf()
        .From("https://fonts.gstatic.com")
        .From("https://fonts.googleapis.com");
    
    csp.AllowConnections
        .ToSelf()
        .To("wss://localhost:63039")
        .To("wss://localhost:63319")
        .To("wss://test.akkenterprise.com");
    
    csp.AllowImages
        .FromSelf();
    
    csp.AllowFraming
        .FromSelf()
        .From("https://botprodev.botpropanel.com")
        .From("https://botpro.ai");

    csp.AllowFormActions
        .ToSelf();

});


app.Run();

public static class Startup
{
    public static Guid Id { get; } = Guid.NewGuid();
}