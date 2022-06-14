## Run project without build
### Root of project
    docker-compose up

## Structure 
```src
  -src
  --ApiGateway -> Http Gateway for external API purposes
  --BuildingBlocks -> Common projects
  ---EventBus -> Abstractions for event bus implementation
  ---EventBus.Tests -> Tests for abstractions
  ---EventBusRabbitMQ -> Implementation for RabbitMQ
  ---EventBusServiceBus -> Implementation for Azure service bus
  ---IntegrationEventLogEF -> Implementation for integration events(persistance in sql database)
  ---WebHost.Customization -> .NET Core Web host extension
  --Services -> DDD Projects
  ---Persons -> Persons Service
  ----Persons.API -> Presentation level(Controllers) and Application Layer
  ----Persons.Domain -> Persons service aggregates models
  ----Persons.Infrastructure -> Infrastructure and Persistence Layers
  ---PR -> People Relation (PR) Service
  ----PR.API -> Presentation lever(Controllers) and Application Layer
  ----PR.Domain -> People Relation aggregates models
  ----PR.Infrastructure -> Infrastructure and Persistence Layers 
```
## Miro board with flows diagrams
https://miro.com/app/board/uXjVOBNRNPs=/