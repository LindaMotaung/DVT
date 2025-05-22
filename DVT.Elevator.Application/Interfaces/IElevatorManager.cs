namespace DVT.Elevator.Application.Interfaces
{
    /// <summary>
    /// Provides methods for managing the collection of elevators.
    /// </summary>
    public interface IElevatorManager
    {
        void AddElevator(Domain.Elevator elevator);
        void RemoveElevator(int elevatorId);
        List<Domain.Elevator> GetElevators();
        Domain.Elevator GetNearestAvailableElevator(int floor);
    }
}
