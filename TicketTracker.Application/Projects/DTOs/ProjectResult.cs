using TicketTracker.Entity;

namespace TicketTracker.Application.Projects.DTOs;

public record ProjectResult(ProjectId ProjectId, string Name, IEnumerable<TicketId> TicketIds, IEnumerable<AccountId> ProjectManagerAccountIds);