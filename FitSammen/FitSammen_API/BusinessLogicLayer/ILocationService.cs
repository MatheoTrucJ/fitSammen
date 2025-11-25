namespace FitSammen_API.BusinessLogicLayer
{
    public interface ILocationService
    {
        IEnumerable<string> GetAllLocations();
        IEnumerable<string> GetRoomsByLocation(int locationId);
    }
}
