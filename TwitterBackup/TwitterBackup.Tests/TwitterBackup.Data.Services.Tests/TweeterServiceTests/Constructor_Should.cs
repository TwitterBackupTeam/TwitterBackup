using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Tests.TwitterBackup.Data.Services.Tests.TweeterServiceTests
{
    [TestClass]
    public class Constructor_Should
    {
        [TestMethod]
        public void Throw_ArgumentNullException_When_UnitOfWork_IsNull()
        {
            //Arrange
            var twitterApiServiceMock = new Mock<ITwitterAPIService>();
            var mapperMock = new Mock<IAutoMapper>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweeterService(twitterApiServiceMock.Object, mapperMock.Object, null));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_Mapper_IsNull()
        {
            //Arrange
            var twitterApiServiceMock = new Mock<ITwitterAPIService>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweeterService(twitterApiServiceMock.Object, null, unitOfWorkMock.Object));
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TwitterApi_IsNull()
        {
            //Arrange
            var mapperMock = new Mock<IAutoMapper>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            //Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TweeterService(null, mapperMock.Object, unitOfWorkMock.Object));
        }
    }
}
