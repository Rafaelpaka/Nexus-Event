# Análise de Padrões Arquiteturais e Violações

## Parte 1: Padrões Arquiteturais

### Cenário 1 – Sistema de Venda de Ingressos com Alta Concorrência

**Padrão Provável:** Transação Otimista com Dapper (Optimistic Concurrency)

**Trade-off:**
- **Positivo:** Garante integridade dos dados em cenários de alta concorrência sem locks prolongados no banco. O Dapper é leve e performático para operações transacionais.
- **Negativo:** Exige tratamento de exceções de concorrência (conflitos de versão) no código da aplicação, aumentando a complexidade da lógica de reserva. Pode gerar retentativas e experiência de usuário instável em picos.

---

### Cenário 2 – Aplicação Web com Separação de Responsabilidades

**Padrão Provável:** Clean Architecture / Camadas (Presentation, Application, Infrastructure, Domain)

**Trade-off:**
- **Positivo:** Isola o domínio do negócio de detalhes de infraestrutura, permitindo trocar ORM, banco de dados ou interface sem afetar as regras de negócio.
- **Negativo:** Aumenta a quantidade de projetos e arquivos no repositório. Para sistemas pequenos, pode ser excessivo e gerar sobrecarga de manutenção.

---

### Cenário 3 – Comunicação entre Frontend e Backend via REST

**Padrão Provável:** RESTful API com Minimal API (HTTP + JSON)

**Trade-off:**
- **Positivo:** Simplicidade e baixo acoplamento. O frontend (Blazor WebAssembly) pode consumir a API de forma independente, facilitando a evolução separada de cada camada.
- **Negativo:** Sem estado (stateless) exige que cada requisição valide autenticação e autorização, aumentando a latência por chamada. Além disso, não há contrato formal forte como em GraphQL ou gRPC.

---

## Parte 2: Análise de Violações Arquiteturais

Trecho de código analisado (adaptado do [`Program.cs`](src/backend/Program.cs)):

```csharp
app.MapPost("/api/usuarios", async (
    CriarUsuarioRequest request,
    UsuarioService service) =>
{
    // ...
    var entity = new UsuarioEntity
    {
        Cpf = request.Cpf,
        Nome = request.Nome,
        Email = request.Email,
        Login = request.Login,
        SenhaHash = request.Senha,  // Violação
        Telefone = request.Telefone,
        Endereco = request.Endereco
    };
    // ...
});
```

### Violações Identificadas

---

**Problema:** Senha armazenada sem hash no cadastro
**Evidência:** Linha `SenhaHash = request.Senha` — a senha em texto puro é passada diretamente para a entidade, sem aplicar hash SHA256.
**Impacto:** Violação grave de segurança. Qualquer acesso ao banco de dados expõe senhas dos usuários.
**Ação Recomendada:** Aplicar hash SHA256 antes de atribuir à propriedade `SenhaHash`, assim como já é feito no serviço [`UsuarioService.CriarUsuarioAsync()`](src/backend/Services/UsuarioService.cs:41).

---

**Problema:** Lógica de hash duplicada no código
**Evidência:** [`UsuarioService.GerarHash()`](src/backend/Services/UsuarioService.cs:85) e [`SeedService.GerarHash()`](src/backend/Services/SeedService.cs:53) possuem implementações idênticas de SHA256.
**Impacto:** Viola o princípio DRY (Don't Repeat Yourself). Qualquer alteração no algoritmo de hash precisa ser replicada em dois lugares.
**Ação Recomendada:** Extrair o método `GerarHash` para uma classe estática compartilhada, como `HashUtils`, e referenciá-la de ambos os serviços.

---

**Problema:** Responsabilidade de desativar cupom está no `ReservaService`
**Evidência:** O método [`ReservaService.Desativar()`](src/backend/Services/ReservaService.cs:100) manipula cupons, mas deveria estar no `CupomService`.
**Impacto:** Viola o Princípio da Responsabilidade Única (SRP). Um serviço de reservas não deveria gerenciar cupons.
**Ação Recomendada:** Mover o método `Desativar` para [`CupomService`](src/backend/Services/CupomService.cs) e corrigir a referência no [`Program.cs`](src/backend/Program.cs:315).

---

**Problema:** Validação de senha fraca na camada de apresentação (endpoint)
**Evidência:** Em [`Program.cs`](src/backend/Program.cs:82), a chamada `UsuarioValidator.ValidarSenhaForte(request.Senha)` é feita diretamente no endpoint, em vez de estar encapsulada no serviço ou entidade.
**Impacto:** Viola a separação de camadas. Endpoints devem delegar validações e regras de negócio para as camadas inferiores (Services/Entities).
**Ação Recomendada:** Mover a validação de senha forte para dentro do método [`UsuarioService.CriarUsuarioAsync()`](src/backend/Services/UsuarioService.cs) ou para a própria entidade [`UsuarioEntity`](src/backend/Entities/UsuarioEntity.cs).

---

**Problema:** Uso de SHA256 para senhas sem salt
**Evidência:** [`UsuarioService.GerarHash()`](src/backend/Services/UsuarioService.cs:85) usa apenas `SHA256.Create()` com a senha pura, sem adicionar salt individual por usuário.
**Impacto:** Senhas idênticas geram hashes idênticos, tornando o sistema vulnerável a ataques de dicionário e rainbow tables.
**Ação Recomendada:** Substituir SHA256 por um algoritmo moderno como `Rfc2898DeriveBytes` (PBKDF2) com salt único por usuário, ou utilizar `BCrypt`/`Argon2`.

---

**Problema:** Lógica de negócio exposta em endpoints da API
**Evidência:** Em [`Program.cs`](src/backend/Program.cs:111-137), o endpoint de login contém lógica de hash e comparação de senha diretamente, em vez de delegar ao `UsuarioService`.
**Impacto:** Dificulta a testabilidade e manutenção. Viola a separação de responsabilidades entre camada de transporte e camada de negócio.
**Ação Recomendada:** Criar um método `LoginAsync` em [`UsuarioService`](src/backend/Services/UsuarioService.cs) que receba email e senha, realize a validação e retorne os dados do usuário.

---

**Problema:** Ausência de tratamento de concorrência em reservas
**Evidência:** O método [`ReservaService.CriarReserva()`](src/backend/Services/ReservaService.cs:31) verifica capacidade e cria a reserva sem usar transação ou lock.
**Impacto:** Em cenários de alta demanda, duas requisições simultâneas podem ultrapassar a capacidade do evento (condição de corrida).
**Ação Recomendada:** Envolver as operações de verificação e inserção em uma transação SQL com hints de lock (`WITH (UPDLOCK, HOLDLOCK)` ou `SERIALIZABLE`).
