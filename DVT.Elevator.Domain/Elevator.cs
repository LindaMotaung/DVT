namespace DVT.Elevator.Domain
{
    /// <summary>
    /// Represents an elevator
    /// </summary>
    public class Elevator
    {
        public int Id { get; set; }
        public Status CurrentStatus { get; set; }
        //public Direction CurrentDirection { get; set; }
        public int WeightLimit { get; set; }

        public Elevator(int id, int startFloor, int weightLimit)
        {
            Id = id;
            CurrentStatus = new Status(startFloor, false, Direction.Idle, 0);
            //CurrentDirection = Direction.Idle;
            WeightLimit = weightLimit;
        }

        public void MoveToFloor(int floor)
        {
            if (floor > CurrentStatus.Floor)
            {
                CurrentStatus.Direction = Direction.Up;
            }
            else if (floor < CurrentStatus.Floor)
            {
                CurrentStatus.Direction = Direction.Down;
            }
            else
            {
                CurrentStatus.Direction = Direction.Idle;
            }

            // Logic to move the elevator
            CurrentStatus.IsMoving = true;
            // After reaching the floor
            CurrentStatus.Floor = floor;
            CurrentStatus.IsMoving = false;
            CurrentStatus.Direction = Direction.Idle; // Set direction to idle after reaching the floor
        }

        public void AddPeople(int num)
        {
            if (CurrentStatus.NumPeople + num <= WeightLimit)
            {
                CurrentStatus.NumPeople += num;
            }
            else
            {
                throw new InvalidOperationException("Elevator is at capacity");
            }
        }
    }
}
