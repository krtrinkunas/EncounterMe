using EncounterMeApp.Models;
using EncounterMeApp.Views;
using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using EncounterMeApp;
using EncounterMeApp.Services;

namespace AppTests
{
    public class PlayerManagerTests
    {
        Mock<IPlayerService> playerService = new();

        [Fact]
        //playerService nustatyti, ka grazina metodas AddPlayer
        public async Task validateUser_GetsValidPlayer_ReturnsPlayer()
        {
            Player testPlayer = new Player { NickName = "TestNickName", Points = 0, Email = "email@email.com", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" };

            PlayerManager testPlayerValidation = new PlayerManager(playerService.Object);

            Player validatedPlayer = await testPlayerValidation.validateUser(testPlayer);

            Assert.NotNull(validatedPlayer);
        }
    }
}
