using CleanArchitecture.Application.Contracts.Persistence;

using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnityOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnityOfWork>();
            var mockVideoRepository = MockVideoRepository.GetVideoRepository();
            mockUnitOfWork.Setup(r => r.VideoRepository).Returns(mockVideoRepository.Object);

            return mockUnitOfWork;
        }
    }
}
