# DeliveryApp API

API REST para gerenciamento de uma plataforma de delivery, desenvolvida em **C# com .NET**, seguindo princípios de arquitetura em camadas, separação de responsabilidades e aplicação de regras de negócio.

O sistema permite o gerenciamento de **clientes, estabelecimentos, cardápios, produtos e pedidos**, além de possuir fluxos de autenticação específicos para clientes e estabelecimentos.

## 🚀 Funcionalidades

### 👤 Usuários

* Gerenciamento da identidade de acesso dos usuários.
* Identificação do tipo de usuário.
* Vinculação entre identidade de autenticação e perfil de domínio.
* Validação de e-mail e senha.
* Controle de acesso conforme o tipo de usuário.

### 🛵 Clientes

* Cadastro de clientes.
* Autenticação própria para clientes.
* Emissão de token de acesso identificado como cliente.
* Consulta e edição dos próprios dados.
* Cadastro e seleção de endereços de entrega.
* Restrição de acesso aos dados pertencentes ao próprio cliente.

### 🏪 Estabelecimentos

* Cadastro de estabelecimentos parceiros.
* Autenticação própria para estabelecimentos.
* Emissão de token de acesso identificado como estabelecimento.
* Consulta e edição do estabelecimento vinculado.
* Ativação e desativação do estabelecimento.
* Cadastro de horários e áreas de atendimento.
* Controle de disponibilidade para recebimento de pedidos.

### 🍔 Cardápio e Produtos

* Cadastro de categorias.
* Cadastro e edição de produtos.
* Ativação e desativação de produtos.
* Definição de preço e descrição.
* Gerenciamento de complementos.
* Consulta do cardápio vigente.
* Restrição para que somente o estabelecimento responsável administre seu próprio cardápio.

### 📦 Pedidos

* Criação de pedidos.
* Adição, alteração e remoção de itens.
* Definição da quantidade dos produtos.
* Observações e complementos.
* Cálculo de subtotal.
* Cálculo de taxa de entrega.
* Cálculo do valor total.
* Consulta do histórico de pedidos do cliente.
* Aceite ou recusa pelo estabelecimento.
* Atualização do status do pedido.
* Cancelamento conforme as regras de negócio.
* Controle das transições de status.

### 🧾 Itens do Pedido

Cada item mantém:

* Produto.
* Quantidade.
* Preço unitário aplicado no momento do pedido.
* Observações.
* Complementos.

O preço aplicado ao pedido é preservado para evitar alterações no histórico caso o preço do produto seja posteriormente modificado.

---

## 🔐 Autenticação e autorização

A API utiliza autenticação baseada em **token**, permitindo diferenciar os acessos de clientes e estabelecimentos.

O usuário autenticado recebe um token contendo as informações necessárias para identificar seu perfil e controlar o acesso aos recursos da API.

### Cliente

O cliente pode:

* Consultar seus próprios dados.
* Alterar seus próprios dados.
* Gerenciar seus endereços.
* Criar pedidos.
* Consultar seu histórico de pedidos.

Um cliente não pode acessar ou manipular informações pertencentes a outro cliente.

### Estabelecimento

O estabelecimento pode:

* Administrar seus próprios dados.
* Ativar ou desativar seu estabelecimento.
* Administrar categorias e produtos vinculados.
* Consultar pedidos destinados ao próprio estabelecimento.
* Aceitar, recusar e atualizar pedidos.

Um estabelecimento não pode administrar dados pertencentes a outro estabelecimento.

---

## 🏗️ Arquitetura

O projeto utiliza uma arquitetura separada em responsabilidades, buscando facilitar manutenção, testes e evolução da aplicação.

Estrutura conceitual:

```text
DeliveryApp
│
├── API
│   ├── Controllers
│   ├── Middlewares
│   └── Configurações
│
├── Aplicacao
│   ├── Commands
│   ├── Queries
│   ├── Handlers
│   ├── Validators
│   └── DTOs
│
├── Dominio
│   ├── Entidades
│   ├── Regras de Negócio
│   ├── Interfaces
│   └── Value Objects
│
└── Infraestrutura
    ├── Persistência
    ├── Repositórios
    ├── Autenticação
    └── Configurações
```

A aplicação utiliza **Commands e Queries** para separar operações de escrita e leitura.

Os **Handlers** são responsáveis por receber esses Commands/Queries, executar o fluxo correspondente e coordenar as regras necessárias para realizar a operação.

---

## 🧩 Principais módulos

```text
Usuários
   │
   ├── Clientes
   │      └── Endereços
   │
   └── Estabelecimentos
          │
          ├── Categorias
          │
          └── Produtos
                 │
                 └── Pedidos
                        │
                        └── Itens do Pedido
```

---

## 🔄 Fluxo principal

O fluxo principal da aplicação ocorre da seguinte maneira:

```text
Cliente
   │
   ▼
Autenticação
   │
   ▼
Token de acesso
   │
   ▼
Consulta estabelecimentos disponíveis
   │
   ▼
Consulta cardápio
   │
   ▼
Seleciona produtos
   │
   ▼
Monta pedido
   │
   ▼
Confirma pedido
   │
   ▼
Estabelecimento recebe o pedido
   │
   ├── Aceita
   │
   └── Recusa
          │
          ▼
   Atualização do status
```

O pedido mantém o histórico dos valores e itens utilizados no momento da confirmação.

---

## 📊 Status dos pedidos

As alterações de status seguem uma sequência definida pelas regras de negócio da aplicação.

Exemplo conceitual:

```text
Criado
  │
  ▼
Confirmado
  │
  ▼
Aceito
  │
  ▼
Em preparação
  │
  ▼
Saiu para entrega
  │
  ▼
Concluído
```

Também existem fluxos específicos para situações como recusa e cancelamento.

As transições são controladas pela aplicação para impedir alterações inválidas.

---

## 🛠️ Tecnologias

* **C#**
* **.NET**
* **ASP.NET Core**
* **Entity Framework Core**
* **PostgreSQL**
* **MediatR**
* **FluentResults**
* **ASP.NET Core Identity**
* **JWT**
* **Dependency Injection**
* **Swagger / OpenAPI**
* **Testes automatizados**

---

## 🧪 Testes

O projeto possui testes automatizados para validar o comportamento da aplicação e suas regras de negócio.

Entre os cenários testados estão:

* Cadastro de clientes.
* Cadastro de estabelecimentos.
* Validações de dados.
* Autenticação.
* Autorização.
* Gerenciamento de produtos.
* Criação de pedidos.
* Alteração de quantidade dos itens.
* Cálculo de valores.
* Validação das transições de status.
* Regras de acesso entre clientes e estabelecimentos.

---

## ⚙️ Configuração

### Pré-requisitos

* .NET SDK
* PostgreSQL
* Git

Clone o projeto:

```bash
git clone <URL_DO_REPOSITORIO>
cd DeliveryApp
```

Configure as informações de conexão com o banco de dados e as demais configurações necessárias através das configurações da aplicação.

**Credenciais, chaves JWT e informações sensíveis não devem ser armazenadas diretamente no código-fonte.**

Para desenvolvimento local, utilize mecanismos como:

* User Secrets
* Variáveis de ambiente
* Arquivos de configuração não versionados

---

## 🗄️ Banco de dados

O projeto utiliza **Entity Framework Core** para persistência dos dados.

Após configurar a conexão com o PostgreSQL, execute as migrations:

```bash
dotnet ef database update
```

Caso necessário, uma nova migration pode ser criada com:

```bash
dotnet ef migrations add NomeDaMigration
```

---

## ▶️ Executando a API

Execute o projeto com:

```bash
dotnet run
```

Durante o desenvolvimento, a API pode ser acessada através do endereço configurado pelo ASP.NET Core.

A documentação dos endpoints pode ser acessada pelo **Swagger**, quando habilitado.

---

## 📚 Documentação da API

A API disponibiliza documentação através do **Swagger/OpenAPI**, permitindo visualizar e testar os endpoints disponíveis.

Os endpoints são organizados de acordo com seus respectivos módulos:

```text
/api/usuarios
/api/clientes
/api/estabelecimentos
/api/categorias
/api/produtos
/api/pedidos
```

Os caminhos exatos podem variar conforme a implementação atual dos controllers.

---

## 🔒 Regras importantes

A aplicação possui regras para garantir a integridade dos dados:

* E-mails devem ser válidos e únicos.
* Senhas devem possuir pelo menos 8 caracteres, incluindo dígito e caractere não alfanumérico.
* Clientes só podem acessar seus próprios dados.
* Estabelecimentos só podem administrar seus próprios dados.
* Produtos inativos não aparecem no cardápio disponível para compra.
* Estabelecimentos inativos não recebem novos pedidos.
* Pedidos devem possuir ao menos um item.
* A quantidade dos itens deve ser maior que zero.
* O pedido deve possuir cliente, estabelecimento, endereço e itens.
* O pedido só pode ser confirmado quando o estabelecimento estiver disponível.
* Alterações de status devem respeitar as transições permitidas.
* Pedidos confirmados preservam os valores e itens utilizados no momento da confirmação.

---

## 🎯 Objetivo do projeto

O **DeliveryApp** foi desenvolvido como projeto de estudo para aplicar conceitos de desenvolvimento backend com **C# e .NET**, incluindo:

* Desenvolvimento de APIs REST.
* Arquitetura de software.
* Injeção de dependência.
* CQRS utilizando Commands e Queries.
* MediatR.
* Entity Framework Core.
* Autenticação e autorização.
* JWT.
* Validação de regras de negócio.
* Persistência com PostgreSQL.
* Testes automatizados.
* Boas práticas de desenvolvimento.

---

## 👨‍💻 Autor

**Kauan Silva**


