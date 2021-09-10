using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using test_api.Domain.Enums;
using test_api.Helpers;

namespace test_api.Application.UserApplication.Queries.GetRolesListQuery
{
    public class GetUserRoleListQuery : IRequest<GetUserRolesListItemDto[]>
    {
    }

    public class GetRolesListQueryHandler : IRequestHandler<GetUserRoleListQuery, GetUserRolesListItemDto[]>
    {
        public async Task<GetUserRolesListItemDto[]> Handle(GetUserRoleListQuery query, CancellationToken cancellationToken)
        {
            var data = Enum.GetValues(typeof(UserRoleEnum))
                .Cast<UserRoleEnum>()
                .Select(x => new GetUserRolesListItemDto()
                {
                    Id = (long) x,
                    Name = x.GetDescription(),
                })
                .ToArray();
            return data;
        }
    }
}
