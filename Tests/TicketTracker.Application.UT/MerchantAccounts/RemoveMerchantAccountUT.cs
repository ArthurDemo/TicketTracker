using NSubstitute.Extensions;
using System.Linq;
using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.UT.MerchantAccounts
{
    [TestFixture]
    public class RemoveMerchantAccountUT
    {
        [Test]
        public async Task
            Should_Remove_Account_Peter_When_Remove_A_MerchantAccount_Peter_Given_There_Is_A_MerchantAccount_Peter()
        {
            var account = Account.Create("peter@mail.com", "1234");
            var merchantAccountId = new MerchantAccountId(GuidMaker.NewGuid());
            var merchantAccount = MerchantAccount.Create(merchantAccountId, account.Id, new List<WorkSpace>());

            var dataBase = new List<MerchantAccount>() { merchantAccount };
            var merchantAccountRepository = GenMerchantAccountRepository(dataBase);

            var sut = new RemoveMerchantAccount(merchantAccountRepository);
            var actualResult = await sut.Handle(new RemoveMerchantAccountCommand()
            {
                MerchantAccountId = merchantAccountId.Id
            }, CancellationToken.None);

            actualResult.IsSuccess.ShouldBe(true);
            dataBase.FirstOrDefault(o => o!.Account == account.Id).ShouldBeNull();
        }

        private static IMerchantAccountRepository GenMerchantAccountRepository(List<MerchantAccount> dataBase)
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            merchantAccountRepository.GetById(Arg.Any<MerchantAccountId>())
                .Returns(o => dataBase.First(db => db.Id == o.Arg<MerchantAccountId>()));

            merchantAccountRepository.Configure()
                .When(o => o.DeleteById(Arg.Any<MerchantAccountId>()))
                .Do(o =>
                {
                    var idx = dataBase.FindIndex(db => db.Id == o.Arg<MerchantAccountId>());
                    dataBase.RemoveAt(idx);
                });

            return merchantAccountRepository;
        }
    }
}