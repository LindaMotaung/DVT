using DVT.Elevator.Domain;

namespace DVT.Elevator.Application.Interfaces
{
    /// <summary>
    /// Provides methods for interacting with elevators
    /// </summary>
    public interface IElevatorService
    {
        void MoveElevatorToFloor(int elevatorId, int floor);
        void AddPeopleToElevator(int elevatorId, int numPeople);
        Status? GetElevatorStatus(int elevatorId);
        int RequestElevator(int floor);
    }
}
