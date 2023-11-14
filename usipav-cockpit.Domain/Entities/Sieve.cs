using usipav_cockpit.Domain.Base;

namespace usipav_cockpit.Domain.Entities
{
    public class Sieve : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
    }
}
