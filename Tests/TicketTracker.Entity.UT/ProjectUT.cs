using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace TicketTracker.Entity.UT
{
    [TestFixture]
    public class ProjectUT
    {
        [Test]
        public void Should_Be_Count_3_When_Add_A_Ticket_Given_Ticket_Count_Is_2()
        {
            var ticketIds = new List<TicketId>(3)
            {
                new(Guid.NewGuid()),
                new TicketId(Guid.NewGuid())
            };
            var project = Project.Create("Project1", new ProjectManager(), ticketIds);

            project.AddTicket(new TicketId(Guid.NewGuid()));

            project.Tickets!.Count.ShouldBe(3);
        }

        [Test]
        public void Should_Be_Count_3_When_Add_A_ProjectManager_Given_ProjectManager_Count_Is_2()
        {
            var projectManager = new ProjectManager(new List<AccountId>(3)
            {
                new(Guid.NewGuid()),
                new(Guid.NewGuid())
            });
            var sut = Project.Create("Project1", projectManager);

            sut.AddProjectManager(new AccountId(Guid.NewGuid()));

            sut.ProjectManager.Accounts.Count.ShouldBe(3);
        }
    }
}