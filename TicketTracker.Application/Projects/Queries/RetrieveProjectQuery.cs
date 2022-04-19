using MediatR;
using TicketTracker.Application.Projects.DTOs;

namespace TicketTracker.Application.Projects.Queries;

public class RetrieveProjectQuery : IRequest<ProjectResult>
{
    public Guid ProjectId { get; set; }
}