using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;
using test_api.Helpers;

namespace test_api.Application.UserApplication.Queries.GetUserInfoQuery
{
    public class GetUserInfoQuery : IRequest<GetUserInfoDto>
    {
        public long Id { get; set; }
    }

    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoDto>
    {
        public async Task<GetUserInfoDto> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                var user = session.Get<Domain.Model.User>(query.Id);

                return user != null
                    ? new GetUserInfoDto() { 
                        Email = user.Email,
                        FullName = $"{user.SurName} {user.FirstName}",
                        Phone = user.Phone,
                        RoleName = user.Role.GetDescription()
                    }
                    : null;
            }
        }
    }
}
