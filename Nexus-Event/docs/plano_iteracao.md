# Plano de Iteração

## Objetivo da Iteração:
Implementar a funcionalidade completa de reserva de ingressos com controle de capacidade, validação de cupons de desconto e limite de 2 reservas por CPF por evento, juntamente com a documentação arquitetural SDD para a AV2.

## Escopo (Backlog Selecionado):
1. Corrigir hash de senha no endpoint POST /api/usuarios (DT-001)
2. Remover duplicação do método GerarHash (DT-002) criando classe HashUtils compartilhada
3. Mover método Desativar cupom de ReservaService para CupomService (DT-003)
4. Adicionar transação com lock na criação de reservas (DT-006)
5. Refatorar login para o UsuarioService (DT-005)
6. Corrigir construtores dos testes para compilar com as entidades (DT-008)
7. Criar novos endpoints: GET /api/eventos/estatisticas (com JOIN) e POST /api/eventos/pesquisar
8. Adicionar padrão AAA e renomear testes conforme nomenclatura padronizada
9. Produzir todos os artefatos SDD (ADR, análise arquitetural, dívida técnica, fluxo de manutenção, plano de iteração, operação, segurança, topologia de times)

## Entregáveis (Evidências):
- Código backend compilando e testado
- Testes unitários com nomenclatura padronizada e padrão AAA
- Documentos SDD em /docs/:
  - [`analise_arquitetura.md`](docs/analise_arquitetura.md)
  - [`adrs/001-escolha-do-micro-orm.md`](docs/adrs/001-escolha-do-micro-orm.md)
  - [`registro_divida_tecnica.md`](docs/registro_divida_tecnica.md)
  - [`fluxo_manutencao.md`](docs/fluxo_manutencao.md)
  - [`plano_iteracao.md`](docs/plano_iteracao.md)
  - [`operacao.md`](docs/operacao.md)
  - [`seguranca_ciclo.md`](docs/seguranca_ciclo.md)
  - [`topologia_times.md`](docs/topologia_times.md)
- Arquivo [`release_checklist_final.md`](release_checklist_final.md) na raiz

## Risco Principal do Ciclo:
Condição de corrida no endpoint de criação de reservas durante picos de demanda. A implementação da transação com lock (DT-006) é crítica para mitigar este risco e deve ser priorizada.

## Definição de Pronto (DoD):
- ✅ Código compila sem erros
- ✅ Todos os testes existentes passam
- ✅ Novos endpoints testados manualmente via Swagger
- ✅ Nenhuma credencial hardcoded em arquivos .cs
- ✅ Documentação SDD completa com 20 itens da tabela de avaliação
- ✅ Todos os arquivos revisados e consistentes

---

## Quadro Visual e Limite de WIP

### Colunas do Quadro Kanban

| Backlog | Em Desenvolvimento | Code Review | Concluído |
|---|---|---|---|
| Criar métricas operacionais | Corrigir DT-001 (hash senha) | ADR + Análise Arquitetural | Documentos SDD base |
| SLO e Error Budget | Corrigir DT-006 (lock reservas) | Refatoração de código | Plano de Iteração |
| Threat Model | Novos endpoints API | Novos endpoints | Fluxo de Manutenção |
| Topologia de Times | Testes AAA + nomenclatura | Testes unitários | Registro Dívida Técnica |

**WIP máximo:** 3 tarefas

> O limite de WIP é igual ao número de integrantes do grupo (5), estabelecendo um máximo de 3 tarefas simultâneas para garantir foco e qualidade nas entregas.
