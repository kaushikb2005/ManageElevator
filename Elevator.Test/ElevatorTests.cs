using Elevator.Library;

namespace Elevator.Test
{
    [TestFixture]
    public class ElevatorTests
    {
        private ElevatorControl _elevatorController;

        [SetUp]
        public void Setup()
        {
            _elevatorController = new ElevatorControl();
        }

        [Test]
        public void ElevatorStartsAtFirstFloor()
        {
            var status = _elevatorController.GetElevatorStatus();
            Assert.That(status.CurrentFloor, Is.EqualTo(1));
            Assert.That(status.CurrentDirection, Is.EqualTo(Direction.Idle));
            Assert.That(status.DoorStatus, Is.EqualTo(DoorStatus.Closed));
        }

        [Test]
        public async Task RequestElevatorToHigherFloor()
        {
            _elevatorController.RequestElevator(3, Direction.Up);
            await Task.Delay(3000 + 2000); // Wait for the elevator to move and door to close
            var status = _elevatorController.GetElevatorStatus();
            Assert.That(status.CurrentFloor, Is.EqualTo(3));
            Assert.That(status.CurrentDirection, Is.EqualTo(Direction.Idle));
            Assert.That(status.DoorStatus, Is.EqualTo(DoorStatus.Closed));
        }

        [Test]
        public async Task RequestElevatorToLowerFloor()
        {
            _elevatorController.RequestElevator(5, Direction.Up);
            await Task.Delay(4000 + 2000); // Wait for the elevator to move and door to close
            _elevatorController.RequestElevator(2, Direction.Down);
            await Task.Delay(3000 + 2000); // Wait for the elevator to move and door to close
            var status = _elevatorController.GetElevatorStatus();
            Assert.That(status.CurrentFloor, Is.EqualTo(4));
            Assert.That(status.CurrentDirection, Is.EqualTo(Direction.Idle));
            Assert.That(status.DoorStatus, Is.EqualTo(DoorStatus.Closed));
        }

        [Test]
        public async Task SelectFloorInsideElevator()
        {
            _elevatorController.SelectFloor(4);
            await Task.Delay(3000 + 2000); // Wait for the elevator to move and door to close
            var status = _elevatorController.GetElevatorStatus();
            Assert.That(status.CurrentFloor, Is.EqualTo(4));
            Assert.That(status.CurrentDirection, Is.EqualTo(Direction.Idle));
            Assert.That(status.DoorStatus, Is.EqualTo(DoorStatus.Closed));
        }

        [Test]
        public async Task MultipleRequestsHandled()
        {
            _elevatorController.RequestElevator(2, Direction.Up);
            await Task.Delay(2000 + 2000); // Wait for the elevator to move and door to close
            _elevatorController.RequestElevator(5, Direction.Up);
            await Task.Delay(3000 + 2000); // Wait for the elevator to move and door to close
            var status = _elevatorController.GetElevatorStatus();
            Assert.That(status.CurrentFloor, Is.EqualTo(5));
            Assert.That(status.CurrentDirection, Is.EqualTo(Direction.Idle));
            Assert.That(status.DoorStatus, Is.EqualTo(DoorStatus.Closed));
        }
    }
}