using WebEmpresa.Components;

var builder = WebApplication.CreateBuilder(args);

// 1. Agregar servicios para los componentes de Razor e Interactividad del Servidor
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. CONFIGURACIÓN CORRECTA: Blazor buscará los datos en el puerto de la API (5126)
builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri("http://localhost:5126/") 
});

var app = builder.Build();

// 3. Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
