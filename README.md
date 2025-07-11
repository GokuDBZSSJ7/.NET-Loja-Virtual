# 🏬 Loja Virtual API (.NET + MySQL)

Esta é uma API RESTful para gerenciamento de produtos, pedidos, usuários e operações de loja virtual, construída com C#, ASP.NET Core, Entity Framework Core e MySQL.

O projeto foi desenvolvido com foco em escalabilidade, manutenibilidade e boas práticas modernas, utilizando a **Clean Architecture modular**, separando responsabilidades por camada (Domínio, Aplicação, Infraestrutura, Apresentação e Compartilhamento).

---

## 🧱 Arquitetura

O projeto está dividido em múltiplos projetos `.csproj`, seguindo a arquitetura limpa: \
LojaVirtual.sln \
├── Core/ → Entidades e interfaces de domínio \
├── Application/ → Casos de uso e serviços da aplicação \
├── Infrastructure/ → Acesso a dados e tecnologias externas \
├── Shared/ → Utilitários, exceções e contratos comuns \
├── WebApi/ → Interface HTTP, Controllers e Middlewares \
├── Tests/ → Testes unitários e de integração

---

## 🚀 Tecnologias utilizadas

- ✅ .NET 8
- ✅ ASP.NET Core Web API
- ✅ Entity Framework Core
- ✅ Pomelo MySQL Provider
- ✅ MySQL
- ✅ Swagger (Swashbuckle)
- ✅ Visual Studio Code ou Visual Studio
- ✅ Clean Architecture

---

## 📦 Funcionalidades

- ✅ Cadastro, listagem, edição e exclusão de produtos  
- ✅ Gerenciamento modular por camadas (com injeção de dependência)
- ✅ Separação de responsabilidades entre domínio, aplicação e infraestrutura
- ✅ Migrations automáticas com EF Core
- ✅ Documentação automática da API via Swagger
- ✅ Testes unitários com xUnit

---

## ⚙️ Requisitos

- .NET SDK 8
- MySQL Server instalado e rodando
- Git
- Editor de código Visual Studio Code

---

## ▶️ Como executar localmente

```bash
# 1. Clonar o repositório
git clone https://github.com/GokuDBZSSJ7/.NET-Loja-Virtual.git

# 2. Navegar até a pasta do projeto
cd LojaVirtual

# 3. Ajustar a string de conexão em WebApi/appsettings.json
# Exemplo:
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=loja_virtual;user=root;password=SUASENHA"
}

# 4. Aplicar as migrations e criar o banco de dados
dotnet ef database update --project Infrastructure --startup-project WebApi

# 5. Executar o projeto
dotnet run --project WebApi

# 6. Acessar a documentação Swagger
http://localhost:5101/swagger