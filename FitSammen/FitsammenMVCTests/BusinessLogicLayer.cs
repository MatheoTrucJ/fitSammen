using FitSammen_API.Model;
using FitSammenWebClient.BusinessLogicLayer;

namespace FitsammenMVCTests
{
    public class BusinessLogicLayer
    {
        internal class memberTest {
            public memberTest()
            {
            }
        }


        [Fact]
        public void SignUpMemberWhenClassIsFull_ReturnIsFull()
        {
            //Arrange
            Class fullClass = new Class();
            fullClass.Capacity = 2;
            Member m1 = new Member();
            Member m2 = new Member();
            Member testMember = new Member();
            fullClass.Participants = new List<MemberBooking>
            {
                new MemberBooking { Member = m1 },
                new MemberBooking { Member = m2 }
            };
            ClassLogic classLogic = new ClassLogic();

            //Act
            Boolean res = classLogic.signUpAMember(testMember, fullClass);

            //Assert
            Assert.False(res);
            Assert.Equal(2, fullClass.Participants.Count());
            Assert.DoesNotContain(fullClass.Participants, mb => mb.Member == testMember);
        }
    }
}