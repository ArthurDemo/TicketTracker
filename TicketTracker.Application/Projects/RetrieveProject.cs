using TicketTracker.Application.Projects.DTOs;
using TicketTracker.Application.Projects.Queries;

namespace TicketTracker.Application.Projects;

public class RetrieveProject : IRequestHandler<RetrieveProjectQuery, ProjectResult>
{
    private readonly IProjectRepository _projectRepository;

    public RetrieveProject(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public Task<ProjectResult> Handle(RetrieveProjectQuery request, CancellationToken cancellationToken)
    {
        var project = _projectRepository.GetById(new ProjectId(request.ProjectId));

        return Task.FromResult(new ProjectResult(project!.Id, project.Name, project.Tickets!,
            project.ProjectManager.Accounts));
    }
}