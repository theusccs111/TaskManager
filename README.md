# Projeto .NET Core com Docker e SQL Server

Este é um projeto .NET Core configurado para rodar em um ambiente Docker com SQL Server. Abaixo estão as instruções para rodar o projeto localmente utilizando Docker e Docker Compose.

## Pré-requisitos

Antes de começar, você precisa ter as seguintes ferramentas instaladas no seu sistema:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Passos para Configuração e Execução

### 1. Clonar o Repositório

Primeiramente, clone o repositório para sua máquina local:

git clone https://github.com/theusccs111/TaskManager
cd TaskManager

Certifique-se de que o Docker e o Docker Compose estão instalados corretamente. Para isso, abra o terminal e execute:

docker --version
docker-compose --version

Com o Docker configurado, execute o seguinte comando para criar e iniciar os containers:

docker-compose up --build

### 2. Perguntas ao PO

- O projeto terá autenticação e gestão de acesso ? Porque hoje não tem nenhum controle dessa parte como Token JWT por exemplo.
- Quais outros relatórios o ajudaria a analisar os usuários com suas tarefas, como algum índice de produtividade.
- E acho que seria melhor poder trocar a prioridade da tarefa, porque vai que o usuário cria ela errada, então tem que excluir essa e criar outra ?
- As tarefas poderia de alguma forma sincronizar com algum dispositivo de chat como Teams, Slack, para todos verem as alterações.


### 3. Melhorias

- Implementaria autenticação JWT e controle de sessão
- Implementaria a parte de Front-end em Angular com Antdesign para facilitar o uso
- Acredito que seria melhor uma classificação para a tarefa como Back-end, Front-end, Analise, Reunião para poder criar relatórios baseado nisso e assim dizer em qual tipo de tarefa foi gasto mais tempo
- Implementaria o cadastro de usuário