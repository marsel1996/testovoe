namespace test_api.Application.UserApplication.Queries.GetUserByIdQuery
{
    public class GetUserByIdDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long RoleId { get; set; }
    }
}