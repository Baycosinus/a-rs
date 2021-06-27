namespace Core.Domain.Entities
{
    public class TripBooking
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int PassengerId { get; set; }
        public int Status { get; set; }

        public virtual Trip Trip { get; set; }
        public virtual User Passenger { get; set; }
    }
}