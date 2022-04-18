﻿using NUnit.Framework;
using Shouldly;

namespace TicketTracker.Entity.UT
{
    [TestFixture]
    public class AccountUT
    {
        [Test]
        public void Should_Be_BatCdotD_When_Change_Email_Given_Email_Is_AatBdotC()
        {
            var newEmail = "b@c.d";
            var sut = Account.Create("a@b.c", "1234");

            sut.ChangeEmail(newEmail);

            sut.Email.ShouldBe(newEmail);
        }

        [Test]
        public void Should_Be_5678_When_Change_Password_Give_Password_Is_1234()
        {
            var sut = Account.Create("a@b.c", "1234");

            sut.ChangePassword("5678");

            sut.Password.ShouldBe("5678");
        }
    }
}