using Worker.Core.Models;
using Worker.Infrastructure.Data;

namespace Worker.Infrastructure.Repositories;

public interface IScheduledJobRepository : IRepository<ScheduledJob> { }

public class ScheduledJobRepository(WorkerDbContext context) : Repository<ScheduledJob>(context), IScheduledJobRepository { }