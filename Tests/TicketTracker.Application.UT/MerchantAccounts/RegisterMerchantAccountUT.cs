using NSubstitute.Extensions;

using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.UT.MerchantAccounts
{
    [TestFixture]
    public class RegisterMerchantAccountUT
    {
        [Test]
        public async Task Should_A_New_MerchantAccount_When_Register_A_MerchantAccount_Given_There_Are_Not_MerchantAccount()
        {
            var dataBase = new List<MerchantAccount>();
            var merchantAccountRepository = GenMerchantAccountRepository(dataBase);

            var sut = new RegisterMerchantAccount(merchantAccountRepository);

            var actualResult = await sut.Handle(new RegisterMerchantAccountCmd(), CancellationToken.None);

            actualResult.IsSuccess.ShouldBeTrue();
            dataBase.Count.ShouldBe(1);
        }

        private static IMerchantAccountRepository GenMerchantAccountRepository(List<MerchantAccount> dataBase)
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            merchantAccountRepository.Configure()
                .When(o => o.Add(Arg.Any<MerchantAccount>(), Arg.Any<MerchantAccountId>()))
                .Do(o => dataBase.Add(o.Arg<MerchantAccount>()));
            return merchantAccountRepository;
        }
    }
}