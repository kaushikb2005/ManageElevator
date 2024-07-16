namespace Elevator.Library
{
    public class ElevatorRequest
    {
        public int Floor { get; }
        public Direction Direction { get; }

        public ElevatorRequest(int floor, Direction direction)
        {
            Floor = floor;
            Direction = direction;
        }
    }
}
