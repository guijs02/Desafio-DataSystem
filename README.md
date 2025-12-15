# TaskManagement - Instruções rápidas

Este README descreve passos mínimos para executar o projeto backend e o frontend localmente, partindo deste repositório.

Pré-requisitos
- Instalar o `.NET 10` (SDK). Verifique a instalação com:
  ```sh
  dotnet --version
  ```
  A saída deve começar com `10`.
- Instalar o Docker (inclui Docker Compose). Verifique com:
  ```sh
  docker --version
  docker compose version
  ```
- Instalar o Node.js (versão compatível com o frontend). Verifique com:
  ```sh
  node --version
  npm --version
  ```

- Para subir o banco da dados (SQL Server)
1. A partir da raiz do repositório (onde este `README.md` está), suba o container com:
   ```sh
   docker compose up -d
   ```
2. Para parar e remover os containers:
   ```sh
   docker compose down
   ```

Backend (.NET)
1. Entre na pasta do backend:
   ```sh
   cd TaskManagement.Api
   ```
2. Restaurar e executar a API (desenvolvimento):
   ```sh
   dotnet restore
   dotnet run
   ```
3. A API será exposta na porta configurada (ver `TaskManagement.Api/Properties/launchSettings.json` ou `appsettings.Development.json`).

Frontend (aplicação Vite/React dentro do projeto)
1. Entre na pasta do frontend:
   ```sh
   cd Front/my-tasks-app
   ```
2. Instalar dependências e iniciar o servidor de desenvolvimento:
   ```sh
   npm install
   npm run dev
   ```
3. O comando `npm run dev` mostrará a URL local (normalmente `http://localhost:5173` ou similar).

Observações
- Se o repositório atual estiver em uma máquina Windows e você usar PowerShell, os comandos acima funcionam; no Linux/macOS use o terminal correspondente.

Comandos úteis
- Verificar status do git:
  ```sh
  git status
  ```
- Para ver logs do Docker Compose:
  ```sh
  docker compose logs -f
  ```

Pronto — seguindo esses passos você terá o backend em `.NET 10`, os containers necessários ativos e o frontend rodando em modo de desenvolvimento.
