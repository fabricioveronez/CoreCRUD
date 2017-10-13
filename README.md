# Projeto CoreCRUD

A proposta é montar um projeto base para aplicações em .NET Core + MongoDB + Docker 

## Como executar ?

Assumindo que já tenha o [Docker](https://www.docker.com) instalado na máquina, execute os procedimentos abaixo:

1. Abra o console na pasta raiz da aplicação

2. Execute o build e os testes unitários ```docker-compose -f .\docker-compose.ci.build.yml up```

3. Execute o o deploy, atualiza a imagem da aplicação e cria o banco MongoDB ```docker-compose up -d --build```

4. No seu browser acesse a url http://localhost:8181/swagger para acessar as apis


## Tecnologias utilizadas:

- [ASP.NET Core 2.0 (com .NET Core)](https://www.microsoft.com/net/core)
 - ASP.NET MVC Core 
 - ASP.NET WebApi Core

- [MongoDB](https://www.mongodb.com/)
- [xUnit](https://xunit.github.io/)
- [AutoMapper](http://automapper.org/)
- [FluentValidation](https://github.com/JeremySkinner/FluentValidation)
- [Swagger UI](https://swagger.io/swagger-ui/)
- [Docker Container](https://www.docker.com/)


