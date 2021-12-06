using EncounterMeApp.Models;
using EncounterMeApp.Views;
using System;
using Xunit;
using Moq;
using System.Threading.Tasks;

namespace AppTests
{
    public class RegistrationPageTests
    {
        [Fact]
        public async Task validateUser_GetsInvalidValues_ReturnsnullAsync()
        {
            Player testPlayer = new Player { NickName = "" };

            var testRegistrationPage = new RegistrationPage();

            Player validatedPlayer = await testRegistrationPage.validateUser(testPlayer);

            Assert.Null(testPlayer);
        }
    }
}
