using frontend.Components;

var builder = WebApplication.CreateBuilder(args);

// --- 1. SERVIÇOS ESSENCIAIS (NÃO COMENTAR!) ---
// Estes serviços permitem que as páginas .razor e a segurança básica funcionem
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// --- 2. SERVIÇOS DO BACKEND (MANTENHA COMENTADOS) ---
// builder.Services.AddScoped<backend.Repositories.EventoRepository>();
// builder.Services.AddScoped<backend.Services.EventoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// 3. Pode manter ou comentar se tiver erro de portas, mas o Antiforgery DEVE ficar.
app.UseHttpsRedirection();
app.UseAntiforgery(); 

app.MapStaticAssets();

// 4. Mapeia os componentes para que o site abra
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();