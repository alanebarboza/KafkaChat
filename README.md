<h1 align="center"> Kafka Chat </h1>

## Descrição do Projeto
<p align="center"> Uma aplicação em .Net core para um Chat em tempo real com Kafka. </p>
 <br>
 ![Preview](https://github.com/alanebarboza/KafkaChat/blob/main/Path/imagem.png)
 <br>
### Features

- [x] Kafka
- [x] Hosted Services.
- [x] DDD
- [x] SOLID
- [x] Entity Framework
- [x] InMemoryDB
 <br>
 
### *Configurações*
  Previamente a execução, é necessário a instalação do Kafka. Deixo abaixo o passo a passo da instalação e configuração do mesmo.

<br>  

- [ *Download* ]
  - Baixar o BINARY no site https://kafka.apache.org/downloads
  - Extrair em "C:\Kafka" 
  - Criar pasta "C:\kafka\data\zookeeper" e "C:\Kafka\data\kafka-logs"
<br>  

- [ *Configurações do Kafka/Zookeeper* ]
  - Editar os arquivos:
     - "C:\kafka\config\zookeeper.properties"
        - Alterar o caminho da Data: "dataDir=C:/kafka/data/zookeeper"   ( Barra pra frente / )
        - Alterar o "broker.id" para 1
     - "C:\Kafka\config\server.properties"
        - Alterar o caminho do LOG: "log.dirs=C:/Kafka/data/kafka-logs" ( Barra pra frente / )

  - Copiar os arquivos "C:\kafka\bin\windows\zookeeper-server-start.bat" e "C:\kafka\bin\windows\kafka-server-start.bat"
     - Colar eles na pasta "C:\kafka\bin"

<br>  

- [ *Comandos para inicializar a gerenciar o Kafka*]
   - Iniciar ZooKeeper: 
     - start C:\kafka\bin\windows\zookeeper-server-start.bat C:\kafka\config\zookeeper.properties > C:\kafka\bin\zookeeper-server-start.bat 

   - Iniciar o Kafka:
     - start C:\kafka\bin\windows\kafka-server-start.bat C:\kafka\config\server.properties > C:\kafka\bin\kafka-server-start.bat 

   - Criar Tópico:
     - C:\kafka\bin\windows\kafka-topics.bat --create --topic topicoteste --bootstrap-server localhost:9092 

   - Listar Tópicos:
     - C:\kafka\bin\windows\kafka-topics.bat --list --bootstrap-server localhost:9092 

   - Escutar o tópico (topicoteste):
     - start C:\kafka\bin\windows\kafka-console-consumer.bat --topic topicoteste --from-beginning --bootstrap-server localhost:9092

   - Escrever no tópico (topicoteste): (apos carregar, só digitar o texto e dar Enter)    
     - C:\kafka\bin\windows\kafka-console-producer.bat --topic topicoteste --bootstrap-server localhost:9092

<br>  
<br>  

### - Nota:
<p> A aplicação já possui funções para inicializar o Zookeeper e o Kafka, considerando que estejam instalados em C:\Kafka.</p>
<p> Para utilizar, ative no appsettings as opções "StartZookeeperServer" e "StartKafkaServer", recomenda-se ativar "ClearKafkaLogs" para limpar os Logs/arquivos temporarios.</p>
<p> Caso esteja desativado, é estritamente necessário inicializar o Zookeeper e Kafka antes de iniciar a aplicação. </p>
   
<br>  
<br>  

<p align="center"> LinkedIn - ( https://www.linkedin.com/in/alan-evandro-barboza/ ) </p>
