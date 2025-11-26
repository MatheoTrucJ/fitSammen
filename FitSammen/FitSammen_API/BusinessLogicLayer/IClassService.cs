using FitSammen_API.DTOs;
using FitSammen_API.Model;

namespace FitSammen_API.BusinessLogicLayer
{
    public interface IClassService
    {
        public IEnumerable<Model.Class> GetUpcomingClasses();
        public ClassCreateResponseDTO CreateClass(ClassCreateRequestDTO classCreateRequestDTO);
    }
}
