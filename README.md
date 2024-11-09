# Projeto .NET Core com Docker e SQL Server

Este � um projeto .NET Core configurado para rodar em um ambiente Docker com SQL Server. Abaixo est�o as instru��es para rodar o projeto localmente utilizando Docker e Docker Compose.

## Pr�-requisitos

Antes de come�ar, voc� precisa ter as seguintes ferramentas instaladas no seu sistema:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Passos para Configura��o e Execu��o

### 1. Clonar o Reposit�rio

Primeiramente, clone o reposit�rio para sua m�quina local:


git clone https://github.com/theusccs111/TaskManager
cd TaskManager

Certifique-se de que o Docker e o Docker Compose est�o instalados corretamente. Para isso, abra o terminal e execute:

docker --version
docker-compose --version

Com o Docker configurado, execute o seguinte comando para criar e iniciar os containers:

docker-compose up --build

O projeto estar� dispon�vel no endere�o http://localhost:5000
