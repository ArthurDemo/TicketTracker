using TicketTracker.Application.Projects.DTOs;
using TicketTracker.Application.Projects.Queries;

namespace TicketTracker.Application.Projects;

public class FetchProject : IRequestHandler<FetchProjectQuery, PaginatedList<ProjectResult>>
{
    private readonly IMerchantAccountRepository _merchantAccountRepository;
    private readonly IProjectRepository _projectRepository;

    public FetchProject(IMerchantAccountRepository merchantAccountRepository, IProjectRepository projectRepository)
    {
        _merchantAccountRepository = merchantAccountRepository;
        _projectRepository = projectRepository;
    }

    public Task<PaginatedList<ProjectResult>> Handle(FetchProjectQuery query, CancellationToken cancellationToken)
    {
        var merchantAccount = _merchantAccountRepository.GetById(new MerchantAccountId(query.MerchantAccountId));

        MerchantAccountCouldNotFoundException.ThrowIfNull(merchantAccount);

        if (merchantAccount!.WorkSpaces is null ||
            merchantAccount.WorkSpaces.Any(o => o.Name == query.WorkSpaceName) == false ||
            merchantAccount.WorkSpaces!.SelectMany(o => o.Projects!).Any() == false)
            throw new ProjectCouldNotFoundException(nameof(merchantAccount.WorkSpaces));

        var projectIds = merchantAccount.WorkSpaces.First(o => o.Name == query.WorkSpaceName).Projects;
        var projects = _projectRepository.Get(o => projectIds!.Contains(o.Id), query.PageNo, query.PageSize)!
            .Select(o => new ProjectResult(o.Id, o.Name, o.Tickets!, o.ProjectManager.Accounts))
            .ToList();

        return Task.FromResult(new PaginatedList<ProjectResult>(projects, projects.Count, query.PageNo,
            query.PageSize));
    }
}