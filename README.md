# NexusEvent - Grupo composto pelos seguintes integrantes:

Carol Diaz - 06010688

Heloiza Custódio - 06009234

Larissa Ferreira - 0601175

Lohana Delgado - 06009900

Rafael de Alcantara - 06010477
# NexusEvent 🎟️

Sistema de venda de ingressos desenvolvido com ASP.NET Core Minimal API, Blazor WebAssembly, Dapper e SQL Server Express.
Sistema de venda de ingressos desenvolvido com ASP.NET Core Minimal API, Blazor WebAssembly, Dapper e SQL Server Express.

## Requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [SQL Server Management Studio - SSMS](https://aka.ms/ssmsfullsetup)
- Navegador moderno (Chrome, Edge, Firefox)
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
> ⚠️ Se aparecer erro de SSL, marque a opção **"Certificado do Servidor de Confiança"**

Execute o script de criação do banco:
```
/db/create_tables.sql
```
Abra o arquivo no SSMS e pressione **F5**

### 3. Configure o arquivo `.env`

Crie o arquivo `.env` dentro da pasta `src/backend/`:
```bash
cp src/backend/.env.example src/backend/.env
```

Edite o arquivo com suas configurações:
```env
ADMIN_PASSWORD=Admin@123
ADMIN_EMAIL=admin@nexusevent.com
ADMIN_CPF=000.000.000-00
DB_CONNECTION=Server=localhost\SQLEXPRESS;Database=NexusEvent;Trusted_Connection=True;TrustServerCertificate=True;
```

> ⚠️ O arquivo `.env` não é enviado para o repositório. Nunca compartilhe suas credenciais.

### 4. Instale os pacotes do Backend
```bash
cd src/backend
dotnet add package Dapper
dotnet add package Microsoft.Data.SqlClient
dotnet add package Swashbuckle.AspNetCore
dotnet add package DotNetEnv
dotnet restore
```

### 5. Instale os pacotes do Frontend
```bash
cd src/frontend
dotnet add package Microsoft.AspNetCore.Components.WebAssembly
dotnet add package Microsoft.AspNetCore.Components.WebAssembly.DevServer
cd src/frontend
dotnet add package Microsoft.AspNetCore.Components.WebAssembly
dotnet add package Microsoft.AspNetCore.Components.WebAssembly.DevServer
dotnet restore
```

## Rodando o Projeto

### Backend — Terminal 1

### Backend — Terminal 1
```bash
cd src/backend
dotnet run
```
Acesse o Swagger em:
```
http://localhost:5178/swagger
```

### Frontend — Terminal 2
```bash
cd src/frontend
dotnet run
```
Acesse o sistema em:
```
http://localhost:5177
```

> ⚠️ As portas podem variar. Confirme no terminal após o `dotnet run`

## Usuário Administrador

O sistema cria automaticamente um usuário administrador ao iniciar com base no `.env`:

| Campo | Valor padrão |
|---|---|
| CPF | `000.000.000-00` |
| Email | `admin@nexusevent.com` |
| Senha | `Admin@123` |

> ⚠️ O administrador tem acesso exclusivo ao cadastro de eventos e cupons.

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/usuarios` | Cadastra um usuário |
| POST | `/api/usuarios/login` | Realiza login |
| POST | `/api/usuarios/login` | Realiza login |
| POST | `/api/eventos` | Cadastra um evento |
| GET | `/api/eventos` | Lista todos os eventos |
| POST | `/api/cupons` | Cadastra um cupom |
| PUT | `/api/cupons/{codigo}/desativar` | Desativa um cupom |
| PUT | `/api/cupons/{codigo}/desativar` | Desativa um cupom |
| GET | `/api/reservas/{cpf}` | Lista reservas por CPF |
| POST | `/api/reservas` | Realiza uma reserva |
| DELETE | `/api/reservas/{id}/{cpf}` | Cancela uma reserva |
| DELETE | `/api/reservas/{id}/{cpf}` | Cancela uma reserva |

## Exemplos de Uso

### Cadastrar Usuário
```json
POST /api/usuarios
{
  "cpf": "123.456.789-00",
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
  "precoPadrao": 150.00
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
  "valorMinimoRegra": 100.00,
  "limiteUsoPorUsuario": 2,
  "disponibilidade": true
}
```

### Realizar Reserva
```json
POST /api/reservas
{
  "usuarioCpf": "123.456.789-00",
  "eventoId": 1,
  "codigoCupom": "DESCONTO10"
}
```

### Cancelar Reserva
```
DELETE /api/reservas/1/123.456.789-00
```

### Cancelar Reserva
```
DELETE /api/reservas/1/123.456.789-00
```

### Listar Reservas por CPF
```
GET /api/reservas/123.456.789-00
```

## Regras de Negócio

| Regra | Descrição |
|---|---|
| R1 | CPF e EventoId devem existir no banco |
| R2 | Mesmo CPF não pode ter mais de 2 reservas por evento |
| R3 | Não é possível reservar em evento lotado |
| R4 | Desconto só é aplicado se o preço for maior ou igual ao valor mínimo do cupom |

## Estrutura do Repositório


```
NexusEvent/
├── db/
│   └── create_tables.sql
├── docs/
│   └── requisitos.md
├── src/
│   ├── backend/
│   │   ├── DTOs/
│   │   ├── Entities/
│   │   ├── Repositories/
│   │   ├── Services/
│   │   ├── Validators/
│   │   ├── .env.example
│   │   ├── appsettings.json
│   │   └── Program.cs
│   └── frontend/
│       ├── Components/
│       │   ├── Layout/
│       │   └── Pages/
│       ├── Models/
│       ├── Services/
│       ├── wwwroot/
│       ├── wwwroot/
│       └── Program.cs
├── tests/
│   └── NexusEvent.Tests/
└── README.md
```

## Tecnologias

| Tecnologia | Uso |
|---|---|
| ASP.NET Core 9 | Minimal API Backend |
| Blazor WebAssembly | Frontend |
| ASP.NET Core 9 | Minimal API Backend |
| Blazor WebAssembly | Frontend |
| Dapper | Acesso ao banco de dados |
| SQL Server Express | Banco de dados |
| DotNetEnv | Variáveis de ambiente |
| xUnit | Testes automatizados |
| Swagger | Documentação da API |
