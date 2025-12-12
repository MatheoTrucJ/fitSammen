using System;
using System.Collections.Generic;
using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Model;
using Xunit;

namespace FitSammen_API.Tests
{
    public class ClassServiceTests
    {
        [Fact]
        public void GetUpcomingClasses_ReturnsClassesFromDal()
        {
            // Arrange
            var expectedClasses = new List<Class>
            {
                new Class(1, DateOnly.FromDateTime(DateTime.Today.AddDays(1)), new Employee(), "Morning yoga", new Room(), "Yoga", 20, 5, 60, new TimeOnly(8,0), ClassType.Yoga),
                new Class(2, DateOnly.FromDateTime(DateTime.Today.AddDays(2)), new Employee(), "Spin class", new Room(), "Spinning", 15, 3, 45, new TimeOnly(9,30), ClassType.Spinning)
            };

            var fakeClassAccess = new FakeClassAccess
            {
                ClassesToReturn = expectedClasses
            };

            var service = new ClassService(fakeClassAccess);

            // Act
            var result = service.GetUpcomingClasses();

            // Assert
            Assert.Same(expectedClasses, result); 
        }

        [Fact]
        public void GetUpcomingClasses_ReturnsEmpty_WhenDalReturnsEmpty()
        {
            // Arrange
            var fakeClassAccess = new FakeClassAccess
            {
                ClassesToReturn = new List<Class>()
            };

            var service = new ClassService(fakeClassAccess);

            // Act
            var result = service.GetUpcomingClasses();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetUpcomingClasses_PropagatesException()
        {
            var fakeClassAccess = new FakeClassAccess { Throw = true };
            var service = new ClassService(fakeClassAccess);
            Assert.Throws<InvalidOperationException>(() => service.GetUpcomingClasses());
        }

        private sealed class FakeClassAccess : IClassAccess
        {
            public IEnumerable<Class> ClassesToReturn { get; set; } = new List<Class>();
            public bool Throw { get; set; }

            public int CreateClass(Class cls)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Location> GetAllLocations()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Employee> GetEmployeesByLocationId(int LocationId)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Room> GetRoomsByLocationId(int LocationId)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Class> GetUpcomingClasses()
            {
                if (Throw) throw new InvalidOperationException("Failure");
                return ClassesToReturn;
            }
        }
    }
}
