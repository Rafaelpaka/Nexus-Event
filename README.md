...
1° - criar solução na pasta NexusEvent
      

NexusEvent/
  - docs/
  - db/
  - src/
  - tests/


# 🚀 Backend - Estrutura Web API (DDD)

Este projeto utiliza a arquitetura de **Domain-Driven Design (DDD)** para garantir separação de preocupações, testabilidade e escalabilidade.

## 📂 Estrutura de Pastas

Abaixo está a hierarquia de pastas que todos os membros do grupo devem seguir:

```text
src/
├── 1-Presentation/          # Camada de Entrada (API)
│   └── MyApi.WebAPI/        # Controllers, Swagger, Program.cs
├── 2-Application/           # Orquestração e DTOs
│   ├── Interfaces/          # Interfaces de Serviços (AppServices)
│   ├── Services/            # Implementação da lógica de aplicação
│   └── DTOs/                # Objetos de transferência de dados (Input/Output)
├── 3-Domain/                # O Coração do Negócio (Regras de Ouro)
│   ├── Entities/            # Classes de domínio (Modelos)
│   ├── Interfaces/          # Interfaces de Repositórios e Serviços de Domínio
│   ├── Services/            # Lógica de negócio pura (Domain Services)
│   └── Validations/         # Regras de validação (ex: FluentValidation)
└── 4-Infrastructure/        # Detalhes de Implementação
    ├── Data/                # DbContext, Migrations (Entity Framework)
    ├── Repositories/        # Implementação do acesso ao banco
    └── CrossCutting/        # Logs, Injeção de Dependência, Configurações Globais