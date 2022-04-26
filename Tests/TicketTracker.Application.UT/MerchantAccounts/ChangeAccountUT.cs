using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.UT.MerchantAccounts
{
    [TestFixture]
    public class ChangeAccountUT
    {
        [Test]
        public async Task Should_Be_New_Account_Id_When_Change_Account_Given_An_Existing_Account()
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            var newAccountId = GuidMaker.NewGuid();
            var merchantAccount = MerchantAccount.Create(new AccountId(GuidMaker.NewGuid()), new List<WorkSpace>());
            merchantAccountRepository.GetById(Arg.Any<MerchantAccountId>())
                .Returns(merchantAccount);

            var command = new ChangeAccountCommand()
            {
                MerchantAccountId = GuidMaker.NewGuid(),
                NewAccountId = newAccountId
            };

            var sut = new ChangeAccount(merchantAccountRepository);

            var actualResult = await sut.Handle(command, CancellationToken.None);

            actualResult.IsSuccess.ShouldBeTrue();
            merchantAccount!.Account.ShouldBe(new AccountId(newAccountId));
        }
    }
}