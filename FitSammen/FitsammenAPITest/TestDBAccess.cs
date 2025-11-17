using FitSammen_API.DatabaseAccessLayer;

namespace FitsammenAPITest
{
    public class TestDBAccess
    {
        private readonly IClassAccess _classAccess = new ClassAccess("Server=localhost;Database=GeometryData;User Id=sa;Password=@12tf56so;TrustServerCertificate=True;");

        [Fact]
        public void GetAllClassesSuccessValidParam()
        {

        }
    }
}
