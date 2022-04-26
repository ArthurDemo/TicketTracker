using System.Linq;
using System.Linq.Expressions;
using TicketTracker.Application.Projects.DTOs;
using TicketTracker.Application.Projects.Queries;

namespace TicketTracker.Application.UT.Projects
{
    [TestFixture]
    public class FetchProjectUT
    {
        [Test]
        public async Task Should_Get_3_Projects_When_Fetch_Given_There_Are_3_Projects()
        {
            var merchantAccountId = new MerchantAccountId(GuidMaker.NewGuid());
            var projectIds = new[]
            {
                new ProjectId(GuidMaker.NewGuid()),
                new ProjectId(GuidMaker.NewGuid()),
                new ProjectId(GuidMaker.NewGuid())
            };

            var expectProjectResults = projectIds.Select((o, idx) =>
                new ProjectResult(o, $"PJ{idx}", new List<TicketId>(), new List<AccountId>())
            ).ToList();

            var merchantAccount = GenMerchantAccount(merchantAccountId, projectIds);

            var merchantAccountRepository = GenMerchantAccountRepository(merchantAccountId, merchantAccount);
            var projectRepository = GenProjectRepository(projectIds);

            var query = new FetchProjectQuery()
            {
                MerchantAccountId = merchantAccountId.Id,
                WorkSpaceName = "WS1",
                PageNo = 1,
                PageSize = 5
            };

            var sut = new FetchProject(merchantAccountRepository, projectRepository);

            var actualResult = await sut.Handle(query, CancellationToken.None);

            actualResult.TotalCount.ShouldBe(3);
            actualResult.Items.ShouldBeEquivalentTo(expectProjectResults);
        }

        private static MerchantAccount GenMerchantAccount(MerchantAccountId merchantAccountId, ProjectId[] projectIds)
        {
            var merchantAccount = MerchantAccount.Create(
                merchantAccountId,
                AccountId.Default,
                new List<WorkSpace>()
                {
                    WorkSpace.Create(
                        "WS1",
                        projectIds.ToList())
                });
            return merchantAccount;
        }

        private static IProjectRepository GenProjectRepository(ProjectId[] projectIds)
        {
            var projectRepository = Substitute.For<IProjectRepository>();
            projectRepository.Get(Arg.Any<Expression<Func<Project, bool>>>(), Arg.Any<int>(), Arg.Any<int>())
                .Returns(projectIds.Select((o, idx) =>
                        Project.Create(o, $"PJ{idx}", new ProjectManager(), new List<TicketId>())
                        )
                    );

            return projectRepository;
        }

        private static IMerchantAccountRepository GenMerchantAccountRepository(MerchantAccountId merchantAccountId,
            MerchantAccount merchantAccount)
        {
            var merchantAccountRepository = Substitute.For<IMerchantAccountRepository>();
            merchantAccountRepository.GetById(merchantAccountId)
                .Returns(merchantAccount);
            return merchantAccountRepository;
        }
    }
}