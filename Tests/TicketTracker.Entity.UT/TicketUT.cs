using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TicketTracker.Entity.UT
{
    [TestFixture]
    public class TicketUT
    {
        [Test]
        public void Should_Be_NewTitle_When_Edit_Title_Given_Title_Is_Title()
        {
            var sut = Ticket.Create("Title", "Content", AccountId.Default);

            sut.EditTitle("NewTitle");

            sut.Title.ShouldBe("NewTitle");
        }

        [Test]
        public void Should_Be_NewContent_When_Edit_Content_Given_Content_Is_Content()
        {
            var sut = Ticket.Create("Title", "Content", AccountId.Default);

            sut.EditDescription("NewContent");

            sut.Description.ShouldBe("NewContent");
        }

        [Test]
        public void Should_Be_Count_Is_1_When_Add_A_New_Comment_Given_Comment_Is_Empty()
        {
            var sut = Ticket.Create("Title", "Content", AccountId.Default);

            sut.AddComment(Comment.Create("Content", AccountId.Default));

            sut.Comments!.Count.ShouldBe(1);
        }

        [Test]
        public void Should_Be_Empty_When_Remove_A_Comment_Given_Comment_Count_Is_1()
        {
            var comment = Comment.Create("Content", AccountId.Default);
            var sut = Ticket.Create("Title", "Content", AccountId.Default);
            sut.AddComment(comment);

            sut.RemoveComment(comment);

            sut.Comments!.Count.ShouldBe(0);
        }

        [Test]
        public void Should_Be_Count_4_When_Tweak_Label_To_4_Given_Label_Count_Is_6()
        {
            var sut = Ticket.Create("Title", "Content", AccountId.Default);
            sut.TweakLabel(new[]
            {
                new Label(Guid.NewGuid(), "1"),
                new Label(Guid.NewGuid(), "2"),
                new Label(Guid.NewGuid(), "3"),
                new Label(Guid.NewGuid(), "4"),
                new Label(Guid.NewGuid(), "5"),
                new Label(Guid.NewGuid(), "6"),
            });

            sut.TweakLabel(new[]
            {
                new Label(Guid.NewGuid(), "7"),
                new Label(Guid.NewGuid(), "8"),
                new Label(Guid.NewGuid(), "9"),
                new Label(Guid.NewGuid(), "10")
            });

            sut.TicketAttribute.Labels!.Count.ShouldBe(4);
            sut.TicketAttribute.Labels!.Select(o => o.displayName)
                .ToArray().ShouldBeEquivalentTo(new[] { "7", "8", "9", "10" });
        }

        [Test]
        public void Should_Be_DisplayName_Is_Nice_When_Change_TicketType_Given_TicketType_DisplayName_Is_Good()
        {
            var sut = Ticket.Create("Title", "Content", AccountId.Default);
            sut.ChangeTicketType(new TicketType(Guid.NewGuid(), "Good"));

            sut.ChangeTicketType(new TicketType(Guid.NewGuid(), "Nice"));

            sut.TicketAttribute.TicketType!.DisplayName.ShouldBe("Nice");
        }

        [Test]
        public void Should_Be_DisplayName_Is_Low_When_Change_Priority_Given_Priority_DisplayName_Is_High()
        {
            var sut = Ticket.Create("Title", "Content", AccountId.Default);
            sut.ChangePriority(new Priority(Guid.NewGuid(), "High"));

            sut.ChangePriority(new Priority(Guid.NewGuid(), "Low"));

            sut.TicketAttribute.Priority!.DisplayName.ShouldBe("Low");
        }

        [Test]
        public void Should_Be_7_When_Change_EstimationPoint_Given_EstimationPoint_Is_2()
        {
            var sut = Ticket.Create("Title", "Content", AccountId.Default);
            sut.ChangeEstimationPoint(2);

            sut.ChangeEstimationPoint(7);

            Assert.AreEqual(sut.TicketAttribute.EstimationPoint, 7);
        }

        [Test]
        public void Should_Be_20220411_When_Change_DueDate_Given_DueDate_Is_20220410()
        {
            var oldDate = DateTime.Parse("2022/04/10");
            var newDate = DateTime.Parse("2022/04/11");

            var sut = Ticket.Create("Title", "Content", AccountId.Default);
            sut.ChangeDueDate(oldDate);

            sut.ChangeDueDate(newDate);

            sut.TicketAttribute.DueDate.ShouldBe(newDate);
        }

        [Test]
        public void Should_Be_New_Account_When_Change_An_Assignee_Given_Assignee_Is_Empty()
        {
            var accountId = new AccountId(Guid.NewGuid());
            var sut = Ticket.Create("Title", "Content", AccountId.Default);

            sut.ChangeAssignees(new List<AccountId>());

            sut.ChangeAssignees(new[] { accountId });

            sut.TicketStakeholder.Assignees!.Length.ShouldBe(1);
            sut.TicketStakeholder.Assignees!.Contains(accountId);
        }
    }
}