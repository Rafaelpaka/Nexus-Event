# Fluxo de Manutenção

## Parte 1: Classificação de Tickets (Taxonomia de Swanson)

### Ticket 1
**Descrição:** Usuário reporta que ao tentar realizar login, o sistema retorna "Senha incorreta" mesmo com a senha correta.
**Classificação:** 🔧 Corretiva

### Ticket 2
**Descrição:** Adicionar suporte a novos métodos de pagamento (PIX, cartão de crédito).
**Classificação:** ➕ Perfectiva

### Ticket 3
**Descrição:** Atualizar a versão do .NET 9.0 para a próxima versão LTS disponível.
**Classificação:** 🔄 Preventiva

### Ticket 4
**Descrição:** Integrar o sistema com gateway de pagamento externo (Stripe/PagSeguro).
**Classificação:** ➕ Perfectiva

### Ticket 5
**Descrição:** Adaptar a interface do frontend para ser responsiva em dispositivos móveis.
**Classificação:** 🔄 Adaptativa

### Ticket 6
**Descrição:** Erro 500 ao tentar criar reserva quando o banco está sob alta carga.
**Classificação:** 🔧 Corretiva

### Ticket 7
**Descrição:** Implementar cache Redis para consultas de eventos mais acessadas.
**Classificação:** ➕ Perfectiva

### Ticket 8
**Descrição:** Migrar o banco de dados de SQL Server Express para Azure SQL.
**Classificação:** 🔄 Adaptativa

### Ticket 9
**Descrição:** Adicionar testes de carga e estresse para o endpoint de reservas.
**Classificação:** 🔄 Preventiva

### Ticket 10
**Descrição:** Refatorar o código de hash de senha para usar BCrypt em vez de SHA256.
**Classificação:** 🔄 Preventiva

### Ticket 11
**Descrição:** Suporte a múltiplos idiomas (i18n) no frontend Blazor.
**Classificação:** ➕ Perfectiva

### Ticket 12
**Descrição:** Corrigir validação de CPF que aceita formatos inválidos.
**Classificação:** 🔧 Corretiva

---

## Parte 2: Pipeline de Liberação Segura

### Ticket de Exemplo: DT-006 — Corrigir condição de corrida em reservas

#### 1. Análise de Impacto
- **Escopo:** Serviço `ReservaService.CriarReserva()` e repositório `ReservaRepository`.
- **Risco:** Duas requisições simultâneas podem ultrapassar a capacidade do evento.
- **Impacto:** Prejuízo financeiro e insatisfação de clientes se mais ingressos forem vendidos que a capacidade.
- **Dependências:** Nenhuma — alteração isolada no backend.

#### 2. Teste como Instrumento Cirúrgico
- Criar teste de integração que simula duas chamadas simultâneas ao endpoint POST /api/reservas para o mesmo evento com 1 lugar disponível.
- O teste deve verificar que apenas uma reserva é criada com sucesso e a segunda retorna 400.
- Utilizar `Task.WhenAll` para disparar as chamadas concorrentes.

#### 3. Feature Toggle
- Adicionar flag `FeatureToggles:ConcurrencyControl` no [`appsettings.json`](src/backend/appsettings.json).
- Se `true`: usar transação com lock (`UPDLOCK`, `HOLDLOCK`).
- Se `false`: manter comportamento atual.
- Valor padrão: `false` (desligado) para deploy seguro.

#### 4. Estratégia de Release e Regressão
- **Release:** Deploy em ambiente de staging, ativar feature toggle, executar testes de carga.
- **Rollback:** Desligar feature toggle (`false`) e redeploy da versão anterior.
- **Regressão:** Executar suite completa de testes unitários (xUnit) + testes de integração.
- **Monitoramento:** Observar métrica de erros 400 no endpoint POST /api/reservas por 48h após deploy.
