using Api.Controllers;
using Api.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTests
{
    public class PlayerControllerTests
    {
        private readonly PlayerController _sut;
        private readonly Mock<IPlayerRepository> _playerRepoMock = new Mock<IPlayerRepository>();
        private readonly Mock<ILogger<PlayerController>> _loggerMock = new Mock<ILogger<PlayerController>>();
        public PlayerControllerTests()
        {
            _sut = new PlayerController(_playerRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetSingle_ShouldReturnPlayer_WhenPlayerExists()
        {
            //Arrange
            var playerId = 1;
            //Act
            var player = await _sut.GetSingle(playerId);
            //Assert
            Assert.Equal(playerId, player.)
        }
    }
}
