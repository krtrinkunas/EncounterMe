using System;
using Api.Controllers;
using Api.Repositories;
using System.Linq;
using Xunit;
using EncounterMeApp.Models;
using Moq;

namespace AppTests.DatabaseIntegrationTests
{
    public class ControllerTests : IClassFixture<DatabaseIntegrationTestsFixture>
    {
        public ControllerTests(DatabaseIntegrationTestsFixture fixture)
            => Fixture = fixture;

        public DatabaseIntegrationTestsFixture Fixture
        {
            get;
        }

        [Fact]
        public async void TestBlocks()
        {
            int idToBeDeleted;
            using var context = Fixture.CreateContext();

            context.Database.BeginTransaction();
            var log = LoggerMocker.GetLogger<BlockController>();
            var repository = new BlockRepository(context);
            var controller = new BlockController(repository, log.Object);
            await controller.Post(new Block { BlockedByID = 1234, UserBlockedID = 9999 });
            await controller.Post(new Block { BlockedByID = 1235, UserBlockedID = 9998 });
            await controller.Post(new Block { BlockedByID = 1236, UserBlockedID = 9997 });

            idToBeDeleted = context.Blocks.Single(bl => bl.BlockedByID == 1236).Id;
            await controller.Delete(idToBeDeleted);

            context.ChangeTracker.Clear();
            

            var newblock = context.Blocks.Single(bl => bl.BlockedByID == 1234);
            var getblock = controller.GetSingle(newblock.Id).Result.Value;
            var allblocks = controller.GetBlocks().Result;

            Assert.Equal(9999, newblock.UserBlockedID);
            Assert.Equal(newblock.Id, getblock.Id);
            Assert.Equal(newblock.BlockedByID, getblock.BlockedByID);
            Assert.Equal(newblock.UserBlockedID, getblock.UserBlockedID);
            Assert.Collection(allblocks,
                b => Assert.Equal(9999, b.UserBlockedID),
                b => Assert.Equal(9998, b.UserBlockedID));
            
            Assert.Null(controller.GetSingle(idToBeDeleted).Result.Value);
            Assert.NotNull(controller.GetSingle(getblock.Id).Result.Value);
        }

        [Fact]
        public async void TestFriendRequests()
        {
            int idToBeDeleted;
            using var context = Fixture.CreateContext();

            context.Database.BeginTransaction();
            var log = LoggerMocker.GetLogger<FriendRequestController>();
            var repository = new FriendRequestRepository(context);
            var controller = new FriendRequestController(repository, log.Object);
            await controller.Post(new FriendRequest { SenderID = 1234, ReceiverID = 9999, Status = 1 });
            await controller.Post(new FriendRequest { SenderID = 1235, ReceiverID = 9998, Status = 2 });
            await controller.Post(new FriendRequest { SenderID = 1236, ReceiverID = 9997, Status = 3 });

            idToBeDeleted = context.FriendRequests.Single(bl => bl.SenderID == 1236).Id;
            await controller.Delete(idToBeDeleted);

            context.ChangeTracker.Clear();


            var newFriendRequest = context.FriendRequests.Single(bl => bl.SenderID == 1234);
            var getFriendRequest = controller.GetSingle(newFriendRequest.Id).Result.Value;
            var allFriendRequests = controller.GetFriendRequests().Result;

            Assert.Equal(9999, newFriendRequest.ReceiverID);
            Assert.Equal(newFriendRequest.Id, getFriendRequest.Id);
            Assert.Equal(newFriendRequest.SenderID, getFriendRequest.SenderID);
            Assert.Equal(newFriendRequest.ReceiverID, getFriendRequest.ReceiverID);
            Assert.Collection(allFriendRequests,
                b => Assert.Equal(9999, b.ReceiverID),
                b => Assert.Equal(9998, b.ReceiverID));

            Assert.Null(controller.GetSingle(idToBeDeleted).Result.Value);
            Assert.NotNull(controller.GetSingle(getFriendRequest.Id).Result.Value);

        }

        [Fact]
        public async void TestFriends()
        {
            int idToBeDeleted;
            using var context = Fixture.CreateContext();

            context.Database.BeginTransaction();
            var log = LoggerMocker.GetLogger<FriendController>();
            var repository = new FriendRepository(context);
            var controller = new FriendController(repository, log.Object);
            await controller.Post(new Friend { Friend1ID = 1234, Friend2ID = 9999 });
            await controller.Post(new Friend { Friend1ID = 1235, Friend2ID = 9998 });
            await controller.Post(new Friend { Friend1ID = 1236, Friend2ID = 9997 });

            idToBeDeleted = context.Friends.Single(bl => bl.Friend1ID == 1236).Id;
            await controller.Delete(idToBeDeleted);

            context.ChangeTracker.Clear();


            var newFriend = context.Friends.Single(bl => bl.Friend1ID == 1234);
            var getFriend = controller.GetSingle(newFriend.Id).Result.Value;
            var allFriends = controller.GetFriends().Result;

            Assert.Equal(9999, newFriend.Friend2ID);
            Assert.Equal(newFriend.Id, getFriend.Id);
            Assert.Equal(newFriend.Friend1ID, getFriend.Friend1ID);
            Assert.Equal(newFriend.Friend2ID, getFriend.Friend2ID);
            Assert.Collection(allFriends,
                b => Assert.Equal(9999, b.Friend2ID),
                b => Assert.Equal(9998, b.Friend2ID));

            Assert.Null(controller.GetSingle(idToBeDeleted).Result.Value);
            Assert.NotNull(controller.GetSingle(getFriend.Id).Result.Value);
        }
    }
}
