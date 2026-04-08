# Histórias de Usuário – Sistema NexusEvent

## História 1 – Cadastro de Usuário

*Como* usuário do sistema,
*Quero* cadastrar o meu usuário utilizando CPF, nome e email,
*Para* permitir que eu possa realizar reservas de ingressos para eventos.

---

## História 2 – Cadastro de Usuário Administrador

*Como* administrador do sistema,
*Quero* cadastrar o meu usuário utilizando CPF, nome e email e demais usuarios,
*Para* permitir que eu possa coordenar a operações de compra e venda no sistema.

---

## História 3 – Cadastro de Usuário Administrador de Eventos

*Como* administrador dos eventos,
*Quero* desempenhar funções relacionadas a cadastro e coordenação de eventos,
*Para* permitir que eu possa coordenar a operações de compra e venda no sistema.

---

## História 4 – Cadastro de Evento

*Como* administrador do sistema ou Administrador de Eventos do sistema,
*Quero* cadastrar novos eventos com nome, capacidade total, data e preço padrão,
*Para* disponibilizar eventos para venda de ingressos na plataforma.

---

## História 5 – Cadastro de Cupom

*Como* administrador do sistema ou Administrador de Eventos,
*Quero* cadastrar cupons de desconto com porcentagem e valor mínimo de aplicação,
*Para* permitir que clientes recebam descontos válidos nas compras de ingressos.

---

## História 6 – Compra de Ingresso (Reserva)

*Como* usuário da plataforma,
*Quero* comprar um ingresso para um evento utilizando meu CPF,
*Para* garantir minha participação no evento escolhido.

---

## História 7 – Consulta de Reservas

*Como* usuário da plataforma,
*Quero* consultar minhas reservas utilizando meu CPF,
*Para* visualizar os eventos para os quais já comprei ingressos.

---

# Critérios de Aceitação (BDD)

### História: Compra de Ingresso

*Dado que* o usuário está cadastrado no sistema
*E* o evento existe e ainda possui ingressos disponíveis
*Quando* o usuário realizar uma requisição de compra de ingresso
*Então* o sistema deve registrar a reserva vinculando o CPF do usuário ao evento.

---

*Dado que* o usuário já possui duas reservas para o mesmo evento
*Quando* ele tentar realizar uma nova compra
*Então* o sistema deve bloquear a operação retornando erro *400 Bad Request*.

---

*Dado que* a capacidade máxima do evento já foi atingida
*Quando* um usuário tentar comprar um ingresso
*Então* o sistema deve impedir a venda e retornar erro *400 Bad Request*.

---

*Dado que* um cupom foi informado na compra
*Quando* o preço do evento for maior ou igual ao valor mínimo exigido pelo cupom
*Então* o sistema deve aplicar o desconto correspondente no valor final.
