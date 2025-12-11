using FitSammen_API.DatabaseAccessLayer;
using FitSammen_API.Exceptions;
using FitSammen_API.BusinessLogicLayer;
using System;
using Xunit;
using FitSammen_API.Model;

namespace FitsammenAPITest
{
    public class TestWaitingListService
    {
        [Fact]
        public void AddMemberToWaitingListSuccess()
        {
            WaitingListService service = new WaitingListService(new MemberAccessMock
            {
                WaitingListEntryPosition = 1,
                ThrowDataAccessException = false,
                AlreadySignedUpClass = false,
                AlreadySignedUpWaitingList = false
            });

            WaitingListResult result = service.AddMemberToWaitingList(1, 10);

            Assert.Equal(WaitingListStatus.Success, result.Status);
            Assert.Equal(1, result.WaitingListPosition);
        }

        [Fact]
        public void AddMemberToWaitingListAlreadySignedUpToClass()
        {
            WaitingListService service = new WaitingListService(new MemberAccessMock
            {
                AlreadySignedUpClass = true,
                AlreadySignedUpWaitingList = false,
            });

            WaitingListResult result = service.AddMemberToWaitingList(1, 10);

            Assert.Equal(WaitingListStatus.AlreadySignedUpMB, result.Status);
            Assert.Null(result.WaitingListPosition);
        }
        [Fact]
        public void AddMemberToWaitingListAlreadySignedUpToWaitingList()
        {
            WaitingListService service = new WaitingListService(new MemberAccessMock
            {
                AlreadySignedUpClass = false,
                AlreadySignedUpWaitingList = true,
                WaitingListEntryPosition = 5
            });

            WaitingListResult result = service.AddMemberToWaitingList(1, 10);

            Assert.Equal(WaitingListStatus.AlreadySignedUpWL, result.Status);
            Assert.Equal(5, result.WaitingListPosition);
        }

        [Fact]
        public void AddMemberToWaitingListDataAccessException()
        {
            WaitingListService service = new WaitingListService(new MemberAccessMock
            {
                ThrowDataAccessException = true,
            });

            WaitingListResult result = service.AddMemberToWaitingList(1, 10);

            Assert.Equal(WaitingListStatus.Error, result.Status);
            Assert.Null(result.WaitingListPosition);
        }

        private sealed class MemberAccessMock : IMemberAccess
        {
            public int WaitingListEntryPosition { get; set; }
            public bool ThrowDataAccessException { get; set; }
            public bool ThrowGenericException { get; set; }
            public bool AlreadySignedUpClass { get; set; }
            public bool AlreadySignedUpWaitingList { get; set; }

            public int CreateMemberBooking(int memberUserNumber, int classId)
            {
                throw new NotImplementedException();
            }

            public int CreateWaitingListEntry(int memberUserNumber, int classId)
            {
                if (ThrowGenericException)
                {
                    throw new Exception("Unexpected DAL error");
                }

                if (ThrowDataAccessException)
                {
                    throw new DataAccessException("Could not create member booking - class may be full");
                }

                return WaitingListEntryPosition;
            }

            public User FindUserByEmailAndPassword(string email, byte[] password)
            {
                throw new NotImplementedException();
            }

            public int GetMemberCountFromClassId(int classId)
            {
                throw new NotImplementedException();
            }

            public byte[] GetSaltByEmail(string email)
            {
                throw new NotImplementedException();
            }

            public int IsMemberOnWaitingList(int memberId, int classId)
            {
                if (AlreadySignedUpWaitingList)
                {
                    return WaitingListEntryPosition;
                }
                return 0;
            }

            public bool IsMemberSignedUp(int memberUserNumber, int classID) => AlreadySignedUpClass;
        }
    }
}
