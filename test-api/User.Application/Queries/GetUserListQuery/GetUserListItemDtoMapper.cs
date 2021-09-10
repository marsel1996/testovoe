using test_api.Domain.Model;

namespace test_api.Queries.GetUserListQuery
{
    public static class GetUserListItemDtoMapper
    {
        public static GetUserListItemDto MapToDto(this User user)
        {
            return new GetUserListItemDto()
            {
                Id = user.Id,
                FullName = $"{user.SurName} {user.FirstName}",
                Phone = user.Phone,
                Email = user.Email,
            };
        }
    }
}
