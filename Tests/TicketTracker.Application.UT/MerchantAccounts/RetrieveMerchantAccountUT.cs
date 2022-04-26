using System.Linq;
using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.Queries;

namespace TicketTracker.Application.UT.MerchantAccounts
{
    [TestFixture]
    public class RetrieveMerchantAccountUT
    {
        [Test]
        public async Task
            Should_Get_1_MerchantAccount_When_Retrieve_MerchantAccounts_Given_There_Are_3_MerchantAccounts()
        {
            var merchantAccountId = new MerchantAccountId(GuidMaker.NewGuid());
            var retrieveTarget = GenMerchantAccount(merchantAccountId);
            var dataBase = new List<MerchantAccount>()
            {
                retrieveTarget,
                GenMerchantAccount(),
                GenMerchantAccount()
            };

            var merchantAccountRepository = GenMerchantAccountRepository(dataBase);

            var sut = new RetrieveMerchantAccount(merchantAccountRepository);
            var actualResult = await sut.Handle(new RetrieveMerchantAccountQuery() { MerchantAccountId = merchantAccountId.Id }, CancellationToken.None);

            actualResult.ShouldNotBeNull()
                .Id.ShouldBe(retrieveTarget.Id);
        }

        private static IMerchantAccountRepository GenMerchantAccountRepository(List<MerchantAccount> dataBase)
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            merchantAccountRepository.GetById(Arg.Any<MerchantAccountId>())
                .Returns(o => dataBase.First(db => db.Id == o.Arg<MerchantAccountId>()));
            return merchantAccountRepository;
        }

        private static MerchantAccount GenMerchantAccount(MerchantAccountId? merchantAccountId = null)
        {
            return MerchantAccount.Create(merchantAccountId ?? MerchantAccountId.Default,
                new AccountId(GuidMaker.NewGuid()), new List<WorkSpace>());
        }
    }
}