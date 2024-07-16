using Elevator.Library;
using Microsoft.AspNetCore.Mvc;

namespace Elevator.Web.Controllers
{
    public class ElevatorController : Controller
    {
        private static ElevatorControl _elevatorController = new ElevatorControl();
        public IActionResult Index()
        {
            return View(_elevatorController.GetElevatorStatus());
        }

        [HttpPost]
        public IActionResult RequestElevator(int floor, string direction)
        {
            //var dir = direction == "Up" ? Direction.Up : Direction.Down;
            //_elevatorController.RequestElevator(floor, dir);
            //return RedirectToAction("Index");

            var dir = direction == "Up" ? Direction.Up : Direction.Down;
            ThreadPool.QueueUserWorkItem(_ => _elevatorController.RequestElevator(floor, dir));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SelectFloor(int floor)
        {
            //_elevatorController.SelectFloor(floor);
            //return RedirectToAction("Index");
            ThreadPool.QueueUserWorkItem(_ => _elevatorController.SelectFloor(floor));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetStatus()
        {
            var status = _elevatorController.GetElevatorStatus();
            var obj = new
            {
                CurrentFloor = status.CurrentFloor,
                CurrentDirection = status.CurrentDirection.ToString(),
                DoorStatus = status.DoorStatus.ToString()
            };
            return Json(obj);
        }
    }
}
