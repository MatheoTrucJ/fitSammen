using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using FitSammen_API.BusinessLogicLayer;
using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.DTOs;
using FitSammen_API.Model;
using Xunit;

namespace FitsammenAPITest
{
    public class TestLocationService
    {
        [Fact]
        public void GetAllLocationsSuccess()
        {
            //Arrange
            FakeClassAccess fakeClassAccess = new FakeClassAccess();
            LocationService locationService = new LocationService(fakeClassAccess);

            //Act
            IEnumerable<Location> locations = locationService.GetAllLocations();

            //Assert
            Assert.NotNull(locations);
            Assert.Collection(locations,
                loc =>
                {
                    Assert.Equal(1, loc.LocationId);
                    Assert.Equal("Main St", loc.StreetName);
                    Assert.Equal(123, loc.HouseNumber);
                    Assert.Equal(1000, loc.Zipcode.ZipcodeNumber);
                    Assert.Equal("Aalborg", loc.Zipcode.City.CityName);
                    Assert.Equal("Danmark", loc.Zipcode.City.Country.CountryName);
                },
                loc =>
                {
                    Assert.Equal(2, loc.LocationId);
                    Assert.Equal("Second St", loc.StreetName);
                    Assert.Equal(456, loc.HouseNumber);
                    Assert.Equal(2000, loc.Zipcode.ZipcodeNumber);
                    Assert.Equal("Copenhagen", loc.Zipcode.City.CityName);
                    Assert.Equal("Danmark", loc.Zipcode.City.Country.CountryName);
                });
        }
        [Fact]
        public void GetRoomsByLocationIdSuccess()
        {
            //Arrange
            FakeClassAccess fakeClassAccess = new FakeClassAccess();
            LocationService locationService = new LocationService(fakeClassAccess);
            int locationId = 1;
            //Act
            IEnumerable<Room> rooms = locationService.GetRoomsByLocationId(locationId);
            //Assert
            Assert.NotNull(rooms);
            Assert.Collection(rooms,
                (Action<Room>)(room =>
                {
                    Assert.Equal(1, room.RoomId);
                    Assert.Equal("Room A", room.RoomName);
                    Assert.Equal(30, room.Capacity);
                    Assert.Equal(1, (int?)room.Location.LocationId);
                }),
                (Action<Room>)(room =>
                {
                    Assert.Equal(2, room.RoomId);
                    Assert.Equal("Room B", room.RoomName);
                    Assert.Equal(20, room.Capacity);
                    Assert.Equal(1, (int?)room.Location.LocationId);
                }));
        }
    }

    sealed class FakeClassAccess : IClassAccess
    {
        private Location l1 = new Location(1, "Main St", 123, 1000, "Aalborg", "Danmark");
        private Location l2 = new Location(2, "Second St", 456, 2000, "Copenhagen", "Danmark");
        private Room r1;
        private Room r2;
        private Room r3;
        private Room r4;

        public FakeClassAccess()
        {
            r1 = new Room(1, "Room A", 30, l1);
            r2 = new Room(2, "Room B", 20, l1);
            r3 = new Room(3, "Room C", 25, l2);
            r4 = new Room(4, "Room D", 15, l2);
        }

        public int CreateClass(Class cls)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return new List<Location>()
            {
                l1,
                l2
            };
        }

        public IEnumerable<Employee> GetEmployeesByLocationId(int LocationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Room> GetRoomsByLocationId(int LocationId)
        {
            if (LocationId == l1.LocationId)
            {
                return new List<Room>() { r1, r2 };
            }
            else if (LocationId == l2.LocationId)
            {
                return new List<Room>() { r3, r4 };
            }
            else
            {
                return new List<Room>();
            }
        }

        public IEnumerable<Class> GetUpcomingClasses()
        {
            throw new NotImplementedException();
        }
    }
}

