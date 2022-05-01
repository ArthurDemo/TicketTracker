using FluentAssertions;

using MediatR;

using NSubstitute.Extensions;

namespace TicketTracker.Application.UT.Projects
{
    [TestFixture]
    public class AddProjectUT
    {
        [Test]
        public async Task Should_Have_A_New_Project_PJ1_When_Add_A_Project_PJ1_Given_There_Is_Not_Project()
        {
            var projectDataBase = new List<Project>();
            var projectRepository = GenProjectRepository(projectDataBase);

            var mediator = Substitute.For<IMediator>();
            var projectId = Guid.NewGuid();
            GuidMaker.InjectActualGuid(projectId);
            var merchantAccountId = Guid.Empty;

            var sut = new AddProject(projectRepository, mediator);
            var actualResult = await sut.Handle(new AddProjectCommand()
            {
                MerchantAccountId=merchantAccountId,
                ProjectManagerAccountIds=new List<Guid>() { projectId },
                ProjectName="PJ1",
                WorkSpaceName="WS1"
            }, CancellationToken.None);

            actualResult.IsSuccess.ShouldBeTrue();
            projectDataBase.Count.ShouldBeGreaterThanOrEqualTo(1);
            await mediator.ReceivedWithAnyArgs().Publish<AddedProject>(
                new AddedProject(merchantAccountId, "WS1", new ProjectId(projectId), GuidMaker.NewGuid()),
                CancellationToken.None);
        }

        [Test]
        public async Task Should_Throw_An_Exception_When_Add_A_Project_Given_Specific_MerchantAccount_Does_Not_Exist()
        {
            var projectRepository = Substitute.For<IProjectRepository>();
            var mediator = Substitute.For<IMediator>();
            var sut = new AddProject(projectRepository, mediator);

            Func<Task> action = () => sut.Handle(new AddProjectCommand(), CancellationToken.None);

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        private static IProjectRepository GenProjectRepository(List<Project> projectDataBase)
        {
            var projectRepository = Substitute.For<IProjectRepository>();

            projectRepository
                .Configure().When(o => o.Add(Arg.Any<Project>()))
                .Do(o => projectDataBase.Add(o.Arg<Project>()));

            return projectRepository;
        }
    }
}