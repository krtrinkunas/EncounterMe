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
            playerService.Verify(m => m.AddPlayer(It.IsAny<Player>()));
        }
        [Theory]
        [MemberData(nameof(TestData))]
        public async Task validateUser_GetsInvalidPlayer_ReturnsNull(Player expected, Player player)
        {
            Player validatedPlayer = await _sut.validateUser(player);

            Assert.Equal(expected, validatedPlayer);
            playerService.Verify(m => m.AddPlayer(It.IsAny<Player>()), Times.Never());
        }
        public static IEnumerable<object[]> TestData()
        {
            yield return new object[] { null, new Player { NickName = "", Points = 0, Email = "email@email.com", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" } };
            yield return new object[] { null, new Player { NickName = "TestNickName", Points = 0, Email = "email@email.com", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "", Lastname = "TestLatsName", Password = "password123" } };
            yield return new object[] { null, new Player { NickName = "TestNickName", Points = 0, Email = "email@email.com", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "", Password = "password123" } };
            yield return new object[] { null, new Player { NickName = "TestNickName", Points = 0, Email = "", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" } };
            yield return new object[] { null, new Player { NickName = "TestNickName", Points = 0, Email = "email@email.com", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "" } };
        }

        [Fact]
        public async Task validateUser_GetsValidPlayerWithInvalidEmail_ReturnsNull()
        {
            Player validPlayer = new Player { NickName = "TestNickName", Points = 0, Email = "email", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" };

            Player validatedPlayer = await _sut.validateUser(validPlayer);

            Assert.Null(validatedPlayer);
        }
    }
}
