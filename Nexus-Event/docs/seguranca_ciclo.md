# Segurança no Ciclo de Vida

## Threat Model — Rota de Maior Risco

### Rota: POST /api/reservas (Criação de Reserva)

| Campo | Descrição |
|---|---|
| **Ativos Protegidos** | Dados de usuário (CPF, nome, email), registros de reserva, integridade de capacidade do evento, valor final pago |
| **Vetor de Ataque Provável** | Injeção de SQL via parâmetros não validados da requisição (UsuarioCpf, EventoId, CodigoCupom). Ataque de condição de corrida (race condition) para esgotar ingressos. Fraude de CPF inválido para reservas múltiplas |
| **Falha Arquitetural Potencial** | Ausência de transação no banco (todas as verificações e INSERT estão fora de uma transação única). Dados sensíveis trafegando sem criptografia. Validação de CPF apenas no frontend |
| **Controle de Engenharia (Mitigação)** | (1) Uso exclusivo de parâmetros nomeados do Dapper (@Parametro) eliminando risco de SQL injection. (2) Implementar transação com nível de isolamento SERIALIZABLE para prevenir condição de corrida. (3) Validar CPF no backend utilizando cálculo de dígitos verificadores. (4) Utilizar HTTPS obrigatório em produção com certificado válido |

---

## Gates de Segurança

### Gate 1 — Análise Estática de Segurança (SAST)
- **Ferramenta:** Roslyn Analyzers + Security Code Scan
- **Momento:** Antes de cada commit (pre-commit hook)
- **Verifica:** Presença de credenciais hardcoded (Password=, Pwd=, ConnectionString=), SQL injection potencial, uso de algoritmos criptográficos fracos
- **Critério de Aprovação:** Zero alertas de severidade alta ou crítica

### Gate 2 — Revisão de Dependências (SCA)
- **Ferramenta:** `dotnet list package --vulnerable` + GitHub Dependabot
- **Momento:** Semanalmente e antes de cada release
- **Verifica:** Bibliotecas com vulnerabilidades conhecidas (CVEs)
- **Critério de Aprovação:** Nenhuma dependência com CVE de severidade alta ou crítica sem patch disponível

### Gate 3 — Teste de Segurança Dinâmico (DAST)
- **Ferramenta:** OWASP ZAP (Zed Attack Proxy)
- **Momento:** Antes do deploy em produção
- **Verifica:** Injeção de SQL, XSS, vazamento de informações sensíveis nos responses da API, headers de segurança ausentes
- **Critério de Aprovação:** Nenhum alerta de risco alto ou crítico no relatório do ZAP
