namespace DVT.Elevator.Domain
{
    /// <summary>
    /// Represents an elevator
    /// </summary>
    public class Elevator
    {
        public int Id { get; set; }
        public Status CurrentStatus { get; set; }
        public Direction CurrentDirection { get; set; }
        public int WeightLimit { get; set; }

        public Elevator(int id, int startFloor, int weightLimit)
        {
            Id = id;
            CurrentStatus = new Status(startFloor, false, 0);
            CurrentDirection = Direction.Idle;
            WeightLimit = weightLimit;
        }

        public void MoveToFloor(int floor)
        {
            // Logic to move the elevator
            CurrentStatus.IsMoving = true;
            // After reaching the floor
            CurrentStatus.Floor = floor;
            CurrentStatus.IsMoving = false;
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
