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

O projeto estará disponível no endereço http://localhost:5000
