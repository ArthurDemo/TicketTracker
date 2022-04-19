using TicketTracker.Entity;

namespace TicketTracker.Application.MerchantAccounts.DTOs;

public record MerchantAccountResult(AccountId AccountId, List<WorkSpace> WorkSpaces);