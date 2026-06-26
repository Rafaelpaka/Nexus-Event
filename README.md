# NexusEvent 🎟️

Sistema de venda de ingressos desenvolvido com ASP.NET Core Minimal API, Blazor WebAssembly, Dapper e SQL Server Express.

**Grupo:**
- Carol Diaz — 06010688
- Heloiza Custódio — 06009234
- Larissa Ferreira — 06011175
- Lohana Delgado — 06009900
- Rafael de Alcantara — 06010477

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [SQL Server Management Studio — SSMS](https://aka.ms/ssmsfullsetup)
- Navegador moderno (Chrome, Edge, Firefox)

## Instalação

### 1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/NexusEvent.git
cd NexusEvent
```

### 2. Configure o Banco de Dados
Abra o SSMS e conecte em:
```
Server name:    localhost\SQLEXPRESS
Authentication: Windows Authentication
```

Execute o script de criação do banco:
- Abra o arquivo `db/create_tables.sql` no SSMS e pressione **F5**

### 3. Configure o arquivo `.env`
Crie o arquivo `src/backend/.env`:
```env
ADMIN_CPF=00000000000
ADMIN_EMAIL=admin@teste.com
ADMIN_PASSWORD=Admin123!
```

### 4. Restaure os pacotes
```bash
cd src/backend
dotnet restore

cd ../frontend
dotnet restore
```

## Rodando o Projeto

### Backend — Terminal 1
```bash
cd src/backend
dotnet run
```
Acesse o Swagger em: `http://localhost:5178/swagger`

### Frontend — Terminal 2
```bash
cd src/frontend
dotnet run
```
Acesse o sistema em: `http://localhost:5177`

## Usuário Administrador

| Campo | Valor |
|-------|-------|
| CPF | `00000000000` |
| Email | `admin@teste.com` |
| Senha | `Admin123!` |

O administrador tem acesso ao cadastro de eventos e cupons via menu **⚙️ Admin**.

## Endpoints da API

| Método | Rota | Descrição |
|--------|------|-----------|
| POST | `/api/usuarios` | Cadastra um usuário |
| POST | `/api/usuarios/login` | Realiza login |
| GET | `/api/usuarios/get` | Lista todos os usuários |
| GET | `/api/usuarios/getByCpf/{cpf}` | Busca usuário por CPF |
| GET | `/api/usuarios/getByEmail/{email}` | Busca usuário por email |
| PUT | `/api/usuarios/update` | Atualiza dados do usuário |
| DELETE | `/api/usuarios/{cpf}` | Remove um usuário |
| POST | `/api/eventos` | Cadastra um evento |
| GET | `/api/eventos` | Lista todos os eventos |
| GET | `/api/eventos/estatisticas` | 📊 Estatísticas com JOIN |
| POST | `/api/eventos/pesquisar` | 🔍 Pesquisa avançada com filtros |
| POST | `/api/cupons` | Cadastra um cupom |
| PUT | `/api/cupons/{codigo}/desativar` | Desativa um cupom |
| GET | `/api/reservas/{cpf}` | Lista reservas por CPF (JOIN) |
| POST | `/api/reservas` | Realiza uma reserva |
| DELETE | `/api/reservas/{id}/{cpf}` | Cancela uma reserva |

## Exemplos de Uso

### Cadastrar Usuário
```json
POST /api/usuarios
{
  "cpf": "12345678900",
  "nome": "João Silva",
  "email": "joao@email.com",
  "login": "joaosilva",
  "senha": "Senha@123"
}
```

### Login
```json
POST /api/usuarios/login
{
  "email": "joao@email.com",
  "senha": "Senha@123"
}
```

### Cadastrar Evento
```json
POST /api/eventos
{
  "nome": "Show de Rock",
  "capacidadeTotal": 100,
  "dataEvento": "2026-12-01T20:00:00",
  "precoPadrao": 150.00,
  "imagemUrl": "https://exemplo.com/foto.jpg"
}
```

### Cadastrar Cupom
```json
POST /api/cupons
{
  "codigo": "DESCONTO10",
  "porcentagemDesconto": 10.00,
  "valorMinimoRegra": 100.00,
  "limiteUsoPorUsuario": 2,
  "disponibilidade": true
}
```

### Realizar Reserva
```json
POST /api/reservas
{
  "usuarioCpf": "12345678900",
  "eventoId": 1,
  "codigoCupom": "DESCONTO10"
}
```

### Cancelar Reserva
```
DELETE /api/reservas/1/12345678900
```

### Listar Reservas por CPF
```
GET /api/reservas/12345678900
```

## Regras de Negócio

| Regra | Descrição |
|-------|-----------|
| R1 | CPF e EventoId devem existir no banco |
| R2 | Mesmo CPF não pode ter mais de 2 reservas por evento |
| R3 | Não é possível reservar em evento lotado |
| R4 | Desconto só é aplicado se o preço ≥ valor mínimo do cupom |
| R5 | Cupom inativo ou com limite de uso excedido é recusado |
| R6 | Senha deve ter 8+ caracteres, maiúscula, minúscula, número e especial |

## Estrutura do Projeto

```
NexusEvent/
├── db/
│   └── create_tables.sql
├── docs/
│   ├── requisitos.md
│   ├── analise_arquitetura.md
│   ├── arquitetura.md
│   ├── fluxo_manutencao.md
│   ├── operacao.md
│   ├── plano_iteracao.md
│   ├── registro_divida_tecnica.md
│   ├── seguranca_ciclo.md
│   ├── tests.md
│   ├── topologia_times.md
│   └── adrs/
│       └── 001-escolha-do-micro-orm.md
├── src/
│   ├── backend/
│   │   ├── DTOs/
│   │   ├── Entities/
│   │   ├── Interfaces/
│   │   ├── Repositories/
│   │   ├── Services/
│   │   ├── Utils/
│   │   ├── Validators/
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── .env
│   ├── frontend/
│   │   ├── Components/
│   │   │   ├── Layout/
│   │   │   └── Pages/
│   │   ├── Models/
│   │   ├── Services/
│   │   ├── wwwroot/
│   │   └── Program.cs
│   └── tests/
│       ├── Entities/
│       └── backend.Tests.csproj
├── release_checklist_final.md
└── README.md
```

## Tecnologias

| Tecnologia | Uso |
|------------|-----|
| ASP.NET Core 9 | Minimal API Backend |
| Blazor WebAssembly | Frontend |
| Dapper | Micro ORM — acesso ao banco |
| SQL Server Express | Banco de dados |
| xUnit | Testes unitários |
| Swagger | Documentação da API |
| DotNetEnv | Variáveis de ambiente (.env) |
