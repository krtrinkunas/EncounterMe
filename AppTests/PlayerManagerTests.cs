using EncounterMeApp.Models;
using EncounterMeApp.Views;
using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using EncounterMeApp;
using EncounterMeApp.Services;
using System.Collections.Generic;

namespace AppTests
{
    public class PlayerManagerTests
    {
        Mock<IPlayerService> playerService = new();

        private readonly PlayerManager _sut;

        public PlayerManagerTests()
        {
            _sut = new PlayerManager(playerService.Object);
        }

        [Fact]
        public async Task validateUser_GetsValidPlayer_ReturnsPlayer()
        {
            Player validPlayer = new Player { NickName = "TestNickName", Points = 0, Email = "email@email.com", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" };

            Player validatedPlayer = await _sut.validateUser(validPlayer);

            Assert.NotNull(validatedPlayer);
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task validateUser_GetsInvalidPlayer_ReturnsNull(Player expected, Player player)
        {
            Player validatedPlayer = await _sut.validateUser(player);

            Assert.Equal(expected, validatedPlayer);
        }
        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { null, new Player { NickName = "" } };
            yield return new object[] { null, new Player { Firstname = "" } };
            yield return new object[] { null, new Player { Lastname = "" } };
            yield return new object[] { null, new Player { Email = "" } };
            yield return new object[] { null, new Player { Password = "" } };
        }
    }
}
