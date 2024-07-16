namespace Elevator.Library
{
    public class ElevatorProcess
    {
        public int CurrentFloor { get; private set; }
        public Direction CurrentDirection { get; private set; }
        public DoorStatus DoorStatus { get; private set; }

        public ElevatorProcess()
        {
            CurrentFloor = 1;
            CurrentDirection = Direction.Idle;
            DoorStatus = DoorStatus.Closed;
        }

        public async Task MoveToFloorAsync(int floor)
        {
            while (CurrentFloor != floor)
            {
                if (CurrentFloor < floor)
                {
                    CurrentDirection = Direction.Up;
                    CurrentFloor++;
                }
                else if (CurrentFloor > floor)
                {
                    CurrentDirection = Direction.Down;
                    CurrentFloor--;
                }
                await Task.Delay(2000); // Simulate moving delay
            }
            CurrentDirection = Direction.Idle;
            DoorStatus = DoorStatus.Open;
        }

        public void CloseDoor()
        {
            DoorStatus = DoorStatus.Closed;
        }
    }
}
