using TicketTracker.Application.MerchantAccounts;
using TicketTracker.Application.MerchantAccounts.Commands;
using TicketTracker.Entity.Exceptions;

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
                MerchantAccountId=GuidMaker.NewGuid(),
                WorkSpace=new ValueTuple<string, IEnumerable<Guid>, uint>("WS", new[] { GuidMaker.NewGuid() }, 3)
            };
            var sut = new AddWorkSpace(merchantAccountRepository);

            var actualResult = await sut.Handle(command, CancellationToken.None);

            actualResult.IsSuccess.ShouldBeTrue();
            merchantAccount.WorkSpaces!.Count.ShouldBe(1);
        }

        [Test]
        public async Task Should_Throw_An_Exception_When_Add_A_WorkSpace_Given_Specific_MerchantAccount_Does_Not_Exist()
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            var sut = new AddWorkSpace(merchantAccountRepository);

            Func<Task> action = () => sut.Handle(new AddWorkSpaceCommand(), CancellationToken.None);

            await action.ShouldThrowAsync<MerchantAccountCouldNotFoundException>();
        }
    }
}