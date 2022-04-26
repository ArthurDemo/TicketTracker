using TicketTracker.Entity;

namespace TicketTracker.Application.MerchantAccounts.DTOs;

public record MerchantAccountResult(MerchantAccountId Id, AccountId AccountId, List<WorkSpace> WorkSpaces);