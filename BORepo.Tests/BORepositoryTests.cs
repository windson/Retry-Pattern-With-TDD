using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BORepo.Models;

namespace BORepo.Tests
{
    [TestClass]
    public class BORepositoryTests
    {
        private Mock<IBOService> _service;
        [TestInitialize]
        public void setup()
        {
            _service=new Mock<IBOService>();
        }

        [TestCleanup]
        public void Teardown()
        {
            _service = null;
        }


        [TestMethod]
        public void Retry_Succeeds_First_Time()
        {
            int trailSuccessOnCnt = 1;
            Common_Trail_Set_Up(trailSuccessOnCnt);
        }
        [TestMethod]
        public void Retry_Succeeds_Second_Time()
        {
            int trailSuccessOnCnt = 2;
            Common_Trail_Set_Up(trailSuccessOnCnt);
        }
        [TestMethod]
        public void Retry_Succeeds_Third_Time()
        {
            int trailSuccessOnCnt = 3;
            Common_Trail_Set_Up(trailSuccessOnCnt);
        }

        private void Common_Trail_Set_Up(int trailSuccessOnCnt)
        {
            //Arrange
            int cnt = 0;
            _service.Setup(x => x.Save(It.IsAny<BO>()))
                .Callback(() =>
                {
                    cnt++;
                    if (cnt != trailSuccessOnCnt) //For first {trailSuccessOnCnt} trials we are throwing ConnectionException for {trailSuccessOnCnt+1}th trail we are letting it pass.
                    {
                        throw new ConnectionException();
                    }
                });

            BORepository repo = new BORepository(_service.Object);
            var boEntity = new BO();

            //Act
            repo.Save(boEntity);

            //Assert
            _service.Verify(x => x.Save(It.IsAny<BO>()), Times.Exactly(trailSuccessOnCnt));
            AssertEx.Throws<ConnectionException>(() => _service.Object.Save(It.IsAny<BO>()));
        }
    }
}
