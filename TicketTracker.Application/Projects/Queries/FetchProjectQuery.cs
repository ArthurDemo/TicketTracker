﻿using TicketTracker.Application.Projects.DTOs;

namespace TicketTracker.Application.Projects.Queries;

public class FetchProjectQuery : IRequest<PaginatedList<ProjectResult>>
{
    public Guid MerchantAccountId { get; set; }
    public string WorkSpaceName { get; set; } = null!;
    public int PageNo { get; set; }
    public int PageSize { get; set; }
}