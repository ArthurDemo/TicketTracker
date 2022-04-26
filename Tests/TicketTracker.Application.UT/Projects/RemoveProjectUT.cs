using MediatR;
using NSubstitute.Extensions;

namespace TicketTracker.Application.UT.Projects
{
    [TestFixture]
    public class RemoveProjectUT
    {
        [Test]
        public async Task Should_Remove_Project_PJ1_When_Remove_Project_PJ1_Given_There_Is_A_Project_PJ1()
        {
            var projectId = new ProjectId(GuidMaker.NewGuid());
            var dataBase = new List<Project>() { Project.Create(projectId, "PJ1", new ProjectManager(), new List<TicketId>()) };
            var projectRepository = GenProjectRepository(dataBase);
            var mediator = Substitute.For<IMediator>();

            var sut = new RemoveProject(projectRepository, mediator);

            var merchantAccountId = GuidMaker.NewGuid();
            var actualResult = await sut.Handle(new RemoveProjectCommand()
            {
                MerchantAccountId = merchantAccountId,
                WorkSpaceName = "WS1",
                ProjectId = projectId.Id
            }, CancellationToken.None);

            actualResult.IsSuccess.ShouldBeTrue();
            dataBase.Count.ShouldBe(0);
            await mediator.ReceivedWithAnyArgs()
                .Publish<RemovedProject>(
                new RemovedProject(merchantAccountId, "WS1", projectId, GuidMaker.NewGuid())
                    , CancellationToken.None);
        }

        private static IProjectRepository GenProjectRepository(List<Project> dataBase)
        {
            var projectRepository = Substitute.For<IProjectRepository>();
            projectRepository.Configure()
                .When(o => o.DeleteById(Arg.Any<ProjectId>()))
                .Do(o => dataBase.RemoveAt(dataBase.FindIndex(db => db.Id == o.Arg<ProjectId>())));
            return projectRepository;
        }
    }
}