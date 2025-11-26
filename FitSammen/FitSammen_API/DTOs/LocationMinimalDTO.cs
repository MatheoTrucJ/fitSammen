using Microsoft.Identity.Client;

namespace FitSammen_API.DTOs
{
    public class LocationMinimalDTO
    {
        public int LocationId { get; set; }

        public LocationMinimalDTO() { }
        public LocationMinimalDTO(int locationId)
        {
            LocationId = locationId;
        }
    }
}
