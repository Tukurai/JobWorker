using Worker.Core.Models;
using Worker.Infrastructure.Data;

namespace Worker.Infrastructure.Repositories;

public interface IQueueJobRepository : IRepository<QueueJob> { }

public class QueueJobRepository(WorkerDbContext context) : Repository<QueueJob>(context), IQueueJobRepository { }