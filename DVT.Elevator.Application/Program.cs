using DVT.Elevator.Application.Managers;
using DVT.Elevator.Application.Services;
using DVT.Elevator.Domain;

var logFilePath = "elevator_simulation.log";
var elevatorManager = new ElevatorManager();
var elevatorService = new ElevatorService(elevatorManager);

// Add elevators
var elevator1 = new Elevator(1, 1, 10);
var elevator2 = new Elevator(2, 5, 10);
elevatorManager.AddElevator(elevator1);
elevatorManager.AddElevator(elevator2);

Console.WriteLine("Elevator Simulation Game");
Console.WriteLine("-------------------------");

void Log(string message)
{
    File.AppendAllText(logFilePath, $"{DateTime.Now} - {message}{Environment.NewLine}");
}

int? requestedElevatorId = null;

while (true)
{
    if (!requestedElevatorId.HasValue || requestedElevatorId.Value == -1)
    {
        Console.WriteLine("Options:");
        Console.WriteLine("1. Request elevator");
        Console.WriteLine("2. Exit");
        Console.Write("Enter your choice: ");
        if (!int.TryParse(Console.ReadLine(), out var choice) || choice < 1 || choice > 2)
        {
            Console.WriteLine("Invalid input. Please enter a valid choice.");
            continue;
        }
        switch (choice)
        {
            case 1:
                Console.Write("Enter the floor number: ");
                if (!int.TryParse(Console.ReadLine(), out var floor))
                {
                    Console.WriteLine("Invalid input. Please enter a valid floor number.");
                    Log("Invalid floor number input.");
                    continue;
                }
                requestedElevatorId = elevatorService.RequestElevator(floor);
                Log($"Elevator {requestedElevatorId} requested to floor {floor}.");
                if (requestedElevatorId != -1)
                {
                    Console.WriteLine($"Elevator {requestedElevatorId} has been requested to floor {floor}.");
                }
                break;
            case 2:
                Console.WriteLine("Exiting the elevator simulation game. Goodbye!");
                return;
        }
    }
    else
    {
        Console.WriteLine("Options:");
        Console.WriteLine("1. Request elevator");
        Console.WriteLine("2. Move elevator to a floor");
        Console.WriteLine("3. Add people to the elevator");
        Console.WriteLine("4. Get elevator status");
        Console.WriteLine("5. Exit");
        Console.Write("Enter your choice: ");
        if (!int.TryParse(Console.ReadLine(), out var choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("Invalid input. Please enter a valid choice.");
            continue;
        }
        switch (choice)
        {
            case 1:
                Console.Write("Enter the floor number: ");
                if (!int.TryParse(Console.ReadLine(), out var floor))
                {
                    Console.WriteLine("Invalid input. Please enter a valid floor number.");
                    continue;
                }
                requestedElevatorId = elevatorService.RequestElevator(floor);
                if (requestedElevatorId != -1)
                {
                    Console.WriteLine($"Elevator {requestedElevatorId} has been requested to floor {floor}.");
                }
                break;
            case 2:
                Console.Write("Enter the floor number: ");
                if (!int.TryParse(Console.ReadLine(), out floor))
                {
                    Console.WriteLine("Invalid input. Please enter a valid floor number.");
                    continue;
                }

                var currentStatus = elevatorService.GetElevatorStatus(requestedElevatorId.Value);
                string movementDirection = "";
                if (currentStatus != null && floor > currentStatus.Floor)
                {
                    movementDirection = "up";
                }
                else if (currentStatus != null && floor < currentStatus.Floor)
                {
                    movementDirection = "down";
                }

                elevatorService.MoveElevatorToFloor(requestedElevatorId.Value, floor);
                Log($"Elevator {requestedElevatorId} moved to floor {floor}.");
                if (!string.IsNullOrEmpty(movementDirection))
                {
                    Console.WriteLine($"Elevator {requestedElevatorId} has moved {movementDirection} to floor {floor}.");
                }
                else
                {
                    Console.WriteLine($"Elevator {requestedElevatorId} is already on floor {floor}.");
                }
                break;

            case 3:
                int elevatorIdToUse = requestedElevatorId.Value;
                Console.Write("Enter the number of people to add: ");
                if (!int.TryParse(Console.ReadLine(), out var numPeople) || numPeople < 0)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number of people.");
                    continue;
                }
                try
                {
                    elevatorService.AddPeopleToElevator(elevatorIdToUse, numPeople);
                    Log($"{numPeople} people added to elevator {elevatorIdToUse}.");
                    Console.WriteLine($"{numPeople} people have boarded elevator {elevatorIdToUse}.");
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 4:
                var status = elevatorService.GetElevatorStatus(requestedElevatorId.Value);
                if (status != null)
                {
                    string direction = status.Direction switch
                    {
                        Direction.Up => "going up",
                        Direction.Down => "going down",
                        Direction.Idle => "idle",
                        _ => "unknown"
                    };

                    string movementStatus = status.IsMoving ? "currently moving" : "stopped";

                    Console.WriteLine($"Elevator {requestedElevatorId} is {movementStatus} on floor {status.Floor} and is {direction} with {status.NumPeople} people.");
                }
                else
                {
                    Console.WriteLine($"Elevator {requestedElevatorId} not found.");
                }
                break;
        }
    }
}
