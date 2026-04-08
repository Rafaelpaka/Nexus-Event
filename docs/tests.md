TDD (Test-Driven Development) é uma forma de desenvolver software escrevendo os testes antes do código.
Exemplo: um campo de senha deve avisar se não tiver caracteres especiais ou se a senha for fraca.

Como funciona:

Instalar xUnit

dotnet add package xunit

Abrir a pasta de teste com a estrutura:

├── NexusEvent/        projeto principal
├── NexusEvent.Tests/  testes

Criar o teste primeiro (ele vai falhar).
No TDD, isso é esperado: o teste falha porque ainda não existe código implementando a regra, mostrando que você está cobrindo o comportamento correto.

Rodar os testes

dotnet test

Implementar o mínimo de código para passar o teste.

Refatorar o código.

Conferir se todos os testes passaram no terminal.

Comitar.

Foco:
Testar comportamentos importantes, como criar usuário, validar email, evitar duplicidade e garantir senha segura, construindo o sistema guiado pelos testes.

Convenções do xUnit:

[Fact] – teste simples, comportamento único.

[Theory] – teste com dados diferentes, usando [InlineData(...)] para variar os valores.
