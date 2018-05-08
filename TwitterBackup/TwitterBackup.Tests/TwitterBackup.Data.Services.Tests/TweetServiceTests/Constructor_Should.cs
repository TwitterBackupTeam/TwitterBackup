using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Tests.TwitterBackup.Data.Services.Tests.TweetServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_UnitOfWork_IsNull()
        {
            //Arrange
            var mapperMock = new Mock<IAutoMapper>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(mapperMock.Object, null));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Mapper_IsNull()
        {
            //Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweetService(null, unitOfWorkMock.Object));
        }
    }
}
