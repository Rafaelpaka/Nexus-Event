
# Contexto do Sistema

| Item | Descrição |
|------|------------|
| **Nome do Sistema** | NexusEvent |
| **Objetivo** | Gerenciamento de vendas de ingressos para eventos |
| **Foco Principal** | Segurança, estabilidade e confiabilidade |
| **Problemas a Evitar** | Venda acima da capacidade, fraudes por CPF, falhas de validação no backend, prejuízos financeiros |

---

# Critérios Determinantes

| Critério | Descrição |
|-----------|------------|
| Controle de Capacidade | O sistema não pode permitir vendas acima do limite do local |
| Validação Backend | Todas as regras críticas devem ser validadas no backend |
| Controle de Concorrência | Impedir compras simultâneas que ultrapassem o limite disponível |
| Limite por CPF | Definir e validar quantidade máxima de ingressos por CPF |
| Auditoria | Regras de negócio devem ser transacionais e rastreáveis |
| Alta Demanda | O sistema deve manter integridade sob alto volume de acessos |

---

# Maiores Riscos Identificados

| Risco | Descrição | Nível |
|--------|------------|--------|
| Mudança de Escopo | Alterações frequentes nos requisitos | Alto |
| Fraude | Compras múltiplas no mesmo CPF por falha de validação | Alto |
| Venda Acima da Capacidade | Comercialização além do limite físico | Alto |
| Segurança de Vida | Impacto físico aos usuários | Baixo |

---

# Modelo de Ciclo Recomendado

| Item | Descrição |
|------|------------|
| **Modelo Escolhido** | Incremental e Iterativo |
| **Motivação** | Entregas parciais e validação contínua |
| **Benefícios** | Redução de riscos, adaptação a mudanças, feedback constante |

---

# Justificativa Técnica

| Aspecto | Justificativa |
|----------|---------------|
| Mitigação de Riscos | Prioriza módulos críticos como controle de capacidade e limite por CPF |
| Testabilidade | Permite testes unitários, integração e carga desde os primeiros incrementos |
| Feedback | Entregas frequentes possibilitam validação com stakeholders |
| Escalabilidade | Evolução gradual da arquitetura com base em métricas reais |
| Controle de Escopo | Ajustes podem ser feitos a cada iteração com menor impacto |
```
