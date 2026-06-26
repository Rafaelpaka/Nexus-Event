# ADR 001: Escolha do Micro-ORM Dapper

- **Status:** Aceito
- **Data:** 2026-06-17

## 📌 Contexto

Precisávamos de uma biblioteca de acesso a dados para o backend .NET 9 do NexusEvent, sistema de venda de ingressos. As principais alternativas consideradas foram:

- **Entity Framework Core** (ORM completo)
- **Dapper** (micro-ORM)
- **ADO.NET puro** (SqlConnection + SqlCommand)

Os critérios de avaliação incluíram: performance, simplicidade, controle sobre SQL, curva de aprendizado e adequação ao porte do projeto.

## ✅ Decisão

Optamos por utilizar **Dapper** como nossa camada de acesso a dados.

**Motivos principais:**
1. O sistema possui consultas com JOINs e regras de negócio específicas que se beneficiam de SQL explícito.
2. O Dapper oferece performance próxima ao ADO.NET puro, com a conveniência de mapeamento automático para objetos.
3. A equipe já possuía familiaridade com SQL, tornando o Dapper mais produtivo que um ORM completo.
4. O porte do projeto (poucas entidades, consultas diretas) não justifica a complexidade adicional do Entity Framework.

## 🔄 Consequências

### Prós:
- Performance elevada em consultas com JOIN e agregações.
- Controle total sobre o SQL executado no banco.
- Código mais enxuto comparado ao ADO.NET puro.
- Facilidade para otimizar queries específicas do domínio de eventos.

### Contras:
- Não há geração automática de migrações (scripts SQL devem ser versionados manualmente em [`db/create_tables.sql`](../db/create_tables.sql)).
- Ausência de change tracking — atualizações exigem SQL explícito.
- Maior acoplamento com o schema do banco de dados.
- Sem validação de integridade referencial em tempo de compilação.
