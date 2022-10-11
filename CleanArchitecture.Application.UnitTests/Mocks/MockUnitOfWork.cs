﻿using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

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
        public static Mock<UnitOfWork> GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<StreamerDbContext>()
               .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{dbContextId}")
               .Options;

            var streamerDbContextFake = new StreamerDbContext(options);

            streamerDbContextFake.Database.EnsureDeleted();
            var mockUnitOfWork = new Mock<UnitOfWork>(streamerDbContextFake);


            return mockUnitOfWork;
        }
    }
}
