using NSubstitute.Extensions;
using System.Linq;
using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.UT.MerchantAccounts
{
    [TestFixture]
    public class RemoveWorkSpaceUT
    {
        [Test]
        public async Task Should_Remove_A_WorkSpace_WS1_When_Remove_WorkSpace_WS1_Given_There_Is_A_WorkSpace_WS1()
        {
            var merchantAccountId = new MerchantAccountId(GuidMaker.NewGuid());
            var merchantAccount = MerchantAccount.Create(merchantAccountId,
                new AccountId(GuidMaker.NewGuid()), new List<WorkSpace>());
            merchantAccount!.AddWorkSpace(WorkSpace.Create("WS1", 1));

            var dataBase = new List<MerchantAccount>() { merchantAccount };

            var merchantAccountRepository = GenMerchantAccountRepository(dataBase);

            var sut = new RemoveWorkSpace(merchantAccountRepository);
            var actualResult = await sut.Handle(new RemoveWorkSpaceCommand()
            {
                MerchantAccountId = merchantAccount.Id.Id,
                WorkSpaceName = "WS1"
            }, CancellationToken.None);

            actualResult.IsSuccess.ShouldBeTrue();
            dataBase.FirstOrDefault(o => o.Id == merchantAccount.Id).ShouldNotBeNull()
                .WorkSpaces!.FirstOrDefault(o => o.Name == "WS1").ShouldBeNull();
        }

        private static IMerchantAccountRepository GenMerchantAccountRepository(List<MerchantAccount> dataBase)
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();

            merchantAccountRepository.GetById(Arg.Any<MerchantAccountId>())
                .Returns(o => dataBase.First(db => db.Id == o.Arg<MerchantAccountId>()));

            merchantAccountRepository.Configure()
                .When(o => o.Update(Arg.Any<MerchantAccount>()))
                .Do(o =>
                {
                    var merchantAccount = o.Arg<MerchantAccount>();
                    var idx = dataBase.FindIndex(db => db.Id == merchantAccount.Id);
                    dataBase[idx] = merchantAccount;
                });
            return merchantAccountRepository;
        }
    }
}