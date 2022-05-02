using FluentValidation;

using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.API.APIs.MerchantAccounts.Validators;

public class RegisterMerchantAccountValidator : Validator<RegisterMerchantAccountCmd>
{
    public RegisterMerchantAccountValidator()
    {
        RuleFor(r => r.AccountId)
            .NotEmpty().WithMessage("AccountId can't be empty")
            .Must(actId => actId != Guid.Empty).WithMessage("格式不正確");

        RuleFor(r => r.WorkSpaces)
            .NotNull().WithMessage("WorkSpace can't be Null")
            .NotEmpty().WithMessage("WorkSpace must have 1 element");
    }
}