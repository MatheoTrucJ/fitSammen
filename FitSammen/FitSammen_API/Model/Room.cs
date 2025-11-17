namespace FitSammen_API.Model
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Capacity { get; set; }
        public Location Location { get; set; }

        public Room(int roomId, string roomName, int capacity, Location location)
        {
            RoomId = roomId;
            RoomName = roomName;
            Capacity = capacity;
            Location = location;
        }

        public Room()
        {
        }
    }
}
