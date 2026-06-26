# Operação

## Matriz de Riscos

| Risco | Probabilidade | Impacto | Estratégia | Ação Planejada | Gatilho |
|---|---|---|---|---|---|
| Venda de ingressos acima da capacidade do evento | Média | Alto | Mitigar | Implementar transação SQL com lock (UPDLOCK/HOLDLOCK) no método CriarReserva para evitar condição de corrida | Quando duas ou mais requisições POST /api/reservas forem recebidas simultaneamente para o mesmo evento com 1 ou menos lugares disponíveis |
| Exposição de senhas por falta de hash adequado | Baixa | Alto | Mitigar | Substituir SHA256 sem salt por PBKDF2/BCrypt com salt único por usuário em todos os pontos de cadastro e login | Quando um invasor obtiver acesso ao banco de dados e conseguir ler a coluna SenhaHash |
| Falha do SQL Server Express por limite de recursos | Baixa | Alto | Aceitar | Monitorar uso de CPU/memória e planejar migração para SQL Server Standard quando necessário | Quando o banco atingir 90% de utilização de CPU ou 80% da memória disponível por mais de 5 minutos consecutivos |
| Fraude por CPF inválido ou duplicado | Média | Alto | Mitigar | Implementar validação de CPF com cálculo de dígitos verificadores e verificação de CPF duplicado no cadastro | Quando um usuário tentar cadastrar múltiplas contas com variações do mesmo CPF em um intervalo menor que 1 hora |
| Indisponibilidade do frontend Blazor WebAssembly | Baixa | Médio | Mitigar | Configurar CDN para distribuição estática dos assets WASM e implementar fallback com Service Worker | Quando o tempo de carregamento da página inicial exceder 10 segundos para mais de 5% dos usuários em uma janela de 1 hora |

---

## Métricas Operacionais

### Métrica 1 — Métrica de Fluxo (DORA): Deployment Frequency

| Campo | Valor |
|---|---|
| **Nome da Métrica** | Frequência de Deploy |
| **O que Mede** | Número de deployments bem-sucedidos para produção por semana |
| **Fórmula** | `Frequência = Total de deploys bem-sucedidos na semana / 7 dias` |
| **Fonte de Dados** | Pipeline CI/CD (GitHub Actions), logs do servidor de produção |
| **Frequência de Coleta** | Semanal (a cada segunda-feira às 09:00) |
| **Limites de Saúde** | 🟢 Bom: ≥ 1 deploy/dia | 🟡 Regular: 1 deploy/semana | 🔴 Ruim: < 1 deploy/mês |
| **Ação se Violado** | Se ficar 🔴 por 2 semanas consecutivas, convocar reunião de revisão do pipeline e alocar 1 desenvolvedor para automatizar o processo de release |

### Métrica 2 — Métrica de Qualidade: Change Failure Rate (CFR)

| Campo | Valor |
|---|---|
| **Nome da Métrica** | Taxa de Falha de Mudanças |
| **O que Mede** | Percentual de deployments que resultam em falha em produção (rollback, incidente, bug crítico) |
| **Fórmula** | `CFR = (Total de deploys com falha / Total de deploys) × 100` |
| **Fonte de Dados** | Sistema de monitoramento (logs de erro, AZDO/GitHub Issues), relatórios de rollback |
| **Frequência de Coleta** | A cada deploy e consolidado mensalmente |
| **Limites de Saúde** | 🟢 Bom: CFR < 5% | 🟡 Regular: CFR 5-15% | 🔴 Ruim: CFR > 15% |
| **Ação se Violado** | Se CFR > 15% por 2 meses consecutivos, interromper deploys de novas features e dedicar 1 sprint inteira para refatoração e aumento de cobertura de testes |

---

## SLO (Service Level Objective)

### Rota Crítica: POST /api/reservas (Criação de Reserva)

| Campo | Valor |
|---|---|
| **SLI (Indicador)** | Proporção de requisições POST /api/reservas com resposta HTTP 201 (Created) em menos de 2000ms |
| **Fórmula de Coleta** | `SLI = (Total de requisições com status 201 e tempo < 2000ms / Total de requisições POST /api/reservas) × 100` |
| **Fonte do Dado** | Logs do servidor (Application Insights / Prometheus) coletados do endpoint |
| **Janela de Medição** | 30 dias corridos |
| **Alvo (SLO)** | 99.5% |

---

## Error Budget Policy

| Nível | Orçamento de Erro Restante | Resposta |
|---|---|---|
| **Nível 1** | ≥ 50% do orçamento disponível | Operação normal. Times podem continuar desenvolvendo novas features. Monitoramento passivo. |
| **Nível 2** | 20% a 49% do orçamento disponível | Acionar alerta para o time de plataforma. Priorizar correções de bugs sobre novas features. Revisar testes de carga. |
| **Nível 3** | < 20% do orçamento disponível | **Feature Freeze** — Zero novas funcionalidades. Todo o esforço do time é dedicado a melhorar a confiabilidade da rota POST /api/reservas. Apenas correções de segurança e bugs críticos são permitidos. |

> **Orçamento de Erro Total:** 100% - SLO = 100% - 99.5% = 0.5% de falhas toleradas na janela de 30 dias.
