using Moq;
using Xunit;
using DVT.Elevator.Application.Interfaces;
using DVT.Elevator.Application.Services;

namespace DVT.Elevator.UnitTests
{
    public class ElevatorServiceTests
    {
        private readonly Mock<IElevatorManager> _elevatorManagerMock;
        private readonly ElevatorService _elevatorService;

        public ElevatorServiceTests()
        {
            _elevatorManagerMock = new Mock<IElevatorManager>();
            _elevatorService = new ElevatorService(_elevatorManagerMock.Object);
        }

        [Fact]
        public void MoveElevatorToFloor_ElevatorExists_ElevatorMovesToFloor()
        {
            // Arrange
            var elevator = new Domain.Elevator(1, 1, 10);
            _elevatorManagerMock.Setup(em => em.GetElevators()).Returns(new List<Domain.Elevator> { elevator });

            // Act
            _elevatorService.MoveElevatorToFloor(1, 5);

            // Assert
            Assert.Equal(5, elevator.CurrentStatus.Floor);
        }

        [Fact]
        public void AddPeopleToElevator_ElevatorExists_PeopleAddedToElevator()
        {
            // Arrange
            var elevator = new Domain.Elevator(1, 1, 10);
            _elevatorManagerMock.Setup(em => em.GetElevators()).Returns(new List<Domain.Elevator> { elevator });

            // Act
            _elevatorService.AddPeopleToElevator(1, 5);

            // Assert
            Assert.Equal(5, elevator.CurrentStatus.NumPeople);
        }

        [Fact]
        public void GetElevatorStatus_ElevatorExists_ReturnsElevatorStatus()
        {
            // Arrange
            var elevator = new Domain.Elevator(1, 1, 10);
            _elevatorManagerMock.Setup(em => em.GetElevators()).Returns(new List<Domain.Elevator> { elevator });

            // Act
            var status = _elevatorService.GetElevatorStatus(1);

            // Assert
            Assert.NotNull(status);
            Assert.Equal(1, status.Floor);
        }


        [Fact]
        public void RequestElevator_NearestElevator_RequestServed()
        {
            // Arrange
            var elevator1 = new Domain.Elevator(1, 1, 10);
            var elevator2 = new Domain.Elevator(2, 5, 10);
            _elevatorManagerMock.Setup(em => em.GetNearestAvailableElevator(3)).Returns(elevator1);

            // Act
            _elevatorService.RequestElevator(3);

            // Assert
            Assert.Equal(3, elevator1.CurrentStatus.Floor);
        }

    }
}
