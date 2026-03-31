# NexusEvent 🎟️

Backend do sistema de venda de ingressos NexusEvent, desenvolvido com ASP.NET Core Minimal API, Dapper e SQL Server Express.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server Express](https://www.microsoft.com/sql-server/sql-server-downloads)
- [SQL Server Management Studio - SSMS](https://aka.ms/ssmsfullsetup)

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

### 3. Entre na pasta do projeto
```bash
cd src/backend
```

### 4. Instale os pacotes
```bash
dotnet add package Dapper
dotnet add package Microsoft.Data.SqlClient
dotnet add package Swashbuckle.AspNetCore
```

### 5. Restaure as dependências
```bash
dotnet restore
```

## Rodando o Projeto
```bash
cd src/backend
dotnet run
```

Acesse o Swagger em:
```
http://localhost:5178/swagger
```
> ⚠️ A porta pode variar. Confirme no terminal após o `dotnet run`

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| POST | `/api/usuarios` | Cadastra um usuário |
| POST | `/api/eventos` | Cadastra um evento |
| GET | `/api/eventos` | Lista todos os eventos |
| POST | `/api/cupons` | Cadastra um cupom |
| GET | `/api/reservas/{cpf}` | Lista reservas por CPF |
| POST | `/api/reservas` | Realiza uma reserva |

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
  "valorMinimoRegra": 100.00
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
│   └── backend/
│       ├── Entities/
│       ├── Repositories/
│       ├── Services/
│       ├── DTOs/
│       ├── Validators/
│       ├── appsettings.json
│       └── Program.cs
├── tests/
│   └── NexusEvent.Tests/
└── README.md
```

## Tecnologias

| Tecnologia | Uso |
|---|---|
| ASP.NET Core 8 | Minimal API |
| Dapper | Acesso ao banco de dados |
| SQL Server Express | Banco de dados |
| xUnit | Testes automatizados |
| Swagger | Documentação da API |
