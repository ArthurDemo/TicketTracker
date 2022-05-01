namespace TicketTracker.Application._Common.Specifications;

public class MerchantAccountSpec : Specification<MerchantAccount>
{
    public override Expression<Func<MerchantAccount, bool>> AsExpression()
    {
        return meAcct => meAcct.WorkSpaces != null && meAcct.WorkSpaces.Any();
    }
}