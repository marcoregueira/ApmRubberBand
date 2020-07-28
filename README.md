# ApmRubberBand

Simple replacement for Elasticsearch APM server that saves events to Postgres, for use in development, integration tests, performance tests or just monitoring aplications that don't require the power of a full Elastic stack.

## Description

ApmRubberBand emulates an APM server, but instead of saving events to ElasticSearch it uses a SQL databasase. Postgres and SqlServer are both compatible. 

ApmRubberBand will enable Postgres extension TimeScaleDb, if available, for improved time-series performance and escalability. In that case, future migrations are not guaranteed to work and may  require to drop the database before upgrading.

It has been tested with a couple of versions of apm-agent-dotnet. It should work for most scenarios, but it is not guaranteed. If you find any trouble, please get all info you can, such as dumped Http requests and open an issue. 

Data in database is stored mostly as json. Some fields have been extracted as columns for ease of use.

![alt tag](https://raw.githubusercontent.com/marcoregueira/ApmRubberBand/master/doc/images/transactionlog.png)

## Limitations

ApmRubberBand only provides basic core functionality, therefore features such as authentication are not available. Mock responses are provided for handshaking and identification http requests.

## Try it out

This is a sample docker-compose file using Postgres/TimescaleDb for storage, RubberBand as APM server and one of the examples provided with dotnet-apm-agent: SampleAspNetCoreApp serving as monitorized application. Once you have it running, you can connect to Postgres/TimescaleDb to see the transactions, errors and server metrics.

```
version: '3.4'

networks:
  network01:
    driver: bridge
      
services:

  timescale:
    image: timescale/timescaledb:latest-pg10
    container_name: timescale
    restart: unless-stopped
    volumes:
      - dbdata:/var/lib/postgresql/data
    networks:
      - network01 
    ports:
      - "5432:5432" 
    environment:
      - POSTGRES_USER=apm_root
      - POSTGRES_PASSWORD=apm_password
      - POSTGRES_DB

  rubberband:
    depends_on:
      - timescale
    image: h2hsoftware/apmrubberband
    container_name: rubberband
    restart: unless-stopped
    networks:
      - network01 
    ports:
      - "8200:80" 
    environment:
      - ConnectionStrings:Postgres=Host=timescale;Username=apm_root;Password=apm_password;Database=apm_rubberband
      - DbEngine=postgres
      - ApmOptions:AutoCreateCentralConfiguration=true
      - ApmOptions:DefaultCentralConfigurationMaxAge=300

  sampleaspnetcoreapp:
    depends_on:
      - rubberband
    image: h2hsoftware/elasticsearch_sampleaspnetcoreapp
    container_name: sampleaspnetcoreapp
    restart: unless-stopped
    networks:
      - network01 
    ports:
      - "80:80" 
    environment:
      - ElasticApm:ServerUrls=http://rubberband:80
      
volumes:
  dbdata:
```
