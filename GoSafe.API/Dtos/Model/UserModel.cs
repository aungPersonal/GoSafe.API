namespace GoSafe.API.Dtos.Model
{
    public class UserModel
    {
        public long Id { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
    }
}
