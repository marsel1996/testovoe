using test_api.Domain.Enums;

namespace test_api.Domain.Model
{
    public class User
    {
        public virtual long Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string SurName { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual UserRoleEnum Role { get; set; }
    }
}
