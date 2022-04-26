using System.Linq;
using System.Linq.Expressions;
using TicketTracker.Application._Common.Models;
using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.DTOs;
using TicketTracker.Application.MerchantAccounts.Queries;

namespace TicketTracker.Application.UT.MerchantAccounts
{
    [TestFixture]
    public class FetchMerchantAccountUT
    {
        [Test]
        public async Task Should_Get_A_List_Of_MerchantAccount_When_Fetch_MerchantAccount_Given_There_Are_5_MerchantAccounts()
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            var merchantAccounts = GenFu.GenFu.ListOf<MerchantAccount>(5);
            var expectedResult = new PaginatedList<MerchantAccountResult>(
                merchantAccounts.Select(o => new MerchantAccountResult(o.Id, o.Account, o.WorkSpaces!)).ToList(), 5, 1, 5);
            merchantAccountRepository.Get(Arg.Any<Expression<Func<MerchantAccount, bool>>>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(merchantAccounts);

            var sut = new FetchMerchantAccount(merchantAccountRepository);

            var actualResult = await sut.Handle(new FetchMerchantAccountQuery(1, 5), CancellationToken.None);

            actualResult.PageNumber.ShouldBe(expectedResult.PageNumber);
            actualResult.TotalCount.ShouldBe(expectedResult.TotalCount);
            actualResult.Items.ShouldBe(expectedResult.Items);
        }
    }
}