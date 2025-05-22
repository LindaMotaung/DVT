using DVT.Elevator.Application.Interfaces;
using DVT.Elevator.Domain;

namespace DVT.Elevator.Application.Services
{
    /// <summary>
    /// Provides a set of services related to elevators
    /// </summary>
    public class ElevatorService : IElevatorService
    {
        private readonly IElevatorManager _elevatorManager;

        public ElevatorService(IElevatorManager elevatorManager)
        {
            _elevatorManager = elevatorManager;
        }

        public void AddPeopleToElevator(int elevatorId, int numPeople)
        {
            if (!IsValidElevatorId(elevatorId))
            {
                Console.WriteLine("Invalid elevator ID. Please enter a positive integer.");
                return;
            }

            if (!IsValidNumberOfPeople(numPeople))
            {
                Console.WriteLine("Number of people must be a non-negative integer.");
                return;
            }

            var elevator = _elevatorManager.GetElevators().FirstOrDefault(e => e.Id == elevatorId);
            if (elevator != null)
            {
                elevator.AddPeople(numPeople);
            }
            else
            {
                Console.WriteLine("Elevator not found.");
            }
        }

        public Status? GetElevatorStatus(int elevatorId)
        {
            if (!IsValidElevatorId(elevatorId))
            {
                Console.WriteLine("Invalid elevator ID. Please enter a positive integer.");
                return null;
            }

            var elevator = _elevatorManager.GetElevators().FirstOrDefault(e => e.Id == elevatorId);
            return elevator?.CurrentStatus;
        }

        public void MoveElevatorToFloor(int elevatorId, int floor)
        {
            if (!IsValidElevatorId(elevatorId))
            {
                Console.WriteLine("Invalid elevator ID. Please enter a positive integer.");
                return;
            }

            var elevator = _elevatorManager.GetElevators().FirstOrDefault(e => e.Id == elevatorId);
            if (elevator != null)
            {
                elevator.MoveToFloor(floor);
            }
            else
            {
                Console.WriteLine("Elevator not found.");
            }
        }

        public int RequestElevator(int floor)
        {
            var nearestElevator = _elevatorManager.GetNearestAvailableElevator(floor);
            if (nearestElevator != null)
            {
                nearestElevator.MoveToFloor(floor);
                return nearestElevator.Id;
            }
            else
            {
                Console.WriteLine("No elevators are available.");
                return -1;
            }
        }

        /// <summary>
        /// Method uses in multiple places to avoid code duplication. DRY
        /// </summary>
        /// <param name="elevatorId"></param>
        /// <returns></returns>
        private static bool IsValidElevatorId(int elevatorId)
        {
            return elevatorId > 0;
        }

        private static bool IsValidNumberOfPeople(int numPeople)
        {
            return numPeople >= 0;
        }
    }
}

