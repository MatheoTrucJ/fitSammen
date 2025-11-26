using System.ComponentModel.DataAnnotations;

namespace FitSammen_API.DTOs
{
    public class EmployeeMinimalDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "EmployeeId must be a positive integer.")]
        public int User_ID { get; set; }

        public EmployeeMinimalDTO() { }
        public EmployeeMinimalDTO(int User_ID)
        {
            this.User_ID = User_ID;
        }
    }
}
