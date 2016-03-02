# Ticket4S
Um ensaio de um projeto Web para um site ficcional de venda de Ingressos para eventos que não existem e nunca irão existir.


Esse projeto é uma demonstração de uma arquitetura **S.O.L.I.D.** aplicada ao .Net num sistema simples de venda de ingressos.
 
Visando melhorar meus conhecimentos em meios de pagamentos digitais, aperfeiçoando meus conhecimentos e permitindo demostrar meu código para quem possa interessar, deixando um sistema completo como portfólio na internet.
E até me possibilitou de usar Libs que tinha pouca experiência, como xUnit, Serilog e SpeckFlow.


### Usando ###
- C# 6
- Asp.Net 4.6
- Asp.Net MVC 5 
- Asp.Net WebApi 5 (REST)
- Asp.Net Identity 2 (Autenticação)
- EntityFramework 6 (ORM)
- EntityFramework 6 Migrations
- Serilog (Logging)
- xUnit (Test)
- SpeckFlow (Test)
- FluentAssertions (Test)

### Usando em Breve ###
- Hangfire ~~ou Akka~~
- SimpleInjector ~~ou Ninject~~
- RX?


---------

## Algumas Premissas ##
No momento o sistema considera que exista uma quantidade infinita de Ingressos disponíveis para um Evento. (Não existe um controle de Lotação, quantidades disponibilizadas ou controle de Race Conditions)

As compras com cartão de credito podem ser salvar de forma segura, mas nessa versão considero que o cartão salvo tem uma validade infinita. (O sistema nunca pede um novo cartão quando o antigo expira)


## TODO ##
- Camada de apresentação Web e de Serviços RESTiful
- Mais Testes
- ???
- PROFIT!!!
