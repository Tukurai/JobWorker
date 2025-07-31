using Worker.Core.Models;
using Worker.Infrastructure.Data;

namespace Worker.Infrastructure.Repositories;

public interface IQueueRepository : IRepository<Queue> { }

public class QueueRepository(WorkerDbContext context) : Repository<Queue>(context), IQueueRepository { }