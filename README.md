#### Projeto prático do AceleraDev C# - Wiz

**Squad 10:**
Cayo Cesar;
Eric Daniel;
Marcelo Amorim;
Marcelo Martins


# Central de Erros

[![Build Status](https://dev.azure.com/csharpwizsquad10/Squad10CentralDeErros/_apis/build/status/Squad10CentralDeErros%20-%20CI?branchName=master)](https://dev.azure.com/csharpwizsquad10/Squad10CentralDeErros/_build/latest?definitionId=4&branchName=master)

## Objetivo
Em projetos modernos é cada vez mais comum o uso de arquiteturas baseadas em serviços ou microsserviços. Nestes ambientes complexos, erros podem surgir em diferentes camadas da aplicação (backend, frontend, mobile, desktop) e mesmo em serviços distintos. Desta forma, é muito importante que os desenvolvedores possam centralizar todos os registros de erros em um local, de onde podem monitorar e tomar decisões mais acertadas. Neste projeto vamos implementar um sistema para centralizar registros de erros de aplicações.

A arquitetura do projeto é formada por:

- [Execução do projeto](#execução-do-projeto)
- [Estrutura](#estrutura)
- [Dependências](#dependências)
- [Build e testes](#build-e-testes)


## Execução do projeto
Certifique-se de que a ferramenta EF já foi instalada.
```bash
dotnet tool install --global dotnet-ef
```
Abra o terminal na raiz do projeto e rode os seguintes comandos para executar as migrations:
```bash
dotnet restore
dotnet ef database update -c CentralDeErrosDbContext -p ./CentralDeErros.Core/CentralDeErros.Core.csproj -s ./CentralDeErros.Api/CentralDeErros.Api.csproj
dotnet ef database update -c ApplicationDbContext -p ./CentralDeErros.Core/CentralDeErros.Core.csproj -s ./CentralDeErros.Api/CentralDeErros.Api.csproj
```
Rodando a aplicação:
```bash
dotnet restore
dotnet build
dotnet run
```

## Estrutura

1. **CentralDeErros.API**:   
    ├─ Configuration  
    └─ Controllers
2. **CentralDeErros.Core**:  
    └─ Migrations
3. **CentralDeErros.Data**:  
    ├─ Maps  
    └─ Models
4. **CentralDeErros.Services**:  
    ├─ Base  
    └─ Interfaces
5. **CentralDeErros.Tests**:  
    ├─ ControllersTests  
    ├─ FakeData  
    ├─ ModelsTests  
    └─ ServicesTests
6. **CentralDeErros.Transport**:  
## Dependências
- Automapper
- Identity
- Efcore
- EntityFrameworkCore.InMemory
- Coverlet
- moq4
- xUnit

## Build e testes
```bash
dotnet build
dotnet test
```
## Acesso
| ![](https://i.imgur.com/QZaFAHo.png) |
|--------|
|[Swagger](https://squad10centraldeerros.azurewebsites.net/swagger)|
