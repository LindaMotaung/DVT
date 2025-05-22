using DVT.Elevator.Application.Interfaces;

namespace DVT.Elevator.Application.Managers
{
    /// <summary>
    /// Acts as a repository for elevators, managing their lifecycle and providing access to elevator data.
    /// </summary>
    public class ElevatorManager : IElevatorManager
    {
        private List<Domain.Elevator> _elevators = new List<Domain.Elevator>();

        public void AddElevator(Domain.Elevator elevator)
        {
            _elevators.Add(elevator);
        }

        public void RemoveElevator(int elevatorId)
        {
            _elevators = _elevators.Where(e => e.Id != elevatorId).ToList();
        }

        List<Domain.Elevator> IElevatorManager.GetElevators()
        {
            return _elevators;
        }

        public Domain.Elevator GetNearestAvailableElevator(int floor)
        {
            // Ensure the method never returns null to match the interface contract
            var nearestElevator = _elevators.OrderBy(e => Math.Abs(e.CurrentStatus.Floor - floor)).FirstOrDefault();
            if (nearestElevator == null)
            {
                throw new InvalidOperationException("No elevators are available.");
            }
            return nearestElevator;
        }
    }
}
