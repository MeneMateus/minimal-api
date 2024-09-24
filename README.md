# README do Projeto Gerenciador de Veículos

## Descrição do Projeto

Este projeto é um **Gerenciador de Veículos** desenvolvido em C#. A aplicação segue os princípios da **Clean Architecture** e da **Onion Architecture**, utilizando **DTOs (Data Transfer Objects)** para garantir a separação de preocupações e facilitar a manutenção. O sistema permite a gestão de veículos com autenticação e autorização baseadas em perfis de usuário: **ADM** (administrador) e **Usuário**.

## Tecnologias Utilizadas

- **C#**: Linguagem de programação principal.
- **.NET 6 ou superior**: Framework utilizado para desenvolvimento da API.
- **MSTest**: Framework de testes unitários e de integração.
- **Entity Framework Core**: Para persistência de dados.
- **JWT (JSON Web Tokens)**: Para autenticação.

## Estrutura do Projeto

### Camadas

- **Domain**: Contém as entidades do domínio e interfaces.
- **Application**: Contém a lógica de negócios, serviços e DTOs.
- **Infrastructure**: Implementações de acesso a dados e serviços externos.
- **Presentation**: Contém os controladores e a API.
  
## Funcionalidades

### Autenticação e Autorização

- **Perfis de Usuário**: Controle de acesso baseado em roles (ADM e Usuário).
- **JWT**: Utilização de tokens JWT para autenticação.

### Gerenciamento de Veículos

- **CRUD de Veículos**: Permite operações de criação, leitura, atualização e exclusão de veículos.
- **Listagem de Veículos**: Usuários podem visualizar todos os veículos cadastrados.

### DTOs

- **VehicleDTO e UserDTO**: Utilização de DTOs para transferir dados entre as camadas e reduzir o acoplamento. Isso ajuda a evitar a exposição de entidades do domínio diretamente na API.

### Testes Unitários e de Persistência

- Implementação de testes para garantir a lógica de negócios e a interação correta com o banco de dados.

## Configuração do Ambiente

1. **Pré-requisitos**:
   - .NET SDK 6 ou superior
   - Banco de dados (por exemplo, SQL Server ou SQLite)

2. **Clonando o repositório**:
   ```bash
   git clone <URL do repositório>
   cd <diretório do projeto>
   ```

3. **Restaurando pacotes**:
   ```bash
   dotnet restore
   ```

4. **Migrando o banco de dados**:
   ```bash
   dotnet ef database update
   ```

5. **Executando a aplicação**:
   ```bash
   dotnet run
   ```

## Execução de Testes

Para executar os testes unitários e de integração, utilize o seguinte comando:

```bash
dotnet test
```

## Conclusão

Este projeto ilustra a aplicação de **Clean Architecture** e **Onion Architecture** em C#, utilizando DTOs para promover uma arquitetura limpa e organizada. A separação de preocupações facilita a manutenção e a escalabilidade, enquanto os testes garantem a robustez do sistema.
