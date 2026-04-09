using backend.DTOs.Cupom;
using backend.DTOs.Evento;
using backend.DTOs.Reserva;
using backend.DTOs.Usuario;
using backend.Entities;
using backend.Repositories;
using backend.Services;
using backend.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirBlazor", policy =>
    {
        policy.WithOrigins("https://localhost:7000", "http://localhost:5000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration
    .GetConnectionString("DefaultConnection")!;

builder.Services.AddScoped<UsuarioRepository>(_ =>
    new UsuarioRepository(connectionString));
builder.Services.AddScoped<EventoRepository>(_ =>
    new EventoRepository(connectionString));
builder.Services.AddScoped<CupomRepository>(_ =>
    new CupomRepository(connectionString));
builder.Services.AddScoped<ReservaRepository>(_ =>
    new ReservaRepository(connectionString));

builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<CupomService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<SeedService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("PermitirBlazor");

using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<SeedService>();
    await seed.CriarAdminSeNaoExistir();
}

// ==========================================
// POST /api/usuarios
// ==========================================
app.MapPost("/api/usuarios", async (
    CriarUsuarioRequest request,
    UsuarioService service) =>
{
    try
    {
        UsuarioValidator.ValidarCamposObrigatorios(request);
        UsuarioValidator.ValidarEmail(request.Email);
        UsuarioValidator.ValidarSenhaForte(request.Senha);

        var entity = new UsuarioEntity
        {
            Cpf = request.Cpf,
            Nome = request.Nome,
            Email = request.Email,
            Login = request.Login,
            SenhaHash = request.Senha,
            Telefone = request.Telefone,
            Endereco = request.Endereco
        };

        await service.CriarUsuarioAsync(entity);
        return Results.Created($"/api/usuarios/{entity.Cpf}", entity);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// ==========================================
// POST /api/eventos
// ==========================================
app.MapPost("/api/eventos", async (
    CriarEventoRequest request,
    EventoService service) =>
{
    try
    {
        var entity = new EventoEntity
        {
            Nome = request.Nome,
            CapacidadeTotal = request.CapacidadeTotal,
            DataEvento = request.DataEvento,
            PrecoPadrao = request.PrecoPadrao
        };

        var (sucesso, mensagem) = await service.Cadastrar(entity);

        if (!sucesso)
            return Results.BadRequest(mensagem);

        return Results.Created($"/api/eventos/{entity.Id}", entity);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// ==========================================
// GET /api/eventos
// ==========================================
app.MapGet("/api/eventos", async (EventoService service) =>
{
    var eventos = await service.ListarTodos();
    return Results.Ok(eventos);
});

// ==========================================
// POST /api/cupons
// ==========================================
app.MapPost("/api/cupons", async (
    CriarCupomRequest request,
    CupomService service) =>
{
    try
    {
        var entity = new CupomEntity
        {
            Codigo = request.Codigo,
            PorcentagemDesconto = request.PorcentagemDesconto,
            ValorMinimoRegra = request.ValorMinimoRegra,
            LimiteUsoPorUsuario = request.LimiteUsoPorUsuario,
            Disponibilidade = request.Disponibilidade
        };

        var (sucesso, mensagem) = await service.Cadastrar(entity);

        if (!sucesso)
            return Results.BadRequest(mensagem);

        return Results.Created($"/api/cupons/{entity.Codigo}", entity);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// ==========================================
// PUT /api/cupons/{codigo}/desativar
// ==========================================
app.MapPut("/api/cupons/{codigo}/desativar", async (
    string codigo,
    ReservaService service) =>
{
    var (sucesso, mensagem) = await service.Desativar(codigo);

    if (!sucesso)
        return Results.BadRequest(mensagem);

    return Results.Ok(mensagem);
});

// ==========================================
// GET /api/reservas/{cpf}
// ==========================================
app.MapGet("/api/reservas/{cpf}", async (
    string cpf,
    ReservaService service) =>
{
    var reservas = await service.ListarPorCpf(cpf);
    return Results.Ok(reservas);
});

// ==========================================
// POST /api/reservas
// ==========================================
app.MapPost("/api/reservas", async (
    CriarReservaRequest request,
    ReservaService service) =>
{
    try
    {
        var (sucesso, mensagem, reserva) = await service.CriarReserva(
            request.UsuarioCpf,
            request.EventoId,
            request.CodigoCupom
        );

        if (!sucesso)
            return Results.BadRequest(mensagem);

        return Results.Created("/api/reservas", reserva);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// ==========================================
// DELETE /api/reservas/{id}/{cpf}
// ==========================================
app.MapDelete("/api/reservas/{id}/{cpf}", async (
    int id,
    string cpf,
    ReservaService service) =>
{
    try
    {
        var (sucesso, mensagem) = await service.Cancelar(id, cpf);

        if (!sucesso)
            return Results.BadRequest(mensagem);

        return Results.Ok(mensagem);
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.Run();