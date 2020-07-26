# ApmRubberBand
Simple APM server that saves events to Postgres, for use in development or tests.

## Description

ApmRubberBand emulates an APM server, but instead of saving events to ElasticSearch it uses a SQL databasase. Postgres and SqlServer are both compatible. 

ApmRubberBand will enable Postgres extension TimeScaleDb, if available, for improved time-series performance and escalability. In that case, future migrations are not guaranteed to work and may  require to drop the database before upgrading.

It has been tested with a couple of versions of apm-agent-dotnet. It should work for most scenarios, but it is not guaranteed. If you find any trouble, please get all info you can, such as dumped Http requests and open an issue. 

Data in database is stored mostly as json. Some fields have been extracted as columns for ease of use.

## Limitations

ApmRubberBand only provides basic core functionality, therefore features such as authentication are not available. Mock responses are provided for handshaking and identification http requests.

