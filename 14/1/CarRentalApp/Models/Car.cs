namespace CarRentalApp.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal PricePerDay { get; set; }
        public bool IsAvailable { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Model} - {PricePerDay} руб/день";
        }
    }
}