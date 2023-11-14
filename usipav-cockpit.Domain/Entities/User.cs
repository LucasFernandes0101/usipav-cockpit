using usipav_cockpit.Domain.Base;

namespace usipav_cockpit.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
