namespace usipav_cockpit.Domain.ViewModels
{
    public class SieveViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
    }
}
