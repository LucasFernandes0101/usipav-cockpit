namespace usipav_cockpit.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
