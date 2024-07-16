namespace Elevator.Library
{
    public class ElevatorControl
    {
        private ElevatorProcess _elevator;
        private Queue<ElevatorRequest> _requests;
        private bool _isProcessingRequests;

        public ElevatorControl()
        {
            _elevator = new ElevatorProcess();
            _requests = new Queue<ElevatorRequest>();
            _isProcessingRequests = false;
        }

        public void RequestElevator(int floor, Direction direction)
        {
            _requests.Enqueue(new ElevatorRequest(floor, direction));
            ProcessRequests();
        }

        public void SelectFloor(int floor)
        {
            _requests.Enqueue(new ElevatorRequest(floor, Direction.Idle));
            ProcessRequests();
        }

        private async void ProcessRequests()
        {
            if (_isProcessingRequests)
                return;

            _isProcessingRequests = true;

            while (_requests.Count > 0)
            {
                var request = _requests.Dequeue();
                if (request != null) 
                {
                    await _elevator.MoveToFloorAsync(request.Floor);
                    // Simulate door closing after some time
                    await Task.Delay(2000);
                    _elevator.CloseDoor();
                }
            }
            _isProcessingRequests = false;
        }

        public ElevatorProcess GetElevatorStatus()
        {
            return _elevator;
        }
    }
}