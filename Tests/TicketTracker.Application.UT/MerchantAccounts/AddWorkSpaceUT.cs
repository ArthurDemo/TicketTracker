using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.Commands;

namespace TicketTracker.Application.UT.MerchantAccounts
{
    [TestFixture]
    public class AddWorkSpaceUT
    {
        [Test]
        public async Task Should_Increase_A_WorkSpace_When_Add_A_WorkSpace_Given_WorkSpace_Count_Is_0()
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            var merchantAccount = new MerchantAccount();
            merchantAccountRepository.GetById(Arg.Any<MerchantAccountId>())
                .Returns(merchantAccount);

            var command = new AddWorkSpaceCommand()
            {
                MerchantAccountId = GuidMaker.NewGuid(),
                WorkSpace = new ValueTuple<string, IEnumerable<Guid>, uint>("WS", new[] { GuidMaker.NewGuid() }, 3)
            };
            var sut = new AddWorkSpace(merchantAccountRepository);

            var actualResult = await sut.Handle(command, CancellationToken.None);

            actualResult.IsSuccess.ShouldBeTrue();
            merchantAccount.WorkSpaces!.Count.ShouldBe(1);
        }
    }
}