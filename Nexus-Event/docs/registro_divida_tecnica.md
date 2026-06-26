# Registro de Dívida Técnica

| ID da Dívida | Descrição Técnica | Freq. Alteração | Risco | Esforço | Decisão |
|---|---|---|---|---|---|
| DT-001 | Senha armazenada sem hash no endpoint POST /api/usuarios (SenhaHash = request.Senha) — a senha em texto puro é enviada ao banco sem aplicar SHA256 | Baixa | Alto | Baixo | Prioridade 1 (Imediato) |
| DT-002 | Duplicação do método GerarHash em UsuarioService e SeedService — viola DRY | Baixa | Médio | Baixo | Prioridade 2 (Próxima Sprint) |
| DT-003 | Método Desativar cupom está em ReservaService em vez de CupomService — viola SRP | Baixa | Médio | Baixo | Prioridade 2 (Próxima Sprint) |
| DT-004 | Ausência de salt no hash de senhas (SHA256 puro sem salt) — vulnerável a rainbow tables | Baixa | Alto | Médio | Prioridade 1 (Imediato) |
| DT-005 | Lógica de login (hash + comparação) implementada diretamente no endpoint Program.cs em vez de no UsuarioService | Baixa | Médio | Baixo | Prioridade 2 (Próxima Sprint) |
| DT-006 | Ausência de transação com lock na criação de reservas — condição de corrida em alta concorrência | Alta | Alto | Alto | Prioridade 1 (Imediato) |
| DT-007 | Validação de senha forte chamada no endpoint (Program.cs) em vez de no Service ou Entity | Média | Baixo | Baixo | Prioridade 3 (Aceitar/Ignorar) |
| DT-008 | Testes utilizam construtores que não existem nas entidades (ReservaEntity, UsuarioEntity com parâmetros nomeados) — testes não compilam | Média | Alto | Médio | Prioridade 1 (Imediato) |
