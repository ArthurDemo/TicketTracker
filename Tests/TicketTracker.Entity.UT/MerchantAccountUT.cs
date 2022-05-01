using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;

namespace TicketTracker.Entity.UT
{
    [TestFixture]
    public class MerchantAccountUT
    {
        [Test]
        public void
            Should_Be_90258307_910A_4D41_8CC3_F0AEF75AAAF0_When_Change_AccountId_Given_AccountId_Is_34F20109_48DE_4843_8624_0A0AB94DAAB4()
        {
            var originalAccountId = new AccountId(Guid.Parse("34F20109-48DE-4843-8624-0A0AB94DAAB4"));
            var newAccountId = new AccountId(Guid.Parse("90258307-910A-4D41-8CC3-F0AEF75AAAF0"));
            var sut = MerchantAccount.Create(originalAccountId);

            sut!.ChangeAccount(newAccountId);

            sut.Account!.ToString().ShouldBe(newAccountId.ToString());
        }

        [Test]
        public void Should_Be_3_To_WorkSpace_Count_When_Add_A_WorkSpace_Successfully_Given_WorkSpaces_Count_Is_2()
        {
            var sut = MerchantAccount.Create(new AccountId(Guid.NewGuid()),
                new List<WorkSpace>(3)
                {
                    WorkSpace.Create("WS1",1),
                    WorkSpace.Create("WS2",1)
                });

            sut!.AddWorkSpace(WorkSpace.Create("NewWorkSpace", 3));

            sut.WorkSpaces!.Count.ShouldBe(3);
        }

        [Test]
        public void Should_Be_2_To_WorkSpace_Count_When_Remove_A_WorkSpace_Successfully_Given_WorkSpaces_Count_Is_3()
        {
            var removableWorkSpace = WorkSpace.Create("WS3", 1);
            var sut = MerchantAccount.Create(new AccountId(Guid.NewGuid()),
                new List<WorkSpace>(3)
                {
                    WorkSpace.Create("WS1",1),
                    WorkSpace.Create("WS2",1),
                    removableWorkSpace
                });

            sut!.RemoveWorkSpace(removableWorkSpace);

            sut.WorkSpaces!.Count.ShouldBe(2);
        }
    }
}