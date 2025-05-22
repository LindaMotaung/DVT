namespace DVT.Elevator.Domain
{
    /// <summary>
    /// Represents the current status of an elevator,
    /// </summary>
    public class Status
    {
        public int Floor { get; set; }
        public bool IsMoving { get; set; }
        public int NumPeople { get; set; }

        public Status(int floor, bool isMoving, int numPeople)
        {
            Floor = floor;
            IsMoving = isMoving;
            NumPeople = numPeople;
        }
    }
}
