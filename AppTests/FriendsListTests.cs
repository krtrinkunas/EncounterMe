using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EncounterMeApp;
using EncounterMeApp.Models;
using EncounterMeApp.Services;
using Moq;

namespace AppTests
{
    public class FriendsListTests
    {
        private readonly InternetPlayerService _ipls;
        public int deletedPlayerId;

        List<Player> players = new List<Player>();

        public FriendsListTests()
        {
            _ipls = new InternetPlayerService();
        }

        private async Task SetupDataForDatabaseTests()
        {
            Player player1 = new Player { NickName = "Petras", Points = 0, Email = "email1@email.com", Id = 52, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" };
            Player player2 = new Player { NickName = "Antanas", Points = 0, Email = "email2@email.com", Id = 53, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" };
            Player player3 = new Player { NickName = "Rapolas", Points = 0, Email = "email3@email.com", Id = 54, LocationsOwned = 0, LocationsVisited = 0, ProfilePic = "https://cdn3.iconfinder.com/data/icons/games-11/24/_user-512.png", Type = 0, Firstname = "TestFirstName", Lastname = "TestLatsName", Password = "password123" };

            await _ipls.AddPlayer(player1);
            await _ipls.AddPlayer(player2);
            await _ipls.AddPlayer(player3);

            //maybe need to add buffer?
            await _ipls.DeletePlayer(player2);
            deletedPlayerId = player2.Id;

            players.Add(player1);
            players.Add(player2);
            players.Add(player3);
        }

        [Fact]
        public async Task GetPlayer_ReturnsCorrectPlayer()
        {
            await SetupDataForDatabaseTests();
            Player player1 = await _ipls.GetPlayer(players[0].Id);

            Assert.Equal(player1, players[0]);
        }

        [Fact]
        public async Task GetPlayers_ReturnsCorrectPlayers()
        {
            List<Player> searchedPlayers =(List<Player>) await _ipls.GetPlayers();


            Assert.Equal(searchedPlayers[0], players[0]);
        }

        [Fact]
        public async Task GetPlayers_ReturnsCorrectPlayerList()
        {
            List<Player> players1 = (List<Player>)await _ipls.GetPlayers();
            List<Player> players2 = new List<Player> { players[0], players[2] };

            Assert.Equal(players1, players2);
        }

        [Fact]
        public async Task GetPlayer_ReturnsNullIfPlayerIsDeleted()
        {
            Player player = await _ipls.GetPlayer(deletedPlayerId);

            Assert.Null(player);
        }

    }
}
