using AutoMapper;
using Collini.GestioneInterventi.Dal;
using Collini.GestioneInterventi.Domain.Docs;

namespace Collini.GestioneInterventi.Application.Jobs.Services;

public class JobService
{
    private readonly IMapper mapper;
    private readonly IRepository<Job> jobRepository;
    private readonly IColliniDbContext dbContext;

    public JobService(
        IMapper mapper,
        IRepository<Job> jobRepository,
        IColliniDbContext dbContext)
    {
        this.mapper = mapper;
        this.jobRepository = jobRepository;
        this.dbContext = dbContext;
    }
    
    
}