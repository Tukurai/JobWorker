# Prompt Design
You are helping me build a C# Scheduler and Queue solution. The solution is called JobWorker.

# Job Worker Service
The job worker service is a collection of microservices that are responsible for processing data and performing tasks in the background. It is designed to be scalable, resilient, and efficient.

# Architecture Overview
The worker service architecture consists of the following components:
1. **Worker.API** Responsible for interacting with the worker service and providing an API for external clients.
2. **Worker.Core** Contains the core business logic and domain models of the worker service.
3. **Worker.Infrastructure** Responsible for data access and persistence, including database interactions.
4. **Worker.Queue** Manages the queue for job processing, ensuring that jobs are executed efficiently. Possibly distributed across multiple instances.
5. **Worker.Scheduler** Responsible for executing scheduled jobs and managing the timing of job execution.

## Worker.API
The Worker.API is the entry point for external clients to interact with the job worker services. It provides REST-ful endpoints for submitting jobs, checking job status, and retrieving results. The API is meant to be a one of instance due to the underlying database(SqlLite) not being able to handle access from several sources at once.

### Files
- **Controllers**: Contains the API controllers that handle incoming requests and route them to the appropriate services.
- **Models**: Contains the data transfer objects (DTOs) used for communication between the API and the clients.
- **Helpers**: Contains utility classes and methods that assist in various tasks, such as validation and error handling.

## Worker.Core
The Worker.Core contains the core business logic of the job worker service. It defines the domain models, interfaces, and services that encapsulate the business rules and operations. This layer is responsible for processing jobs, managing job states, and coordinating with other components.

### Files
- **Models**: Contains the domain models that represent the jobs, job states, and other entities in the system.
- **Services**: Contains the business logic and services that handle job processing, state management, and coordination between different components.
- **Interfaces**: Contains the interfaces that define the contracts for the services and repositories used in the core layer.

## Worker.Infrastructure
The Worker.Infrastructure is responsible for data access and persistence. It interacts with the database to store job information, job states, and results. This layer abstracts the underlying data storage mechanism, allowing for flexibility in choosing different databases or storage solutions in the future.

### Files
- **Repositories**: Contains the data access repositories that interact with the database to perform CRUD operations on job entities.
- **Data**: Contains the database context and configuration for the data access layer.
- **Migrations**: Contains the database migrations that define the schema changes for the database.

## Worker.Queue
The Worker.Queue manages the job queue for processing. It ensures that jobs are executed in the correct order and handles job retries in case of failures. The queue can be distributed across multiple instances to improve scalability and performance. It is responsible for processing the jobs, it enqueues based on the jobs it retrieves through the Worker.API.

### Files
- **Services**: Contains the services that manage the job queue, including enqueueing, dequeueing, and retrying jobs.

## Worker.Scheduler
The Worker.Scheduler is responsible for executing scheduled jobs and managing the timing of job execution. It ensures that jobs are executed at the right time and can handle recurring jobs. The scheduler can be configured to run at specific intervals or based on specific triggers.

### Files
- **Services**: Contains the services that manage the scheduling of jobs, including scheduling recurring jobs and executing scheduled jobs.