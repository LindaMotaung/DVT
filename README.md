# DVT
Elevator Challenge

In this implementation, I've focused on delivering a working elevator simulation with in-memory state management. If data persistence were a requirement, I would consider adding a data access layer using a database or file storage to ensure data is retained across application restarts.

-----------------
Design Relationship:
- I made use of composition to model the relationship between Elevator and Status as I do not forsee a status existing independently outside of an elevator unless the status is shared across multiple elevators
- Elevator and Direction have a generic association relationship as an elevator uses-a Direction to go Up or Down or remain Idle 

-----------------
In instances where a users enters a non-positive elevatorId, as opposed to throwing an exception, considering the fact that this is a console application that wants to simulate a certain flow in usability, we instead handled the exceptions gracefully because raising an exception would not only terminate the program but is quite a heavy-handed approach. 

-----------------
The difference between ElevatorService.cs and ElevatorManager.cs is their responsibilities and how they fit into the overall architecture/design of the system. 

The ElevatorManager class is responsible for managing the collection of elevators, including adding and removing elevators, and finding the nearest available elevator to a given floor. It provides a basic set of operations for managing elevators, and it's focused on the data and the logic for finding the nearest elevator.

On the other hand, the ElevatorService class is a higher-level class that provides a set of services related to elevators, such as moving an elevator to a floor, adding people to an elevator, and getting the status of an elevator. It uses the ElevatorManager class to perform these operations, and it adds additional logic and validation to ensure that the operations are performed correctly.

In general, the ElevatorManager class is more focused on the data and the basic operations, while the ElevatorService class is more focused on the business logic and the services that are provided to the users.

While it's true that the ElevatorManager and ElevatorService classes are related, separating them into different classes provides several benefits. This separation of responsibilities is an example of the Single Responsibility Principle (SRP) and the Separation of Concerns (SoC) principles, which are important principles in software design. By separating the responsibilities into different classes, we can make the code more modular, maintainable, and scalable.

Additionally, separating the classes allows us to reuse the ElevatorManager class in other parts of the system, if needed. For example, if we need to create a different service that uses the same elevator management logic, we can simply use the ElevatorManager class without having to duplicate the code. For example, say we need a FloorService that will check if a floor is temporarily unavailable due to construction or whatever reason then we can reuse the ElevatorManager class. 

Furthermore, separating the classes makes it easier to test the code. We can test the ElevatorManager class independently of the ElevatorService class, which makes it easier to identify and fix bugs.

While it's possible to put the two classes into one, it would likely make the class more complex and harder to maintain. By separating them, we can keep the code organized and make it easier to understand and modify.
