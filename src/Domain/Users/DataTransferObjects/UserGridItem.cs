namespace Domain.Users.DataTransferObjects
{
    public class UserGridItem
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Roles { get; set; }
        public bool IsActive { get; set; }
    }
}
