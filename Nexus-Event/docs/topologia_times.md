# Topologia de Times (Team Topologies)

Mapeamento dos 4 tipos de time do **Team Topologies** para o contexto do NexusEvent.

---

## 1. Stream-aligned Team — Time de Feature

**Nome sugerido:** Time de Ingressos (Ticketing Squad)

**Responsabilidade:** Entregar funcionalidades de ponta a ponta relacionadas ao fluxo de venda de ingressos — cadastro de eventos, reservas, aplicação de cupons, consulta de reservas.

**Composição sugerida:**
- 2 Desenvolvedores Full-stack (.NET + Blazor)
- 1 QA
- 1 Product Owner (stakeholder)

**Interação:** Consome as plataformas fornecidas pelo Time de Plataforma. Interage com o Time Facilitador para treinamento em novas tecnologias.

---

## 2. Platform Team — Time de Plataforma

**Nome sugerido:** Time de Infraestrutura (Platform Squad)

**Responsabilidade:** Prover e manter a plataforma subjacente: banco de dados SQL Server, pipeline CI/CD, monitoramento (Application Insights), infraestrutura em nuvem.

**Composição sugerida:**
- 1 DevOps/SRE
- 1 DBA
- 1 Desenvolvedor Backend Sênior

**Interação:** Fornece APIs e serviços internos para os Stream-aligned Teams consumirem via autosserviço. Reduz a carga cognitiva dos times de feature.

---

## 3. Enabling Team — Time Facilitador

**Nome sugerido:** Time de Capacitação (Enablement Squad)

**Responsabilidade:** Acelerar outros times com experimentação, pesquisa, treinamento e ferramentas. Ajuda na adoção de Blazor WebAssembly, Dapper, e boas práticas de testes.

**Composição sugerida:**
- 1 Arquiteto de Software
- 1 Desenvolvedor Sênior com foco em qualidade

**Interação:** Trabalha por períodos limitados com cada Stream-aligned Team, transferindo conhecimento e saindo. Não possui propriedade de código em produção.

---

## 4. Complicated-Subsystem Team — Time de Subsistema Complexo

**Nome sugerido:** Time de Pagamentos e Segurança (Payments & Security Squad)

**Responsabilidade:** Desenvolver e manter o subsistema de validação de CPF, cálculo de descontos com cupons, e integração com gateways de pagamento — lógicas que exigem conhecimento matemático/financeiro especializado.

**Composição sugerida:**
- 1 Desenvolvedor Sênior com expertise em segurança
- 1 Especialista em meios de pagamento

**Interação:** Colaboram com os Stream-aligned Teams via APIs bem definidas. O time de feature nunca precisa entender os detalhes internos de cálculo de descontos ou integração bancária.
