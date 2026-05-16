

```markdown
# Ecossistema de Gestão (ERP + E-commerce + Logística)

Este repositório centraliza o desenvolvimento do ecossistema modular de gestão, composto por módulos de ERP (Catálogo, Compras, Inventário), E-commerce e Logística. O projeto foi construído utilizando **.NET** e **PostgreSQL**.

---

## 🛠️ Pré-requisitos para Desenvolvimento

Antes de rodar o projeto, você precisa instalar os seguintes componentes na sua máquina:

### 1. Ambiente .NET
*   **SDK do .NET 8.0** (ou superior)
    *   Necessário para compilar e rodar as aplicações.
    *   [Download .NET SDK](https://dotnet.microsoft.com/download)

### 2. Banco de Dados (PostgreSQL)
O projeto utiliza o PostgreSQL como banco de dados principal. Você pode rodá-lo de duas formas:
*   **Opção A (Recomendada): Docker**
    *   Instale o [Docker Desktop](https://www.docker.com/products/docker-desktop/).
    *   Com o Docker instalado, você pode subir o banco rodando:
        ```bash
        docker run --name pg-ecossistema -e POSTGRES_PASSWORD=sua_senha_aqui -p 5432:5432 -d postgres:latest
        

```

* **Opção B: Instalação Nativa**
* Instale o PostgreSQL (Versão 13 ou superior).
* [Download PostgreSQL](https://www.postgresql.org/download/)



### 3. Ferramentas de Linha de Comando (CLI)

Para gerenciar as Migrations do Entity Framework Core, é obrigatório ter a ferramenta de CLI do EF instalada globalmente.

Instale rodando o comando abaixo no seu terminal:

```bash
dotnet tool install --global dotnet-ef

```

*(Caso já tenha instalado e precise atualizar: `dotnet tool update --global dotnet-ef`)*

### 4. Interface para o Banco (Sugerido)

Para visualizar as tabelas, índices e dados gerados pelo EF Core:

* [pgAdmin 4](https://www.pgadmin.org/) ou [DBeaver](https://dbeaver.io/) (Excelente para gerenciar o Postgres).

---

## 🚀 Como Executar o Projeto Localmente

### 1. Clonar o Repositório

```bash
git clone [https://github.com/seu-usuario/seu-repositorio.git](https://github.com/seu-usuario/seu-repositorio.git)
cd seu-repositorio

```

### 2. Configurar a String de Conexão

Abra o arquivo `appsettings.Development.json` no projeto de entrada (ex: `ERP.API` ou `WebApp`) e ajuste as credenciais do PostgreSQL na chave `ConnectionStrings:DefaultConnection`.

### 3. Aplicar as Migrations (Criar o Banco de Dados)

Navegue até a pasta do projeto que contém o `DbContext` (geralmente `ERP.Infrastructure`) ou execute na raiz apontando os projetos:

```bash
dotnet ef database update --project Sources/DotNet/ERP/ERP.Infrastructure/ --startup-project Sources/DotNet/ERP/ERP.API/

```

### 4. Rodar a Aplicação

```bash
dotnet run --project Sources/DotNet/ERP/ERP.API/

```

---

## 📐 Padrão de Commits

Este projeto segue estritamente o padrão **Conventional Commits**. Antes de abrir um Pull Request, certifique-se de que suas mensagens de commit seguem a estrutura descrita na nossa **Wiki**.

Exemplo: `feat(catalogo): adicionar entidade produto #21990`

```

---

### 💡 Dica extra para o seu fluxo:
Se você optar por usar o Docker para subir o PostgreSQL, vale a pena criar um arquivo chamado `docker-compose.yml` na raiz do projeto futuramente. Assim, o comando para o novo desenvolvedor iniciar o banco vira apenas um simples `docker-compose up -d`.

O que achou dessa estrutura para o seu README? Falta algum detalhe específico da máquina de vocês?

```
