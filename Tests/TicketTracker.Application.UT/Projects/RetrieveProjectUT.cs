using TicketTracker.Application.Projects.Queries;

namespace TicketTracker.Application.UT.Projects
{
    [TestFixture]
    public class RetrieveProjectUT
    {
        [Test]
        public async Task Should_Get_Project_PJ1_When_Retrieve_PJ1_Given_There_Is_A_Project_PJ1()
        {
            var projectRepository = Substitute.For<IProjectRepository>();
            var projectId = ProjectId.Default;
            projectRepository.GetById(Arg.Any<ProjectId>())
                .Returns(Project.Create(projectId, "PJ1", new ProjectManager(), new List<TicketId>()));

            var sut = new RetrieveProject(projectRepository);

            var actualResult = await sut.Handle(new RetrieveProjectQuery()
            {
                ProjectId = projectId.Id
            }, CancellationToken.None);

            actualResult.ProjectId.ShouldBe(projectId);
            actualResult.Name.ShouldBe("PJ1");
        }
    }
}