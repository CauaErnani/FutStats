FutStats API

> **Status do Projeto:** Em Desenvolvimento (Work in Progress)

O **FutStats** é uma API RESTful desenvolvida em C# e .NET 10 para o gerenciamento de campeonatos esportivos. O sistema permite o cadastro de times, o registro de partidas e processa dinamicamente a tabela de classificação do campeonato aplicando as regras de negócio oficiais do futebol.

## Tecnologias e Arquitetura

O projeto foi estruturado seguindo as melhores práticas de desenvolvimento back-end e injeção de dependência:

* **Linguagem:** C# 12
* **Framework:** ASP.NET Core Web API (.NET 8 LTS)
* **ORM:** Entity Framework Core
* **Banco de Dados:** EF Core In-Memory Database (Estruturado para futura persistência em PostgreSQL/SQL Server)
* **Documentação:** Swagger (OpenAPI)

## Funcionalidades da API

O motor do FutStats foi projetado para simular o comportamento de um campeonato real, processando regras de negócio no back-end. As principais funcionalidades incluem:

* **Gestão de Times:** Endpoints para o cadastro, leitura e gerenciamento das equipes participantes.
* **Registro de Partidas:** Processamento de resultados de jogos, cruzando os IDs dos times mandantes e visitantes com seus respectivos placares.
* **Motor de Classificação Dinâmico:** Geração da tabela de classificação em tempo real utilizando consultas otimizadas em **LINQ**. A lógica da API calcula automaticamente a pontuação (vitórias e empates), o saldo de gols e aplica os critérios de desempate oficiais para a ordenação dos times.
* **Arquitetura Escalável:** O projeto utiliza o padrão de Injeção de Dependência e ORM (Entity Framework), preparado para ser facilmente migrado de um banco In-Memory para bancos relacionais robustos, bem como para a inclusão de novas entidades (como jogadores e artilharias) no futuro.
