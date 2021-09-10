namespace test_api.Application.UserApplication.Queries.GetUserListQuery
{
    public static class GetUserListItemDtoMapper
    {
        public static GetUserListItemDto MapToDto(this Domain.Model.User user)
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
