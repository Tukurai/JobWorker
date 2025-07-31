using Microsoft.EntityFrameworkCore;
using System;
using Worker.Core.Models;
using Worker.Infrastructure.Data;

namespace Worker.Infrastructure.Repositories;

public interface IJobRepository : IRepository<Job> { }

public class JobRepository(WorkerDbContext context) : Repository<Job>(context), IJobRepository { }