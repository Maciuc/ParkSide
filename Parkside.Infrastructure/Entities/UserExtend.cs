namespace Parkside.Infrastructure.Entities
{
    public partial class UserExtend
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NickName { get; set; }
        public string? UserId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
