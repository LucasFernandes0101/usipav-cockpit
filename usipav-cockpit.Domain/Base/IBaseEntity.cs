namespace usipav_cockpit.Domain.Base
{
    public interface IBaseEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
